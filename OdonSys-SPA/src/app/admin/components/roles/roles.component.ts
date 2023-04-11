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

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.scss']
})
export class RolesComponent implements OnInit {
  public load: boolean = false
  public gridOptions!: GridOptions
  public roleList: RoleModel[] = []

  constructor(
    private readonly router: Router,
    private readonly roleApiService: RoleApiService,
    private readonly alertService: AlertService,
    private readonly agGridService: AgGridService
  ) { }

  ngOnInit() {
    this.setupAgGrid();
    this.load = true;
    this.getList();

  }
  private getList = () => {
    this.roleApiService.getAll().subscribe({
      next: (response) => {
        this.roleList =  response.map(x => new RoleModel(x.name, x.code, x.rolePermission, x.userRoles.map(y => new DoctorModel())));
        this.gridOptions.api?.setRowData(this.roleList);
        this.gridOptions.api?.sizeColumnsToFit();
        if (this.roleList.length === 0) {
          this.gridOptions.api?.showNoRowsOverlay();
        }
      },
      error: (e) => {
        this.gridOptions.api?.showNoRowsOverlay();
        throw e;
      }
    });
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getProcedureGridOptions();
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef;
    const params: GridActionModel = {
      buttonShow: [ButtonGridActionType.Editar],
      clicked: this.actionColumnClicked
    };
    columnAction.cellRendererParams = params;
  }

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions);
    switch (action) {
      case ButtonGridActionType.Editar:
        this.router.navigate([`${this.router.url}/actualizar/${currentRowNode.data.id}/${currentRowNode.data.active}`]);
        break;
      default:
        break;
    }
  }
}
