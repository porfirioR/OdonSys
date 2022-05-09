import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ColDef, GridOptions } from 'ag-grid-community';
import { throwError } from 'rxjs';
import { catchError, first, tap } from 'rxjs/operators';
import { ButtonGridActionType } from 'src/app/core/enums/button-grid-action-type.enum';
import { GridActionModel } from 'src/app/core/models/view/grid-action-model';
import { DoctorApiService } from 'src/app/core/services/api/doctor-api.service';
import { AgGridService } from 'src/app/core/services/shared/ag-grid.service';
import { AlertService } from 'src/app/core/services/shared/alert.service';
import { DoctorModel } from '../../models/doctors/doctor-model';

@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.scss']
})
export class DoctorsComponent implements OnInit {
  public load: boolean = false;
  public gridOptions!: GridOptions;
  public procedureList: DoctorModel[] = [];

  constructor(
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly doctorApiService: DoctorApiService,
    private readonly agGridService: AgGridService

  ) { }

  ngOnInit() {
    this.setupAgGrid();
    this.load = true;
    this.getList();
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getProcedureGridOptions();
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef;
    const params: GridActionModel = {
      buttonShow: [ButtonGridActionType.Borrar, ButtonGridActionType.Editar],
      clicked: this.actionColumnClicked
    };
    columnAction.cellRendererParams = params;
  }


  private getList = () => {
    this.doctorApiService.getAll().pipe(
      tap(response => {
        this.procedureList = Object.assign(Array<DoctorModel>(), response);
        this.gridOptions.api?.setRowData(this.procedureList);
        this.gridOptions.api?.sizeColumnsToFit();
        if (this.procedureList.length === 0) {
          this.gridOptions.api?.showNoRowsOverlay();
        }
      }), catchError(err => {
        this.gridOptions.api?.showNoRowsOverlay();
        return throwError(err);
      })
    ).subscribe();
  }

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions);
    switch (action) {
      case ButtonGridActionType.Ver:
        this.router.navigate([`${this.router.url}/ver/${currentRowNode.data.id}`]);
        break;
      case ButtonGridActionType.Borrar:
        this.deleteSelectedItem(currentRowNode.data.id);
        break;
      default:
        break;
    }
  }

  private deleteSelectedItem = (id: string): void => {
    this.alertService.showQuestionModal('Estas seguro de eliminar el procedimiento?', 'Los cambios son permanentes.')
    .then((result) => {
      if (result.value) {
        this.doctorApiService.delete(id).pipe(
          first(),
          tap(() => {
            this.alertService.showSuccess('El procedimiento ha sido eliminado');
            this.getList();
          })
        ).subscribe();
      }
    });
  }

  public approve = () => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions);
    this.doctorApiService.approve(currentRowNode.data.id).pipe(
      first(),
      tap(() => {
        this.alertService.showSuccess('El doctor ha sido habilitado para ingresar al sistema.');
        this.getList();
      })
    ).subscribe();
  }
}
