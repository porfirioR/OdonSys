export class UpdateInvoiceDetailRequest {
  constructor(
    public id: string,
    public finalPrice: number,
    public toothIds?: string[]
  ) {}
}
