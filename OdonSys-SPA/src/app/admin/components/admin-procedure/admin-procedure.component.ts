import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, tap } from 'rxjs';
import { ColDef, GridOptions } from 'ag-grid-community';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { ProcedureModel } from '../../../core/models/procedure/procedure-model';
import { ConditionalGridButtonShow } from '../../../core/models/view/conditional-grid-button-show';
import { SystemAttributeModel } from '../../../core/models/view/system-attribute-model';
import { PatchRequest } from '../../../core/models/api/patch-request';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { Permission } from '../../../core/enums/permission.enum';
import { FieldId } from '../../../core/enums/field-id.enum';
import { AlertService } from '../../../core/services/shared/alert.service';
import { AgGridService } from '../../../core/services/shared/ag-grid.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { selectProcedures } from '../../../core/store/procedures/procedure.selectors';
import  * as fromProceduresActions from '../../../core/store/procedures/procedure.actions';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-admin-procedure',
  templateUrl: './admin-procedure.component.html',
  styleUrls: ['./admin-procedure.component.scss']
})
export class AdminProcedureComponent implements OnInit {
  public load: boolean = false
  public gridOptions!: GridOptions
  protected rowData$!: Observable<ProcedureModel[]>
  protected canCreate = false
  protected canEdit = false
  protected canDelete = false
  private canDeactivate = false
  private canRestore = false
  private attributeActive!: string

  constructor(
    private readonly router: Router,
    private readonly agGridService: AgGridService,
    private store: Store,
    private userInfoService: UserInfoService,
    private readonly alertService: AlertService
  ) { }

  ngOnInit() {
    this.attributeActive = (environment.systemAttributeModel as SystemAttributeModel[]).find(x => x.id === FieldId.Active)?.value!
    this.canCreate = this.userInfoService.havePermission(Permission.CreateProcedures)
    this.canEdit = this.userInfoService.havePermission(Permission.UpdateProcedures)
    this.canDelete = this.userInfoService.havePermission(Permission.DeleteProcedures)
    this.canDeactivate = this.userInfoService.havePermission(Permission.DeactivateProcedures)
    this.canRestore = this.userInfoService.havePermission(Permission.RestoreProcedures)
    this.setupAgGrid()
    let loading = true
    this.rowData$ = this.store.select(selectProcedures).pipe(tap(x => {
      if(loading && x.length === 0) {
        this.store.dispatch(fromProceduresActions.loadProcedures()) 
        loading = false
      }
    }))
    this.load = true
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getProcedureGridOptions()
    const columnAction: ColDef = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action')!
    if (!this.canEdit && this.canDelete) {
      columnAction.hide = true
      return
    }
    const buttonShows = []
    if (this.canEdit) {
      buttonShows.push(ButtonGridActionType.Editar)
    }
    if (this.canDelete) {
      buttonShows.push(ButtonGridActionType.Borrar)
    }
    const conditionalButtons = []
    if (this.canRestore) {
      conditionalButtons.push(new ConditionalGridButtonShow(this.attributeActive, false.toString(), ButtonGridActionType.Restaurar))
    }
    if (this.canDeactivate) {
      conditionalButtons.push(new ConditionalGridButtonShow(this.attributeActive, true.toString(), ButtonGridActionType.Desactivar))
    }
    const params: GridActionModel = {
      buttonShow: [ButtonGridActionType.Borrar, ButtonGridActionType.Editar],
      clicked: this.actionColumnClicked,
      conditionalButtons: conditionalButtons
    }
    columnAction.cellRendererParams = params
  }

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions)
    switch (action) {
      case ButtonGridActionType.Editar:
        this.router.navigate([`${this.router.url}/actualizar/${currentRowNode.data.id}`])
        break
      case ButtonGridActionType.Borrar:
        this.deleteSelectedItem(currentRowNode.data.id)
        break
      case ButtonGridActionType.Restaurar:
      case ButtonGridActionType.Desactivar:
        this.changeSelectedDoctorVisibility(currentRowNode.data)
        break
      default:
        break
    }
  }

  private changeSelectedDoctorVisibility = (procedure: ProcedureModel): void => {
    const message = procedure.active ?
                    '¿Está seguro de deshabilitar el tratamiento?, no será visible y no podra ser seleccionado en el sistema.' :
                    '¿Está seguro de restaurar el tratamiento?, será visible para ser seleccionado en el sistema.'
    this.alertService.showQuestionModal(message).then((result) => {
      if (result.value) {
        const request = new PatchRequest(!procedure.active)
        this.store.dispatch(fromProceduresActions.changeProcedureVisibility({ id: procedure.id, model: request }))
      }
    })
  }

  public deleteSelectedItem = (id: string): void => {
    this.alertService.showInfo('Sin implementar')
    return
    this.alertService.showQuestionModal('¿Está seguro de eliminar el procedimiento?', 'No va a ser seleccionable en otras vistas').then((result) => {
      if (result.value) {
        this.store.dispatch(fromProceduresActions.deleteProcedure({ id }))
      }
    })
  }
}

