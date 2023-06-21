import { Component, OnInit } from '@angular/core';
import { ColDef, GridOptions } from 'ag-grid-community';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import { Observable, tap } from 'rxjs';
import { AgGridService } from '../../../core/services/shared/ag-grid.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { environment } from '../../../../environments/environment';
import { ConditionalGridButtonShow } from '../../../core/models/view/conditional-grid-button-show';
import { DoctorApiModel } from '../../../core/models/api/doctor/doctor-api-model';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { SystemAttributeModel } from '../../../core/models/view/system-attribute-model';
import { PatchRequest } from '../../../core/models/api/patch-request';
import { CustomGridButtonShow } from '../../../core/models/view/custom-grid-button-show';
import { DoctorModel } from '../../models/doctors/doctor-model';
import { FieldId } from '../../../core/enums/field-id.enum';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { OperationType } from '../../../core/enums/operation-type.enum';
import { Permission } from '../../../core/enums/permission.enum';
import { selectDoctors } from '../../../core/store/doctors/doctor.selectors';
import { UserRoleComponent } from '../../modals/user-role/user-role.component';
import  * as fromDoctorsActions from '../../../core/store/doctors/doctor.actions';

@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.scss']
})
export class DoctorsComponent implements OnInit {
  public load: boolean = false
  public gridOptions!: GridOptions
  protected rowData$!: Observable<DoctorModel[]>
  private attributeActive!: string
  private attributeId!: string
  private attributeApproved!: string
  private canDeactivate = false
  private canRestore = false
  private canApprove = false
  private canAddRoles = false

  constructor(
    private readonly alertService: AlertService,
    private readonly agGridService: AgGridService,
    private readonly userInfoService: UserInfoService,
    private modalService: NgbModal,
    private store: Store,
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
    let loading = true
    this.rowData$ = this.store.select(selectDoctors).pipe(tap(x => {
      if(loading && x.length === 0) {
        this.store.dispatch(fromDoctorsActions.loadDoctors()) 
        loading = false
      }
    }))
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getDoctorGridOptions()
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef
    const userId = this.userInfoService.getUserData().id.toLocaleUpperCase()
    const conditionalButtons: ConditionalGridButtonShow[] = []
    if (this.canRestore) {
      conditionalButtons.push(new ConditionalGridButtonShow(this.attributeActive, false.toString(), ButtonGridActionType.Restaurar, OperationType.Equal, this.attributeId, userId, OperationType.NotEqual))
    }
    if (this.canDeactivate) {
      conditionalButtons.push(new ConditionalGridButtonShow(this.attributeActive, true.toString(), ButtonGridActionType.Desactivar, OperationType.Equal, this.attributeId, userId, OperationType.NotEqual))
    }
    if (this.canApprove) {
      conditionalButtons.push(new ConditionalGridButtonShow(this.attributeApproved, false.toString(), ButtonGridActionType.Aprobar))
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

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions)
    switch (action) {
      case ButtonGridActionType.Aprobar:
        this.approveDoctor()
        break
      case ButtonGridActionType.Desactivar:
      case ButtonGridActionType.Restaurar:
        this.changeSelectedDoctorVisibility(currentRowNode.data)
        break
      case ButtonGridActionType.CustomButton:
        const modalRef = this.modalService.open(UserRoleComponent)
        modalRef.componentInstance.userId = currentRowNode.data.id
        modalRef.componentInstance.name = currentRowNode.data.name
        modalRef.result.then((result: string[]) => {
          if(result) {
            this.store.dispatch(fromDoctorsActions.updateDoctorRoles({ doctor: currentRowNode.data, doctorRoles: result }))
            setTimeout(() => this.gridOptions.api?.refreshCells({ force: true, columns: ['roles'] }))
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
        this.load = true
        const request = new PatchRequest(!doctor.active)
        this.store.dispatch(fromDoctorsActions.changeDoctorVisibility({ id: doctor.id, model: request }))
      }
    })
  }

  private approveDoctor = (): void => {
    this.load = true
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions)
    this.store.dispatch(fromDoctorsActions.approveDoctor({ doctorId: currentRowNode.data.id }))
    setTimeout(() => this.gridOptions.api?.refreshCells({ force: true, columns: ['approved'] }))
  }
}
