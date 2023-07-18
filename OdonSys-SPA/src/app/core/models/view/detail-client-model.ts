import { InvoiceDetailModel } from "./invoice-detail-model";

export class DetailClientModel {
  procedures: InvoiceDetailModel[]
  hasData: boolean

  constructor() {
    this.procedures = []
    this.hasData = false
  }
}
