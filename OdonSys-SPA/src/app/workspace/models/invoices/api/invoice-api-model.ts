import { InvoiceStatus } from "../../../../core/enums/invoice-status.enum"
import { InvoiceDetailApiModel } from "./invoice-detail-api-model"

export interface InvoiceApiModel {
  id: string
  invoiceNumber: string
  iva10: number
  totalIva: number
  subTotal: number
  total: number
  timbrado: string
  status: InvoiceStatus
  clientId: string
  dateCreated: Date
  userCreated: string
  invoiceDetails: InvoiceDetailApiModel[]
}
