import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ColDef, GridOptions } from 'ag-grid-community';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { InvoiceApiModel } from '../../models/invoices/api/invoice-api-model';
import { CustomGridButtonShow } from '../../../core/models/view/custom-grid-button-show';
import { ConditionalGridButtonShow } from '../../../core/models/view/conditional-grid-button-show';
import { PaymentApiModel } from '../../models/payments/payment-api-model';
import { InvoicePatchRequest } from '../../models/invoices/api/invoice-patch-request';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { Permission } from '../../../core/enums/permission.enum';
import { InvoiceStatus } from '../../../core/enums/invoice-status.enum';
import { OperationType } from '../../../core/enums/operation-type.enum';
import { AgGridService } from '../../../core/services/shared/ag-grid.service';
import { InvoiceApiService } from '../../services/invoice-api.service';
import { UserInfoService } from '../../../core/services/shared/user-info.service';
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
  private canRegisterPayments = false
  private canDeactivateInvoice = false

  constructor(
    private readonly agGridService: AgGridService,
    private readonly router: Router,
    private readonly invoiceApiService: InvoiceApiService,
    private readonly userInfoService: UserInfoService,
    private modalService: NgbModal
  ) { }

  ngOnInit() {
    this.canRegisterPayments = this.userInfoService.havePermission(Permission.RegisterPayments)
    this.canRegisterInvoice = this.userInfoService.havePermission(Permission.CreateInvoices)
    this.canAccessMyInvoices = this.userInfoService.havePermission(Permission.AccessMyInvoices)
    this.canDeactivateInvoice = this.userInfoService.havePermission(Permission.ChangeInvoiceStatus)
    this.setupAgGrid()
    this.ready = true
    this.getList()
  }

  private getList = () => {
    this.loading = true;
    this.invoiceApiService.getInvoices().subscribe({
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

  private setupAgGrid = (): void => {
    this.gridOptions = this.agGridService.getInvoiceGridOptions()
    const columnAction = this.gridOptions.columnDefs?.find((x: ColDef) => x.field === 'action') as ColDef
    const conditionalButtons: ConditionalGridButtonShow[] = []
    if (this.canRegisterPayments) {
      conditionalButtons.push(
        new ConditionalGridButtonShow('status', InvoiceStatus.Completado, ButtonGridActionType.CustomButton, OperationType.NotEqual, 'status', InvoiceStatus.Cancelado, OperationType.NotEqual),
        new ConditionalGridButtonShow('status', InvoiceStatus.Nuevo, ButtonGridActionType.Ver, OperationType.NotEqual, 'status', InvoiceStatus.Pendiente, OperationType.NotEqual),
      )
    }
    if (this.canDeactivateInvoice) {
      conditionalButtons.push(
        new ConditionalGridButtonShow('status', InvoiceStatus.Cancelado, ButtonGridActionType.Desactivar, OperationType.NotEqual, 'status', InvoiceStatus.Completado, OperationType.NotEqual)
      )
    }
    const params: GridActionModel = {
      buttonShow: [],
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
      case ButtonGridActionType.Desactivar:
        const status = InvoiceStatus.Cancelado
        const request = new InvoicePatchRequest(status)
        this.invoiceApiService.changeStatus(currentRowNode.data.id, request).subscribe({
          next: () => {
            currentRowNode.setDataValue('status', status)
            const columnToRefresh = ['status', 'action']
            this.gridOptions.api!.refreshCells({ rowNodes: [currentRowNode], columns: columnToRefresh, force: true })
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

}
