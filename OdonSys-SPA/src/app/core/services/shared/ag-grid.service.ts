import { Injectable } from '@angular/core';
import { ColDef, GridApi, GridOptions, RowNode } from 'ag-grid-community';
import { GridActionsComponent } from '../../components/grid-actions/grid-actions.component';

@Injectable({
  providedIn: 'root'
})
export class AgGridService {
  private columnDef: ColDef[] = [
    { headerName: 'Id', field: 'id', sortable: true, filter: true, resizable: true, minWidth: 200 }
  ];
  private greenColor = '#1DC9B7';
  private redColor = '#FF6565';

  private procedureColumnDef: ColDef[] = [
    { headerName: 'Nombre', field: 'name', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Descripción', field: 'description', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Sesiones', field: 'estimatedSessions', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Activo', field: 'active', filter: false, resizable: true, maxWidth: 150,
      cellRenderer: this.activeFormatter, cellStyle: params => ({ color: params.data.active === true ? this.greenColor : this.redColor})
    },
    { headerName: 'Fecha Creada', field: 'dateCreated', filter: 'agDateColumnFilter', minWidth: 145, resizable: true },
    { headerName: 'Fecha Modificada', field: 'dateModified', filter: 'agDateColumnFilter', minWidth: 160, resizable: true },
    { headerName: 'Actions', field: 'action', sortable: false, filter: false, minWidth: 200, maxWidth: 250, resizable: true,
    cellRendererFramework: GridActionsComponent }
  ];

  constructor() { }

  public getGridOptions = (): GridOptions => {
    const gridOptions: GridOptions = {
      rowSelection: 'single',
      overlayLoadingTemplate: '<span class="ag-overlay-loading-center">Porfavor espere mientras carga las filas.</span>',
      overlayNoRowsTemplate:
        '<span style="padding: 10px; border: 2px solid #444; background: lightgoldenrodyellow;">Sin filas que mostrar.</span>',
      paginationAutoPageSize: true,
      enableCellChangeFlash: true,
      columnDefs: this.columnDef,
      pagination: true,
      columnTypes: {
        dateColumn: {
          filter: 'agDateColumnFilter',
          filterParams: {
            comparator(filterLocalDateAtMidnight: Date, cellValue: string): number {
              const cellDate = cellValue ? (new Date(cellValue)) : '';
              if (cellDate < filterLocalDateAtMidnight) { return -1;
              } else if (cellDate > filterLocalDateAtMidnight) { return 1;
              } else { return 0; }
            },
          }
        }
      },
    };
    return gridOptions;
  }
  
  // Configuration
  public getProcedureGridOptions = (): GridOptions => {
    const grid = this.getGridOptions();
    grid.columnDefs = this.columnDef.concat(this.procedureColumnDef);
    return grid;
  }
  
  public getCurrentRowNode = (gridOptions: GridOptions): RowNode => {
    const gridApi = gridOptions.api as GridApi;
    const selectedColumnIndex = gridApi.getFocusedCell()?.rowIndex as number;
    const renderSelectedColumnIndex = selectedColumnIndex > gridApi.getRenderedNodes().length ?
                                      selectedColumnIndex - gridApi.getFirstDisplayedRow() : selectedColumnIndex;
    return gridApi.getRenderedNodes()[renderSelectedColumnIndex];
  }
  
  private activeFormatter(cell: { value: any; }): string {
    return cell.value ? 'Si' : 'No';
  }
}
