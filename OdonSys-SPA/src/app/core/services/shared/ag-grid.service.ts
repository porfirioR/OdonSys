import { Injectable } from '@angular/core';
import { ColDef, GridApi, GridOptions, IRowNode } from 'ag-grid-community';
import { agGridLocaleEs } from '../../constants/ag-grid-locale';
import { GridActionsComponent } from '../../components/grid-actions/grid-actions.component';
import { GridBadgeComponent } from '../../components/grid-badge/grid-badge.component';
import { GridBadgeModel } from '../../models/view/grid-badge-model';
import { InvoiceStatus } from '../../enums/invoice-status.enum';

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
    { headerName: 'Precio', field: 'price', type: 'moneyColumn', filter: 'agNumberColumnFilter', minWidth: 40, maxWidth: 120, resizable: true },
    { headerName: 'Activo', field: 'active', filter: false, resizable: true, minWidth: 20, maxWidth: 83,
      cellRenderer: this.booleanFormatter,
      cellStyle: params => ({ color: params.data.active === true ? this.greenColor : this.redColor })
    },
    { headerName: 'Fecha Creada', field: 'dateCreated', type: 'dateColumn', minWidth: 105, maxWidth: 150, resizable: true },
    { headerName: 'Fecha Modificada', field: 'dateModified', type: 'dateColumn', maxWidth: 175, resizable: true },
    { headerName: 'Acciones', field: 'action', sortable: false, filter: false, minWidth: 298, maxWidth: 298, resizable: true,
      cellRendererFramework: GridActionsComponent }
  ]

  private roleColumnDef: ColDef[] = [
    { headerName: 'Nombre', field: 'name', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Código', field: 'code', filter: 'agTextColumnFilter', minWidth: 40, resizable: true },
    { headerName: 'Quien creo', field: 'userCreated', filter: 'agTextColumnFilter', minWidth: 40, maxWidth: 170, resizable: true },
    { headerName: 'Quien cambio', field: 'userUpdated', filter: 'agTextColumnFilter', minWidth: 40, maxWidth: 150, resizable: true },
    { headerName: 'Fecha Creada', field: 'dateCreated', type: 'dateColumn', minWidth: 105, maxWidth: 180, resizable: true },
    { headerName: 'Fecha Modificada', field: 'dateModified', type: 'dateColumn', maxWidth: 175, resizable: true },
    { headerName: 'Acciones', field: 'action', sortable: false, filter: false, minWidth: 200, maxWidth: 260, resizable: true,
      cellRendererFramework: GridActionsComponent }
  ]

  private doctorColumnDef: ColDef[] = [
    { headerName: 'Nombre', field: 'name', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Apellido', field: 'surname', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Correo', field: 'email', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Teléfono', field: 'phone', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Aprobado', field: 'approved', filter: false, resizable: true, maxWidth: 125,
      cellRenderer: this.booleanFormatter,
      cellStyle: params => ({ color: params.data.approved === true ? this.greenColor : this.redColor })
    },
    { headerName: 'Activo', field: 'active', filter: false, resizable: true, maxWidth: 95,
      cellRenderer: this.booleanFormatter,
      cellStyle: params => ({ color: params.data.active === true ? this.greenColor : this.redColor })
    },
    { headerName: 'Roles', field: 'roles', filter: 'agTextColumnFilter', resizable: true,
      wrapText: true, autoHeight: true, cellClass: 'long-text-cell-ag-grid',
      cellRenderer: this.arrayFormatter,
      valueFormatter: this.arrayFormatter,
    },
    { headerName: 'Acciones', field: 'action', sortable: false, filter: false, minWidth: 300, maxWidth: 650, resizable: true,
      cellRendererFramework: GridActionsComponent
    }
  ]

  private clientColumnDef: ColDef[] = [
    { headerName: 'Nombre', field: 'name', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Apellido', field: 'surname', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Teléfono', field: 'phone', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Correo', field: 'email', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Visible', field: 'active', filter: false, resizable: true, maxWidth: 90,
      cellRenderer: this.booleanFormatter,
      cellStyle: params => ({ color: params.data.active === true ? this.greenColor : this.redColor })
    },
    { headerName: 'Acciones', field: 'action', sortable: false, filter: false, maxWidth: 200, resizable: true,
    cellRendererFramework: GridActionsComponent }
  ]

  private invoiceColumnDef: ColDef[] = [
    { headerName: 'Quien Registró', field: 'userCreated', filter: 'agTextColumnFilter', resizable: true, initialWidth: 200, maxWidth: 340 },
    { headerName: 'Estado', field: 'status', filter: 'agTextColumnFilter', resizable: true, initialWidth: 120, maxWidth: 200,
      cellRendererFramework: GridBadgeComponent,
      cellRendererParams: {
        badgeParams: [
          new GridBadgeModel(InvoiceStatus.Nuevo, InvoiceStatus.Nuevo, 'info'),
          new GridBadgeModel(InvoiceStatus.Completado, InvoiceStatus.Completado, 'success'),
          new GridBadgeModel(InvoiceStatus.Cancelado, InvoiceStatus.Cancelado, 'danger'),
          new GridBadgeModel(InvoiceStatus.Pendiente, InvoiceStatus.Pendiente, 'warning'),
        ]
      }
    },
    { headerName: 'Fecha Registrada', field: 'dateCreated', type: 'dateColumn', minWidth: 105, maxWidth: 180, resizable: true },
    { headerName: 'Total', field: 'total', type: 'moneyColumn', filter: 'agNumberColumnFilter', initialWidth: 120, resizable: true, maxWidth: 240 },
    { headerName: 'Monto Pagado', field: 'paymentAmount', type: 'moneyColumn', minWidth: 105, initialWidth: 150, resizable: true },
    { headerName: 'Acciones', field: 'action', sortable: false, filter: false, initialWidth: 160, resizable: true,
      cellRendererFramework: GridActionsComponent }
  ]

  private adminClientColumnDef: ColDef[] = [
    { headerName: 'Nombre', field: 'name', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Apellido', field: 'surname', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Teléfono', field: 'phone', filter: 'agTextColumnFilter', resizable: true },
    { headerName: 'Documento', field: 'document', type: 'numberColumn', resizable: true, maxWidth: 140 },
    { headerName: 'Visible', field: 'active', filter: false, resizable: true, minWidth: 80, maxWidth: 90,
      cellRenderer: this.booleanFormatter,
      cellStyle: params => ({ color: params.data.active === true ? this.greenColor : this.redColor})
    },
    { headerName: 'Acciones', field: 'action', sortable: false, filter: false, maxWidth: 465, resizable: true,
    cellRendererFramework: GridActionsComponent }
  ]

  constructor() { }

  public getGridOptions(): GridOptions {
    const gridOptions: GridOptions = {
      rowSelection: 'single',
      defaultColDef: { sortable: true, filter: true },
      overlayLoadingTemplate: '<span class="ag-overlay-loading-center">Por favor espere mientras cargan las filas.</span>',
      overlayNoRowsTemplate:
        '<span style="padding: 10px; border: 2px solid #444; background: lightgoldenrodyellow;">Sin filas que mostrar.</span>',
      paginationAutoPageSize: true,
      enableCellChangeFlash: true,
      columnDefs: this.columnDef,
      pagination: true,
      localeText: agGridLocaleEs,
      columnTypes: {
        dateColumn: {
          filter: 'agDateColumnFilter',
          tooltipValueGetter: (data) => !data.value ? '' : new Date(data.value).toLocaleDateString('es'),
          valueFormatter: (data) => !data.value ? '' : new Date(data.value).toLocaleDateString('es'),
          filterParams: {
            comparator(filterLocalDateAtMidnight: Date, cellValue: string): number {
              const cellDate = cellValue ? new Date(cellValue) : ''
              if (!cellDate) { return -1 }
              cellDate.setHours(0, 0, 0, 0)
              return cellDate < filterLocalDateAtMidnight ? -1 : cellDate > filterLocalDateAtMidnight ? 1 :0 
            },
            maxNumConditions: 1
          },
          cellStyle: {
            textAlign: 'right'
          },
        },
        moneyColumn: {
          valueFormatter: (cell) => cell.value ? `Gs. ${cell.value.toLocaleString('es', { minimumFractionDigits: 0 }).replace(/\B(?=(\d{3})+(?!\d))/g, '.')}` : 'Gs. 0',
          filter: 'agTextColumnFilter',
          cellStyle: {
            textAlign: 'right'
          }
        },
        numberColumn: {
          valueFormatter: (cell) => cell.value ? `${cell.value.toLocaleString('es', { minimumFractionDigits: 0 }).replace(/\B(?=(\d{3})+(?!\d))/g, '.')}` : '0',
          filter: 'agTextColumnFilter'
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
    grid.columnDefs = this.columnDef.concat(this.adminClientColumnDef) as ColDef[]
    (grid.columnDefs.find((x: ColDef) => x.field === 'id')! as ColDef).hide = true
    return grid
  }

  public getClientGridOptions = (): GridOptions => {
    const grid = this.getGridOptions()
    grid.columnDefs = this.columnDef.concat(this.clientColumnDef)
    return grid
  }

  public getInvoiceGridOptions = (): GridOptions => {
    const grid = this.getGridOptions()
    grid.columnDefs = this.columnDef.concat(this.invoiceColumnDef) as ColDef[]
    (grid.columnDefs.find((x: ColDef) => x.field === 'id')! as ColDef).hide = true
    return grid
  }

  public getCurrentRowNode = (gridOptions: GridOptions): IRowNode<any> => {
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

}
