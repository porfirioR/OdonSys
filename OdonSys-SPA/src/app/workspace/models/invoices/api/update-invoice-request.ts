import { UpdateInvoiceDetailRequest } from "./update-invoice-detail-request";

export class UpdateInvoiceRequest {
  constructor(
    public id: string,
    public iva10: number,
    public totalIva: number,
    public subTotal: number,
    public total: number,
    public invoiceDetails: UpdateInvoiceDetailRequest[]
  ) { }
}
