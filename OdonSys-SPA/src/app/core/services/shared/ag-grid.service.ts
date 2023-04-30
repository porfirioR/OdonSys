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
  private greenColor = '#1DC9B7'
  private redColor = '#FF6565'

  private procedureColumnDef: ColDef[] = [
    { headerName: 'Nombre', field: 'name', filter: 'agTextColumnFilter', minWidth: 260, resizable: true },
    { headerName: 'Descripción', field: 'description', filter: 'agTextColumnFilter', minWidth: 200, resizable: true },
    { headerName: 'Precio', field: 'price', type: 'numberColumn', filter: 'agNumberColumnFilter', minWidth: 40, maxWidth: 120, resizable: true },
    { headerName: 'Activo', field: 'active', filter: false, resizable: true, minWidth: 20, maxWidth: 83,
      cellRenderer: this.booleanFormatter, cellStyle: params => ({ color: params.data.active === true ? this.greenColor : this.redColor })
    },
    { headerName: 'Fecha Creada', field: 'dateCreated', filter: 'agDateColumnFilter', minWidth: 105, maxWidth: 150, resizable: true,
      valueFormatter: params => this.localDateFormatter({value: params.data.dateCreated}),
      tooltipValueGetter: params => this.localDateFormatter({value: params.data.dateCreated}) },
    { headerName: 'Fecha Modificada', field: 'dateModified', filter: 'agDateColumnFilter', maxWidth: 175, resizable: true,
      valueFormatter: params => this.localDateFormatter({value: params.data.dateModified}),
      tooltipValueGetter: params => this.localDateFormatter({value: params.data.dateModified}) },
    { headerName: 'Actions', field: 'action', sortable: false, filter: false, minWidth: 298, maxWidth: 298, resizable: true,
    cellRendererFramework: GridActionsComponent }
  ]

  private roleColumnDef: ColDef[] = [
    { headerName: 'Nombre', field: 'name', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Código', field: 'code', filter: 'agTextColumnFilter', minWidth: 40, resizable: true },
    { headerName: 'Quien creo', field: 'userCreated', filter: 'agTextColumnFilter', minWidth: 40, maxWidth: 170, resizable: true },
    { headerName: 'Quien cambio', field: 'userUpdated', filter: 'agTextColumnFilter', minWidth: 40, maxWidth: 150, resizable: true },
    { headerName: 'Fecha Creada', field: 'dateCreated', filter: false, minWidth: 105, maxWidth: 180, resizable: true,
      valueFormatter: params => this.localDateFormatter({value: params.data.dateCreated}),
      tooltipValueGetter: params => this.localDateFormatter({value: params.data.dateCreated}) },
    { headerName: 'Fecha Modificada', field: 'dateModified', filter: false, maxWidth: 175, resizable: true,
      valueFormatter: params => this.localDateFormatter({value: params.data.dateModified}),
      tooltipValueGetter: params => this.localDateFormatter({value: params.data.dateModified}) },
    { headerName: 'Actions', field: 'action', sortable: false, filter: false, minWidth: 200, maxWidth: 260, resizable: true,
    cellRendererFramework: GridActionsComponent }
  ]

  private doctorColumnDef: ColDef[] = [
    { headerName: 'Nombre', field: 'name', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Apellido', field: 'surname', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Correo', field: 'email', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Teléfono', field: 'phone', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Aprobado', field: 'approved', filter: false, resizable: true, maxWidth: 125,
      cellRenderer: this.booleanFormatter, cellStyle: params => ({ color: params.data.approved === true ? this.greenColor : this.redColor })
    },
    { headerName: 'Activo', field: 'active', filter: false, resizable: true, maxWidth: 95,
      cellRenderer: this.booleanFormatter, cellStyle: params => ({ color: params.data.active === true ? this.greenColor : this.redColor })
    },
    { headerName: 'Roles', field: 'roles', filter: 'agTextColumnFilter', resizable: true,
      cellRenderer: this.arrayFormatter, valueFormatter: this.arrayFormatter },
    { headerName: 'Actions', field: 'action', sortable: false, filter: false, minWidth: 300, maxWidth: 650, resizable: true,
    cellRendererFramework: GridActionsComponent }
  ]

  private clientColumnDef: ColDef[] = [
    { headerName: 'Nombre', field: 'name', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Apellido', field: 'lastName', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Teléfono', field: 'phone', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Correo', field: 'email', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Visible', field: 'active', filter: false, resizable: true, maxWidth: 90,
      cellRenderer: this.booleanFormatter, cellStyle: params => ({ color: params.data.active === true ? this.greenColor : this.redColor })
    },
    { headerName: 'Actions', field: 'action', sortable: false, filter: false, maxWidth: 200, resizable: true,
    cellRendererFramework: GridActionsComponent }
  ]

  private adminClientColumnDef: ColDef[] = [
    { headerName: 'Nombre', field: 'name', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Apellido', field: 'lastName', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Teléfono', field: 'phone', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Correo', field: 'email', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Visible', field: 'active', filter: false, resizable: true, minWidth: 80, maxWidth: 90,
      cellRenderer: this.booleanFormatter, cellStyle: params => ({ color: params.data.active === true ? this.greenColor : this.redColor})
    },
    { headerName: 'Actions', field: 'action', sortable: false, filter: false, maxWidth: 550, resizable: true,
    cellRendererFramework: GridActionsComponent }
  ]

  constructor() { }

  public getGridOptions(): GridOptions {
    const gridOptions: GridOptions = {
      rowSelection: 'single',
      defaultColDef: { sortable: true, filter: true},
      overlayLoadingTemplate: '<span class="ag-overlay-loading-center">Por favor espere mientras carga las filas.</span>',
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
        },
        numberColumn: {
          valueFormatter: (cell) => cell.value ? `Gs. ${cell.value.toLocaleString('es', { minimumFractionDigits: 0 }).replace(/\B(?=(\d{3})+(?!\d))/g, '.')}` : 'Gs. 0',
          filter: 'agTextColumnFilter',
          cellStyle: {
            textAlign: 'right'
          }
        }
      }
    }
    return gridOptions
  }

  // Configuration
  public getProcedureGridOptions = (): GridOptions => {
    const grid = this.getGridOptions()
    grid.columnDefs = this.columnDef.concat(this.procedureColumnDef) as ColDef[]
    (grid.columnDefs.find((x: ColDef) => x.field === 'id')! as ColDef).hide = true
    return grid
  }

  public getRoleGridOptions = (): GridOptions => {
    const grid = this.getGridOptions()
    grid.columnDefs = [...this.roleColumnDef]
    return grid
  }

  // Configuration
  public getDoctorGridOptions = (): GridOptions => {
    const grid = this.getGridOptions()
    grid.columnDefs = this.columnDef.concat(this.doctorColumnDef) as ColDef[]
    (grid.columnDefs.find((x: ColDef) => x.field === 'id')! as ColDef).hide = true
    return grid
  }

  public getAdminClientGridOptions = (): GridOptions => {
    const grid = this.getGridOptions()
    grid.columnDefs = this.columnDef.concat(this.adminClientColumnDef)
    return grid
  }

  public getClientGridOptions = (): GridOptions => {
    const grid = this.getGridOptions()
    grid.columnDefs = this.columnDef.concat(this.clientColumnDef)
    return grid
  }

  public getCurrentRowNode = (gridOptions: GridOptions): RowNode => {
    const gridApi = gridOptions.api as GridApi
    const selectedColumnIndex = gridApi.getFocusedCell()?.rowIndex as number
    const renderSelectedColumnIndex = selectedColumnIndex > gridApi.getRenderedNodes().length ?
                                      selectedColumnIndex - gridApi.getFirstDisplayedRow() : selectedColumnIndex
    return gridApi.getRenderedNodes()[renderSelectedColumnIndex]
  }

  private booleanFormatter(cell: { value: any }): string {
    return cell.value ? 'Si' : 'No'
  }

  private arrayFormatter(cell: { value: any }): string {
    return cell.value ? cell.value.join(', ') : ''
  }

  private localDateFormatter(data: any): string {
    return !data.value ? '' : new Date(data.value).toLocaleDateString()
  }

}
