import { Component, HostListener, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { ColDef, GridApi, GridOptions, GridReadyEvent } from 'ag-grid-community';
import { Observable, tap } from 'rxjs';
import { RoleModel } from '../../../core/models/view/role-model';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { AgGridService } from '../../../core/services/shared/ag-grid.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { selectRoles } from '../../../core/store/roles/roles.selectors';
import  * as fromRolesActions from '../../../core/store/roles/roles.actions';
import { Permission } from '../../../core/enums/permission.enum';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.scss']
})
export class RolesComponent implements OnInit {
  public load: boolean = false
  public gridOptions!: GridOptions
  protected rowData$!: Observable<RoleModel[]>
  protected canCreate = false
  protected canEdit = false
  private gridApi!: GridApi

  constructor(
    private readonly router: Router,
    private readonly agGridService: AgGridService,
    private store: Store,
    private userInfoService: UserInfoService,
  ) { }

  ngOnInit() {
    this.canCreate = this.userInfoService.havePermission(Permission.ManageRoles)
    this.canEdit = this.userInfoService.havePermission(Permission.ManageRoles)
    this.setupAgGrid()
    let loading = true;
    this.rowData$ = this.store.select(selectRoles).pipe(tap(x => {
      if(loading && x.length === 0) {
        this.store.dispatch(fromRolesActions.loadRoles())
        loading = false
      }
      this.gridApi?.sizeColumnsToFit()
    }))
    this.load = true
  }

  @HostListener('window:resize', ['$event'])
  private getScreenSize(event?: any) {
    this.gridApi?.sizeColumnsToFit()
  }
  
  protected prepareGrid = (event: GridReadyEvent<any, any>) => {
    this.gridApi = event.api
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getRoleGridOptions()
    const columnAction: ColDef = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action')!
    if (!this.canEdit) {
      columnAction.hide = true
      return
    }
    const params: GridActionModel = {
      buttonShow: [ButtonGridActionType.Editar],
      clicked: this.actionColumnClicked
    }
    columnAction!.cellRendererParams = params
  }

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridApi)
    switch (action) {
      case ButtonGridActionType.Editar:
        this.router.navigate([`${this.router.url}/actualizar/${currentRowNode.data.code}`])
        break
      default:
        break
    }
  }
}
