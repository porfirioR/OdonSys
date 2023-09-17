export interface InvoiceDetailReportApiModel {
  id: string
  procedure: string
  procedurePrice: number
  finalPrice: number
  dateCreated: Date
  toothIds: string[]
  displayTeeth: string
}
