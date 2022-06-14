import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ColDef, GridOptions } from 'ag-grid-community';
import { DoctorApiModel } from '../../../core/models/api/doctor/doctor-api-model';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { AgGridService } from '../../../core/services/shared/ag-grid.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { UserApiService } from '../../service/doctor-api.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';

@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.scss']
})
export class DoctorsComponent implements OnInit {
  public loading: boolean = false;
  public ready: boolean = false;
  public gridOptions!: GridOptions;

  constructor(
    private readonly alertService: AlertService,
    private readonly userApiService: UserApiService,
    private readonly agGridService: AgGridService,
    private readonly userInfo: UserInfoService

  ) { }

  ngOnInit() {
    this.setupAgGrid();
    this.ready = true;
    this.getList();
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getDoctorGridOptions();
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef;
    const params: GridActionModel = {
      buttonShow: [ButtonGridActionType.Desactivar, ButtonGridActionType.Aprobar],
      clicked: this.actionColumnClicked
    };
    columnAction.cellRendererParams = params;
  }

  private getList = () => {
    this.loading = true;
    this.userApiService.getAll().subscribe({
      next: (response: DoctorApiModel[]) => {
        this.gridOptions.api?.setRowData(response);
        this.gridOptions.api?.sizeColumnsToFit();
        if (response.length === 0) {
          this.gridOptions.api?.showNoRowsOverlay();
        }
        this.loading = false;
      }, error: (e) => {
        this.gridOptions.api?.showNoRowsOverlay();
        this.loading = false;
        throw e;
      }
    });
  }

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions);
    switch (action) {
      case ButtonGridActionType.Aprobar:
        if (!currentRowNode.data.approved) {
          this.approve();
        } else {
          this.alertService.showInfo('El usuario ya está habilitado para usar el sistema.');
        }
        break;
      // case ButtonGridActionType.Ver:
      //   // TODO SHOW DOCTOR DATA
      //   this.router.navigate([`${this.router.url}/ver/${currentRowNode.data.id}`]);
      //   break;
      case ButtonGridActionType.Desactivar:
        if (currentRowNode.data.id === this.userInfo.getUserData().id.toLocaleUpperCase()) {
          this.alertService.showInfo('Usted no puede deshabilitar su cuenta.');
        } else {
          this.deactivateSelectedItem(currentRowNode.data.id);
        }
        break;
      default:
        break;
    }
  }

  private deactivateSelectedItem = (id: string): void => {
    this.alertService.showQuestionModal('¿Está seguro de deshabilitar al doctor?', 'El doctor no podrá ingresar al sistema.').then((result) => {
      if (result.value) {
        this.loading = true;
        this.userApiService.delete(id).subscribe({
          next: () => {
            this.loading = false;
            this.alertService.showSuccess('El doctor ha sido deshabilitado.');
            this.getList();
          }, error: (e) => {
            this.loading = false;
            throw e;
          }
        });
      }
    });
  }

  private approve = (): void => {
    this.loading = true;
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions);
    this.userApiService.approve(currentRowNode.data.id).subscribe({
      next: (response: DoctorApiModel) => {
        currentRowNode.data.approved = response.approved;
        this.alertService.showSuccess('El doctor ha sido habilitado para ingresar al sistema.');
        this.getList();
      }, error: (e) => {
        this.loading = false;
        throw e;
      }
    });
  }
}
