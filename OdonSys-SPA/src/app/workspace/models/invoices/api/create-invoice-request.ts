import { InvoiceStatus } from "../../../../core/enums/invoice-status.enum";
import { CreateInvoiceDetailRequest } from "./create-invoice-detail-request";

export class CreateInvoiceRequest {
  constructor(
    public invoiceNumber: string,
    public iva10: number,
    public totalIva: number,
    public subTotal: number,
    public total: number,
    public timbrado: string,
    public status: InvoiceStatus,
    public clientId: string,
    public invoiceDetails: CreateInvoiceDetailRequest[]
  ) { }
}
