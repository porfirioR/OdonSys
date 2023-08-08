import { PaymentModel } from "../../../workspace/models/payments/payment-model";
import { FileModel } from "./file-model";
import { InvoiceDetailModel } from "./invoice-detail-model";

export class DetailClientModel {
  procedures: InvoiceDetailModel[]
  payments: PaymentModel[]
  hasPayments: boolean
  hasData: boolean
  invoicePdfFiles: FileModel[]
  invoiceImageFiles: FileModel[]
  hasFiles: boolean

  constructor() {
    this.procedures = []
    this.payments = []
    this.hasPayments = false
    this.hasData = false
    this.invoicePdfFiles = []
    this.invoiceImageFiles = []
    this.hasFiles = false
  }
}
