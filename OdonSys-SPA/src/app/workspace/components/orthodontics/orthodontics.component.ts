import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { ColDef, GridApi, GridOptions, GridReadyEvent } from 'ag-grid-community';
import { forkJoin, Observable, of, switchMap, tap } from 'rxjs';
import { Permission } from '../../../core/enums/permission.enum';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { OrthodonticModel } from '../../../core/models/view/orthodontic-model';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { AgGridService } from '../../../core/services/shared/ag-grid.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { OrthodonticApiService } from '../../../core/services/api/orthodontic-api.service';
import { ClientApiService } from '../../../core/services/api/client-api.service';
import { OrthodonticActions } from '../../../core/store/orthodontics/orthodontic.actions';
import { selectOrthodontics } from '../../../core/store/orthodontics/orthodontic.selectors';

@Component({
  selector: 'app-orthodontics',
  templateUrl: './orthodontics.component.html',
  styleUrls: ['./orthodontics.component.scss']
})
export class OrthodonticsComponent implements OnInit {
  protected gridOptions!: GridOptions
  protected load = false
  protected title = ''
  protected canEdit = false
  protected canCreate = false
  protected rowData$!: Observable<OrthodonticModel[]>
  protected id = ''
  private canDelete = false
  private gridApi!: GridApi
  private isSpecificClientOrthodontic = false

  constructor(
    private readonly agGridService: AgGridService,
    private readonly router: Router,
    private readonly orthodonticApiService: OrthodonticApiService,
    private readonly clientApiService: ClientApiService,
    private readonly userInfoService: UserInfoService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly alertService: AlertService,
    private store: Store,
  ) { }

  ngOnInit() {
    this.isSpecificClientOrthodontic = this.activatedRoute.snapshot.data['permissions'].some((x: Permission) => x === Permission.AccessOrthodontics)
    this.canCreate = this.userInfoService.havePermission(Permission.CreateOrthodontics)
    this.canEdit = this.userInfoService.havePermission(Permission.UpdateOrthodontics)
    this.canDelete = this.userInfoService.havePermission(Permission.DeleteOrthodontics)
    this.title = this.isSpecificClientOrthodontic ? 'Ortodoncias de ' : 'Todas las Ortodoncias'
    this.setupAgGrid()
    this.id = this.activatedRoute.snapshot.params['id']
    if (this.id) {
      this.rowData$ = forkJoin([this.clientApiService.getById(this.id), this.orthodonticApiService.getPatientOrthodonticsById(this.id)]).pipe(
        switchMap(([client, orthodontics]) => {
          this.title += `${client.name} ${client.surname}`
          return of(orthodontics)
        }
      ))
      this.load = true
    } else {
      let loading = true
      this.rowData$ = this.store.select(selectOrthodontics).pipe(tap(x => {
        if(loading && x.length === 0) {
          this.store.dispatch(OrthodonticActions.loadOrthodontics())
          loading = false
        }
        this.gridApi?.sizeColumnsToFit()
      }))
      this.load = true
    }
  }

  protected prepareGrid = (event: GridReadyEvent<any, any>): void => {
    this.gridApi = event.api
    this.gridApi?.sizeColumnsToFit()
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getOrthodonticGridOptions()
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef
    const buttonsToShow: ButtonGridActionType[] = []
    if (this.canDelete) {
      buttonsToShow.push(ButtonGridActionType.Borrar)
    }
    if (this.canEdit) {
      buttonsToShow.push(ButtonGridActionType.Editar)
    }
    const params: GridActionModel = {
      buttonShow: buttonsToShow,
      clicked: this.actionColumnClicked,
    }
    columnAction.cellRendererParams = params
  }

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridApi)
    switch (action) {
      case ButtonGridActionType.Editar:
        this.router.navigate([`${this.router.url}/actualizar/${currentRowNode.data.id}`])
        break
      case ButtonGridActionType.Borrar:
        this.alertService.showQuestionModal(
          'Borrar Ortodoncia',
          '¿Estás seguro de que quieres continuar? \nno se puede revertir esta acción.',
          'question'
        ).then((result) => {
          if (result.value) {
            this.store.dispatch(OrthodonticActions.deleteOrthodontic({ id: currentRowNode.data.id }))
          }
        })
        break
      default:
        break
    }
  }
}
