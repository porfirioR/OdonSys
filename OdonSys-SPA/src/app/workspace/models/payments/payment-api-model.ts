import { InvoiceStatus } from "../../../core/enums/invoice-status.enum"

export interface PaymentApiModel {
  invoiceId: string
  userId: string
  dateCreated: Date
  amount: number
  status: InvoiceStatus
}
