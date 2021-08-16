import { Injectable } from '@angular/core';
import { ColDef, GridOptions } from 'ag-grid-community';

@Injectable({
  providedIn: 'root'
})
export class AgGridService {
  private columnDef: ColDef[] = [
    { headerName: 'Id', field: 'id', sortable: true, filter: true, resizable: true, width: 500 }
  ];

  constructor() { }

  public getGridOptions = (): GridOptions => {
    const gridOptions: GridOptions = {
      rowSelection: 'single',
      overlayLoadingTemplate: '<span class="ag-overlay-loading-center">Porfavor espere mientras cargue las filas.</span>',
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
}
