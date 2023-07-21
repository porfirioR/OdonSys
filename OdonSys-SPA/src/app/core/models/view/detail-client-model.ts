import { PaymentModel } from "../../../workspace/models/payments/payment-model";
import { InvoiceDetailModel } from "./invoice-detail-model";

export class DetailClientModel {
  procedures: InvoiceDetailModel[]
  payments: PaymentModel[]
  hasPayments: boolean
  hasData: boolean

  constructor() {
    this.procedures = []
    this.payments = []
    this.hasPayments = false
    this.hasData = false
  }
}
