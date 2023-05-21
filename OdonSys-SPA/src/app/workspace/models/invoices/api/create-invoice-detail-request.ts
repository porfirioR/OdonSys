export class CreateInvoiceDetailRequest {
  constructor(
    public clientProcedureId: string,
    public procedurePrice: number,
    public finalPrice: number
  ) {}
}
