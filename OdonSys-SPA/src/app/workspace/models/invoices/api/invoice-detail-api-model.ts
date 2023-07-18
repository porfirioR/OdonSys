export interface InvoiceDetailApiModel {
  id: string
  invoiceId: string
  procedure: string
  procedurePrice: number
  finalPrice: number
  dateCreated: Date
  userCreated: string
}
