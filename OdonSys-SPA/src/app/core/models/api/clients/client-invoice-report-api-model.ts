import { InvoiceDetailReportApiModel } from "./invoice-detail-report-api-model"

export interface ClientInvoiceReportApiModel {
  id: string
  total: number
  dateCreated: Date
  invoiceDetails: InvoiceDetailReportApiModel[]
}
