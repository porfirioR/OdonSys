import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ColDef, ColumnResizedEvent, GridOptions } from 'ag-grid-community';
import { Observable } from 'rxjs';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { InvoiceApiModel } from '../../models/invoices/api/invoice-api-model';
import { InvoicePatchRequest } from '../../models/invoices/api/invoice-patch-request';
import { CustomGridButtonShow } from '../../../core/models/view/custom-grid-button-show';
import { ConditionalGridButtonShow } from '../../../core/models/view/conditional-grid-button-show';
import { GridHideColumnModel } from '../../../core/models/view/grid-hide-column-model';
import { PaymentApiModel } from '../../models/payments/payment-api-model';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { Permission } from '../../../core/enums/permission.enum';
import { InvoiceStatus } from '../../../core/enums/invoice-status.enum';
import { OperationType } from '../../../core/enums/operation-type.enum';
import { AgGridService } from '../../../core/services/shared/ag-grid.service';
import { InvoiceApiService } from '../../services/invoice-api.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
import { AlertService } from '../../../core/services/shared/alert.service';
import { PaymentModalComponent } from '../../modals/payment-modal/payment-modal.component';

@Component({
  selector: 'app-invoices',
  templateUrl: './invoices.component.html',
  styleUrls: ['./invoices.component.scss']
})
export class InvoicesComponent implements OnInit {
  protected gridOptions!: GridOptions
  protected ready: boolean = false
  protected loading: boolean = false
  protected canRegisterInvoice = false
  protected canAccessMyInvoices = false
  protected title: string = ''
  private canRegisterPayments = false
  private canDeactivateInvoice = false
  private canUpdateInvoice = false
  private isMyPermission: boolean = false
  private request$!: Observable<InvoiceApiModel[]>

  constructor(
    private readonly agGridService: AgGridService,
    private readonly router: Router,
    private readonly invoiceApiService: InvoiceApiService,
    private readonly userInfoService: UserInfoService,
    private modalService: NgbModal,
    private readonly activatedRoute: ActivatedRoute,
    private readonly alertService: AlertService
  ) { }

  ngOnInit() {
    this.isMyPermission = this.activatedRoute.snapshot.data['permissions'].some((x: Permission) => x === Permission.AccessMyInvoices)
    this.canRegisterPayments = this.userInfoService.havePermission(Permission.RegisterPayments)
    this.canRegisterInvoice = this.userInfoService.havePermission(Permission.CreateInvoices)
    this.canDeactivateInvoice = this.userInfoService.havePermission(Permission.ChangeInvoiceStatus)
    this.canUpdateInvoice = this.userInfoService.havePermission(Permission.UpdateInvoices)
    this.canAccessMyInvoices = !this.isMyPermission && this.userInfoService.havePermission(Permission.AccessMyInvoices)
    this.request$ = !this.isMyPermission ? this.invoiceApiService.getInvoices() : this.invoiceApiService.getMyInvoices()
    this.title = this.isMyPermission ? 'Mis Facturas' : 'Todas las Facturas'
    this.setupAgGrid()
    this.ready = true
    this.loading = true
    this.request$.subscribe({
      next: (response: InvoiceApiModel[]) => {
        this.gridOptions.api?.setRowData(response)
        this.gridOptions.api?.sizeColumnsToFit()
        if (response.length === 0) {
          this.gridOptions.api?.showNoRowsOverlay()
        }
        this.loading = false
      }, error: (e) => {
        this.gridOptions.api?.showNoRowsOverlay()
        this.loading = false
        throw e
      }
    })
  }

  @HostListener('window:resize', ['$event'])
  private getScreenSize() {
    this.gridOptions.api?.sizeColumnsToFit()
  }

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getInvoiceGridOptions()
    this.gridOptions.onGridReady = () => setTimeout(() => { this.gridOptions.api?.sizeColumnsToFit() }, 1000)
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef
    const conditionalButtons: ConditionalGridButtonShow[] = []
    if (this.canRegisterPayments) {
      conditionalButtons.push(
        new ConditionalGridButtonShow('status', InvoiceStatus.Completado, ButtonGridActionType.CustomButton, OperationType.NotEqual, 'status', InvoiceStatus.Cancelado, OperationType.NotEqual),
        // new ConditionalGridButtonShow('status', InvoiceStatus.Nuevo, ButtonGridActionType.Ver, OperationType.NotEqual, 'status', InvoiceStatus.Pendiente, OperationType.NotEqual),
      )
    }
    if (this.canDeactivateInvoice) {
      conditionalButtons.push(
        new ConditionalGridButtonShow('status', InvoiceStatus.Cancelado, ButtonGridActionType.Desactivar, OperationType.NotEqual, 'status', InvoiceStatus.Completado, OperationType.NotEqual)
      )
    }
    if (this.canUpdateInvoice) {
      conditionalButtons.push(
        new ConditionalGridButtonShow('status', InvoiceStatus.Cancelado, ButtonGridActionType.Editar, OperationType.NotEqual, 'status', InvoiceStatus.Completado, OperationType.NotEqual)
      )
    }
    const buttonToShow: ButtonGridActionType[] = [ ButtonGridActionType.Ver ]
    const params: GridActionModel = {
      buttonShow: buttonToShow,
      clicked: this.actionColumnClicked,
      customButton:  this.canRegisterPayments ? new CustomGridButtonShow(' Pagar', 'fa-money-bill', true, 'success') : undefined,
      conditionalButtons: conditionalButtons
    }
    columnAction.cellRendererParams = params
  }

  private actionColumnClicked = (action: ButtonGridActionType): void => {
    const currentRowNode = this.agGridService.getCurrentRowNode(this.gridOptions)
    switch (action) {
      case ButtonGridActionType.Ver:
        this.router.navigate([`${this.router.url}/ver/${currentRowNode.data.id}`])
        break
      case ButtonGridActionType.Editar:
        if (currentRowNode.data.status == InvoiceStatus.Completado || currentRowNode.data.status == InvoiceStatus.Completado) {
          this.alertService.showInfo(`Solamente pueden ser modificados los estados ${InvoiceStatus.Nuevo} y ${InvoiceStatus.Pendiente}`)
          return
        }
        this.router.navigate([`${this.router.url}/actualizar/${currentRowNode.data.id}`])
        break
      case ButtonGridActionType.Desactivar:
        this.alertService.showQuestionModal(
          'Cancelar factura',
          '¿Estás seguro de que quieres continuar? \nSobre esta factura no se podrán realizar más acciones',
          'question'
        ).then((result) => {
          if (result.value) {
            const status = InvoiceStatus.Cancelado
            const request = new InvoicePatchRequest(status)
            this.invoiceApiService.changeStatus(currentRowNode.data.id, request).subscribe({
              next: () => {
                currentRowNode.setDataValue('status', status)
                const columnToRefresh = ['status', 'action']
                this.gridOptions.api!.refreshCells({ rowNodes: [currentRowNode], columns: columnToRefresh, force: true })
              }
            })
          }
        })
        break
      case ButtonGridActionType.CustomButton:
        const modalRef = this.modalService.open(PaymentModalComponent, {
          size: 'xl',
          backdrop: 'static',
          keyboard: false
        })
        modalRef.componentInstance.invoice = currentRowNode.data
        modalRef.result.then((payment: PaymentApiModel | undefined) => {
          if (!!payment) {
            currentRowNode.setDataValue('status', payment.status)
            currentRowNode.setDataValue('paymentAmount', currentRowNode.data.paymentAmount += payment.amount)
            const columnToRefresh = ['status', 'paymentAmount']
            if (payment.status == InvoiceStatus.Completado) {
              columnToRefresh.push('action')
            }
            this.gridOptions.api!.refreshCells({ rowNodes: [currentRowNode], columns: columnToRefresh, force: true })
          }
        }, () => {})
        break
      default:
        break
    }
  }

  protected onGridSizeChanged = () => {
    const screenWidth = window.innerWidth;
    const invoiceColumns = [
      'userCreated',
      'clientFullName',
      'status',
      'total',
      'moneyColumn',
      'dateCreated',
      'total'
    ]

    const agGridHideColumnList = [
      new GridHideColumnModel(576, ['status', 'userCreated', 'total', 'dateCreated']),
      new GridHideColumnModel(768, ['status', 'userCreated', 'total', 'dateCreated']),
      new GridHideColumnModel(992, ['status', 'userCreated']),
      new GridHideColumnModel(1200, ['status'])
    ]
    const agGridHideColumn = agGridHideColumnList.find(x => screenWidth <= x.screenWidth)
    if (agGridHideColumn) {
      agGridHideColumn.columnsToHide.forEach((column) => this.gridOptions!.columnApi!.setColumnVisible(column, false))
      const columnsToShow = invoiceColumns.filter(x => !agGridHideColumn.columnsToHide.includes(x))
      columnsToShow.forEach((column) => this.gridOptions!.columnApi!.setColumnVisible(column, true))
    } else {
      invoiceColumns.forEach((column) => this.gridOptions!.columnApi!.setColumnVisible(column, true))
    }
    if (this.isMyPermission) {
      this.gridOptions!.columnApi!.setColumnVisible('userCreated', false)
    }
  }
}
