import { InvoiceDetailApiModel } from "./invoice-detail-api-model"
import { InvoiceStatus } from "../../../../core/enums/invoice-status.enum"

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
  clientFullName: string
  dateCreated: Date
  userCreated: string
  invoiceDetails: InvoiceDetailApiModel[]
}
