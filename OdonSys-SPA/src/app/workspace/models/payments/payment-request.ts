export class PaymentRequest {
  constructor(
    public invoiceId: string,
    public userId: string,
    public amount: number
  ) {}
}
