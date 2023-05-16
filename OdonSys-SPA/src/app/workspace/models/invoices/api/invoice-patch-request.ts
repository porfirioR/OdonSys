import { InvoiceStatus } from "../../../../core/enums/invoice-status.enum";

export class InvoicePatchRequest {
  constructor(
    public value: InvoiceStatus,
    public path: string = 'status',
    public op: string = 'replace'
  ) {}
}
