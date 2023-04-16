import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ColDef, GridOptions } from 'ag-grid-community';
import { RoleModel } from '../../../core/models/view/role-model';
import { RoleApiService } from '../../../core/services/api/role-api.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { AgGridService } from '../../../core/services/shared/ag-grid.service';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { DoctorModel } from '../../models/doctors/doctor-model';
import { Store } from '@ngrx/store';
import { Observable, tap } from 'rxjs';
import { selectRoles } from '../../../core/store/roles/roles.selectors';
import  * as fromRolesActions from '../../../core/store/roles/roles.actions';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { Permission } from 'src/app/core/enums/permission.enum';

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

  constructor(
    private readonly router: Router,
    private readonly roleApiService: RoleApiService,
    private readonly alertService: AlertService,
    private readonly agGridService: AgGridService,
    private store: Store,
    private userInfoService: UserInfoService,
    ) { }

  ngOnInit() {
    this.setupAgGrid()
    this.canCreate = this.userInfoService.havePermission(Permission.ManageRoles)
    this.canEdit = this.userInfoService.havePermission(Permission.ManageRoles)
    let loading = true;
    this.rowData$ = this.store.select(selectRoles).pipe(tap(x => {
      if(loading && x.length === 0) { 
        this.store.dispatch(fromRolesActions.loadRoles()) 
        loading = false
      }
      // this.gridOptions.api?.sizeColumnsToFit()
    }))
    this.load = true
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
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions)
    switch (action) {
      case ButtonGridActionType.Editar:
        this.router.navigate([`${this.router.url}/actualizar/${currentRowNode.data.id}`])
        break
      default:
        break
    }
  }
}
