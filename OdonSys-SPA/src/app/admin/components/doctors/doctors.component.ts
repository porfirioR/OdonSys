import { Component, OnInit } from '@angular/core';
import { ColDef, GridOptions } from 'ag-grid-community';
import { DoctorApiModel } from '../../../core/models/api/doctor/doctor-api-model';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { AgGridService } from '../../../core/services/shared/ag-grid.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { UserApiService } from '../../service/user-api.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { ConditionalGridButtonShow } from '../../../core/models/view/conditional-grid-button-show';
import { environment } from '../../../../environments/environment';
import { SystemAttributeModel } from '../../../core/models/view/system-attribute-model';
import { FieldId } from '../../../core/enums/field-id.enum';
import { OperationType } from '../../../core/enums/operation-type.enum';
import { PatchRequest } from '../../../core/models/api/patch-request';
import { CustomGridButtonShow } from '../../../core/models/view/custom-grid-button-show';
import { Permission } from '../../../core/enums/permission.enum';

@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.scss']
})
export class DoctorsComponent implements OnInit {
  public loading: boolean = false
  public ready: boolean = false
  public gridOptions!: GridOptions
  private attributeActive!: string
  private attributeId!: string
  private attributeApproved!: string
  private canDeactivate = false
  private canRestore = false
  private canApprove = false

  constructor(
    private readonly alertService: AlertService,
    private readonly userApiService: UserApiService,
    private readonly agGridService: AgGridService,
    private readonly userInfoService: UserInfoService,
  ) { }

  ngOnInit() {
    this.attributeActive = (environment.systemAttributeModel as SystemAttributeModel[]).find(x => x.id === FieldId.Active)?.value!
    this.attributeId = (environment.systemAttributeModel as SystemAttributeModel[]).find(x => x.id === FieldId.Id)?.value!
    this.attributeApproved = (environment.systemAttributeModel as SystemAttributeModel[]).find(x => x.id === FieldId.Approved)?.value!
    this.canDeactivate = this.userInfoService.havePermission(Permission.DeactivateDoctors)
    this.canRestore = this.userInfoService.havePermission(Permission.RestoreDoctors)
    this.canApprove = this.userInfoService.havePermission(Permission.ApproveDoctors)
    this.setupAgGrid()
    this.ready = true
    this.getList()
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getDoctorGridOptions()
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef
    const userId = this.userInfoService.getUserData().id.toLocaleUpperCase()

    const conditionalButtons = []
    if (this.canRestore) {
      conditionalButtons.push(
        new ConditionalGridButtonShow(this.attributeActive, false.toString(), ButtonGridActionType.Restaurar, OperationType.Equal, this.attributeId, userId, OperationType.NotEqual),
      )
    }
    if (this.canDeactivate) {
      conditionalButtons.push(
        new ConditionalGridButtonShow(this.attributeActive, true.toString(), ButtonGridActionType.Desactivar, OperationType.Equal, this.attributeId, userId, OperationType.NotEqual),
      )
    }
    if (this.canApprove) {
      conditionalButtons.push(
        new ConditionalGridButtonShow(this.attributeApproved, false.toString(), ButtonGridActionType.Aprobar)
      )
    }

    const params: GridActionModel = {
      buttonShow: [],
      clicked: this.actionColumnClicked,
      conditionalButtons: conditionalButtons,
      customButton: new CustomGridButtonShow(' Roles', 'fa-id-badge')
    }
    columnAction.cellRendererParams = params
  }

  private getList = () => {
    this.loading = true
    this.userApiService.getAll().subscribe({
      next: (response: DoctorApiModel[]) => {
        this.gridOptions.api?.setRowData(response)
        this.gridOptions.api?.sizeColumnsToFit()
        if (response.length === 0) {
          this.gridOptions.api?.showNoRowsOverlay()
        }
        this.loading = false
      }, error: (e) => {
        this.gridOptions.api?.showNoRowsOverlay()
        this.loading = false
        throw e
      }
    })
  }

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions)
    switch (action) {
      case ButtonGridActionType.Aprobar:
        this.approve()
        break
      case ButtonGridActionType.Desactivar:
      case ButtonGridActionType.Restaurar:
        this.changeSelectedDoctorVisibility(currentRowNode.data)
        break
      default:
        break
    }
  }

  private changeSelectedDoctorVisibility = (doctor: DoctorApiModel): void => {
    const message = doctor.active ?
                    '¿Está seguro de deshabilitar al doctor?, no será visible y no podra acceder al sistema' :
                    '¿Está seguro de habilitar al doctor?, será visible para los doctores y podra acceder al sistema'
    this.alertService.showQuestionModal(message).then((result) => {
      if (result.value) {
        this.loading = true
        const request = new PatchRequest(!doctor.active)
        this.userApiService.doctorVisibility(doctor.id, request).subscribe({
          next: () => {
            this.loading = false
            this.alertService.showSuccess('Visibilidad del doctor ha sido actualizado.')
            this.getList()
          }, error: (e) => {
            this.loading = false
            throw e
          }
        })
      }
    })
  }

  private approve = (): void => {
    this.loading = true
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions)
    this.userApiService.approve(currentRowNode.data.id).subscribe({
      next: (response: DoctorApiModel) => {
        currentRowNode.data.approved = response.approved
        this.alertService.showSuccess('El doctor ha sido habilitado para ingresar al sistema.')
        this.getList()
      }, error: (e) => {
        this.loading = false
        throw e
      }
    })
  }
}
