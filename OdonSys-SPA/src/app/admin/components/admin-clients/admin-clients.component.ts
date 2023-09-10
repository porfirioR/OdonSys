import { Component, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, tap } from 'rxjs';
import { ColDef, GridOptions } from 'ag-grid-community';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { FieldId } from '../../../core/enums/field-id.enum';
import { Permission } from '../../../core/enums/permission.enum';
import { AgGridService } from '../../../core/services/shared/ag-grid.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { PatchRequest } from '../../../core/models/api/patch-request';
import { ClientApiModel } from '../../../core/models/api/clients/client-api-model';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { ConditionalGridButtonShow } from '../../../core/models/view/conditional-grid-button-show';
import { SystemAttributeModel } from '../../../core/models/view/system-attribute-model';
import { ClientModel } from '../../../core/models/view/client-model';
import { CustomGridButtonShow } from '../../../core/models/view/custom-grid-button-show';
import { selectClients } from '../../../core/store/clients/client.selectors';
import  * as fromClientsActions from '../../../core/store/clients/client.actions';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-admin-clients',
  templateUrl: './admin-clients.component.html',
  styleUrls: ['./admin-clients.component.scss']
})
export class AdminClientsComponent implements OnInit {
  public load: boolean = false
  public gridOptions!: GridOptions
  private attributeActive!: string
  protected rowData$!: Observable<ClientModel[]>
  protected canCreate = false
  private canEdit = false
  private canDelete = false
  private canDeactivate = false
  private canRestore = false
  private canAssignToDoctor = false

  constructor(
    private readonly router: Router,
    private readonly agGridService: AgGridService,
    private store: Store,
    private readonly alertService: AlertService,
    private userInfoService: UserInfoService,
  ) { }

  ngOnInit() {
    this.attributeActive = (environment.systemAttributeModel as SystemAttributeModel[]).find(x => x.id === FieldId.Active)?.value!
    this.canCreate = this.userInfoService.havePermission(Permission.CreateClients)
    this.canEdit = this.userInfoService.havePermission(Permission.UpdateClients)
    this.canDelete = this.userInfoService.havePermission(Permission.DeleteClients)
    this.canDeactivate = this.userInfoService.havePermission(Permission.DeactivateClients)
    this.canRestore = this.userInfoService.havePermission(Permission.RestoreClients)
    this.canAssignToDoctor = this.userInfoService.havePermission(Permission.AssignClients)
    this.setupAgGrid()
    let loading = true
    this.rowData$ = this.store.select(selectClients).pipe(tap(x => {
      if(loading && x.length === 0) {
        this.store.dispatch(fromClientsActions.loadClients()) 
        loading = false
      }
      this.gridOptions.api?.sizeColumnsToFit()
    }))
    this.load = true
  }

  @HostListener('window:resize', ['$event'])
  private getScreenSize(event?: any) {
    this.gridOptions.api?.sizeColumnsToFit()
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getAdminClientGridOptions()
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef
    columnAction.minWidth = 360
    const conditionalButtons = []
    const buttonsToShow: ButtonGridActionType[] = [
      ButtonGridActionType.Ver
    ]
    if (this.canDeactivate) {
      conditionalButtons.push(new ConditionalGridButtonShow(this.attributeActive, true.toString(), ButtonGridActionType.Desactivar))
    }
    if (this.canRestore) {
      conditionalButtons.push(new ConditionalGridButtonShow(this.attributeActive, false.toString(), ButtonGridActionType.Aprobar))
    }
    // if (this.canDelete) {
    //   buttonsToShow.push(ButtonGridActionType.Borrar)
    // }
    if (this.canEdit) {
      buttonsToShow.push(ButtonGridActionType.Editar)
    }
    const buttons = buttonsToShow.length + conditionalButtons.length
    if (buttons > 4) {
      columnAction.minWidth = 463
    }
    const params: GridActionModel = {
      buttonShow: buttonsToShow,
      clicked: this.actionColumnClicked,
      conditionalButtons: conditionalButtons.length > 0 ? conditionalButtons : undefined,
      // customButton:  this.canAssignToDoctor ? new CustomGridButtonShow(' Doctores', 'fa-stethoscope') : undefined
      customButton:  new CustomGridButtonShow('Reporte', 'fa-file-lines')
    }
    columnAction.cellRendererParams = params
  }

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions)
    switch (action) {
      case ButtonGridActionType.Aprobar:
      case ButtonGridActionType.Desactivar:
        this.changeSelectedClientVisibility(currentRowNode.data)
        break
      case ButtonGridActionType.CustomButton:
        this.router.navigate([`${this.router.url}/reporte/${currentRowNode.data.id}`])
        break
      case ButtonGridActionType.Aprobar:
        this.alertService.showInfo('No implementado.')
        // this.router.navigate([`${this.router.url}/ver/${currentRowNode.data.id}`])
        break
      case ButtonGridActionType.Ver:
        this.router.navigate([`${this.router.url}/ver/${currentRowNode.data.id}`])
        break
      case ButtonGridActionType.Editar:
        this.router.navigate([`${this.router.url}/actualizar/${currentRowNode.data.id}`])
        break
      case ButtonGridActionType.Borrar:
        this.alertService.showInfo('No implementado.')
        break
      default:
        break
    }
  }

  private changeSelectedClientVisibility = (client: ClientApiModel): void => {
    const message = client.active ?
      '¿Está seguro de deshabilitar al paciente, no será visible para los doctores?' :
      '¿Está seguro de habilitar al paciente, será visible para los doctores?'
    this.alertService.showQuestionModal(message).then((result) => {
      if (result.value) {
        const request = new PatchRequest(!client.active)
        this.store.dispatch(fromClientsActions.changeClientVisibility({ id: client.id, model: request }))
      }
    })
  }

}
