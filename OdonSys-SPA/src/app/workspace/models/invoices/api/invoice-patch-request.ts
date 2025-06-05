import { InvoiceStatus } from "../../../../core/enums/invoice-status.enum";

export class InvoicePatchRequest {
  constructor(
    public value: InvoiceStatus,
    public path = 'status',
    public op = 'replace'
  ) {}
}
