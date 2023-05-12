import { Component, OnInit } from '@angular/core';
import { ColDef, GridOptions } from 'ag-grid-community';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AgGridService } from '../../../core/services/shared/ag-grid.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { UserApiService } from '../../services/user-api.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { environment } from '../../../../environments/environment';
import { ConditionalGridButtonShow } from '../../../core/models/view/conditional-grid-button-show';
import { DoctorApiModel } from '../../../core/models/api/doctor/doctor-api-model';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { SystemAttributeModel } from '../../../core/models/view/system-attribute-model';
import { PatchRequest } from '../../../core/models/api/patch-request';
import { CustomGridButtonShow } from '../../../core/models/view/custom-grid-button-show';
import { FieldId } from '../../../core/enums/field-id.enum';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { OperationType } from '../../../core/enums/operation-type.enum';
import { Permission } from '../../../core/enums/permission.enum';
import { UserRoleComponent } from '../../modals/user-role/user-role.component';

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
  private canAddRoles = false

  constructor(
    private readonly alertService: AlertService,
    private readonly userApiService: UserApiService,
    private readonly agGridService: AgGridService,
    private readonly userInfoService: UserInfoService,
    private modalService: NgbModal,
  ) { }

  ngOnInit() {
    this.attributeActive = (environment.systemAttributeModel as SystemAttributeModel[]).find(x => x.id === FieldId.Active)?.value!
    this.attributeId = (environment.systemAttributeModel as SystemAttributeModel[]).find(x => x.id === FieldId.Id)?.value!
    this.attributeApproved = (environment.systemAttributeModel as SystemAttributeModel[]).find(x => x.id === FieldId.Approved)?.value!
    this.canDeactivate = this.userInfoService.havePermission(Permission.DeactivateDoctors)
    this.canRestore = this.userInfoService.havePermission(Permission.RestoreDoctors)
    this.canApprove = this.userInfoService.havePermission(Permission.ApproveDoctors)
    this.canAddRoles = this.userInfoService.havePermission(Permission.AssignDoctorRoles)
    this.setupAgGrid()
    this.ready = true
    this.getList()
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getDoctorGridOptions()
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef
    const userId = this.userInfoService.getUserData().id.toLocaleUpperCase()

    const conditionalButtons: ConditionalGridButtonShow[] = []
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
    const buttons = conditionalButtons.length
    if (buttons > 2) {
      columnAction.maxWidth = 310
    }
    const params: GridActionModel = {
      buttonShow: [],
      clicked: this.actionColumnClicked,
      conditionalButtons: conditionalButtons,
      customButton: this.canAddRoles ? new CustomGridButtonShow(' Roles', 'fa-id-badge') : undefined
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
      case ButtonGridActionType.CustomButton:
        const modalRef = this.modalService.open(UserRoleComponent)
        modalRef.componentInstance.userId = currentRowNode.data.id
        modalRef.componentInstance.name = currentRowNode.data.name
        modalRef.result.then((result) => {
          if(result) {
            currentRowNode.data.roles = result
            this.gridOptions.api?.refreshCells({ force: true, columns: ['roles'] })
          }
        }, () => {})
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
        this.userApiService.changeVisibility(doctor.id, request).subscribe({
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
        this.gridOptions.api?.refreshCells({ force: true, columns: ['approved'] })
      }, error: (e) => {
        this.loading = false
        throw e
      }
    })
  }
}
