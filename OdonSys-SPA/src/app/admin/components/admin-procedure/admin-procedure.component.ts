import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ColDef, GridOptions } from 'ag-grid-community';
import { first, tap } from 'rxjs/operators';
import { ProcedureApiService } from '../../../admin/service/procedure-admin-api.service';
import { AgGridService } from '../../../core/services/shared/ag-grid.service';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { ProcedureModel } from '../../../core/models/procedure/procedure-model';
import { AlertService } from '../../../core/services/shared/alert.service';

@Component({
  selector: 'app-admin-procedure',
  templateUrl: './admin-procedure.component.html',
  styleUrls: ['./admin-procedure.component.scss']
})
export class AdminProcedureComponent implements OnInit {
  public gridOptions: GridOptions = {};
  public procedureList: ProcedureModel[] = [];

  constructor(
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly procedureApiService: ProcedureApiService,
    private readonly agGridService: AgGridService) {
  }

  ngOnInit() {
    this.setupAgGrid();
    this.getList();
  }

  private getList = () => {
    this.procedureApiService.getAll().pipe(
      tap(response => {
        this.procedureList = Object.assign(Array<ProcedureModel>(), response);
        this.gridOptions.api?.setRowData(this.procedureList);
        this.gridOptions.api?.sizeColumnsToFit();
      })
    ).subscribe();
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getProcedureGridOptions();
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action' ) as ColDef;
    const params: GridActionModel = {
      buttonShow: [ButtonGridActionType.Borrar, ButtonGridActionType.Editar],
      clicked: this.actionColumnClicked
    };
    columnAction.cellRendererParams = params;
  }

  
  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions);
    switch (action) {
      case ButtonGridActionType.Editar:
        this.router.navigate([`${this.router.url}/edit/${currentRowNode.data.code}`]);
        break;
      case ButtonGridActionType.Borrar:
        this.deleteSelectedItem(currentRowNode.data.code);
        break;
      default:
        break;
    }
  }

  public deleteSelectedItem = (code: string): void => {
    this.alertService.showQuestionModal('Estas seguro de eliminar el procedimiento?', 'Los cambios son permanentes.')
    .then((result) => {
      if (result.value) {
        this.procedureApiService.delete(code).pipe(
          first(),
          tap(() => {
            this.alertService.showSuccess('El procedimiento ha sido eliminado');
            this.getList();
          })
        ).subscribe();
      }
    });
  }
}

