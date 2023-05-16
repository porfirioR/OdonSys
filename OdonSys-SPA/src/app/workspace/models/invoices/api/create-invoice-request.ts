import { InvoiceStatus } from "../../../../core/enums/invoice-status.enum";
import { CreateInvoiceDetailRequest } from "./create-invoice-detail-request";

export class CreateInvoiceRequest {
  constructor(
    invoiceNumber: string,
    iva10: number,
    totalIva: number,
    subTotal: number,
    total: number,
    timbrado: string,
    status: InvoiceStatus,
    clientId: string,
    invoiceDetails: CreateInvoiceDetailRequest[]
  ) { }
}
