import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ColDef, GridOptions } from 'ag-grid-community';
import { GridActionModel } from '../../../core/models/view/grid-action-model';
import { InvoiceApiModel } from '../../models/invoices/api/invoice-api-model';
import { CustomGridButtonShow } from '../../../core/models/view/custom-grid-button-show';
import { ButtonGridActionType } from '../../../core/enums/button-grid-action-type.enum';
import { Permission } from '../../../core/enums/permission.enum';
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
  private canRegisterPayments = false

  constructor(
    private readonly agGridService: AgGridService,
    private readonly router: Router,
    private readonly invoiceApiService: InvoiceApiService,
    private readonly userInfoService: UserInfoService,
    private modalService: NgbModal,
  ) { }

  ngOnInit() {
    this.canRegisterPayments = this.userInfoService.havePermission(Permission.RegisterPayments)
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
    const params: GridActionModel = {
      buttonShow: [ButtonGridActionType.Editar, ButtonGridActionType.Ver],
      clicked: this.actionColumnClicked,
      customButton:  this.canRegisterPayments ? new CustomGridButtonShow(' Pagos', 'fa-money-bill') : undefined
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
        this.router.navigate([`${this.router.url}/actualizar/${currentRowNode.data.id}`])
        break
      case ButtonGridActionType.CustomButton:
        const modalRef = this.modalService.open(PaymentModalComponent)
        modalRef.componentInstance.userId = currentRowNode.data.id
        modalRef.componentInstance.name = currentRowNode.data.name
        modalRef.result.then((result) => {
          if(result) {
            currentRowNode.data.roles = result
            // this.gridOptions.api?.refreshCells({ force: true, columns: ['roles'] })
          }
        }, () => {})
        break
      default:
        break
    }
  }

}
