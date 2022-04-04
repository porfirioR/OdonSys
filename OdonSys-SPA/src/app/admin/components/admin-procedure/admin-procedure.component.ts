import { Component, OnInit } from '@angular/core';
import { ColDef, GridOptions } from 'ag-grid-community';
import { ProcedureApiService } from 'src/app/admin/service/procedure-admin-api.service';
import { catchError, first, tap } from 'rxjs/operators';
import { AgGridService } from 'src/app/core/services/shared/ag-grid.service';
import { GridActionModel } from 'src/app/core/models/view/grid-action-model';
import { ButtonGridActionType } from 'src/app/core/enums/button-grid-action-type.enum';
import { BasicComponent } from 'src/app/core/components/basic/basic.component';
import { ProcedureModel } from 'src/app/core/models/procedure/procedure-model';

@Component({
  selector: 'app-admin-procedure',
  templateUrl: './admin-procedure.component.html',
  styleUrls: ['./admin-procedure.component.scss']
})
export class AdminProcedureComponent extends BasicComponent implements OnInit {
  public gridOptions: GridOptions = {};
  public documentList: ProcedureModel[] = [];

  constructor(private readonly procedureApiService: ProcedureApiService,
    private readonly agGridService: AgGridService) {
    super();
  }

  ngOnInit() {
    this.setupAgGrid();
  }

  private getList = () => {
    this.procedureApiService.getAll().pipe(
      tap(response => {
        this.documentList = Object.assign(Array<ProcedureModel>(), response);
        this.gridOptions.api?.setRowData(this.documentList);
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

