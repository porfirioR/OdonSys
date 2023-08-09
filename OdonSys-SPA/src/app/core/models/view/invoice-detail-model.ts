export class InvoiceDetailModel {
  constructor(
    public id: string,
    public invoiceId: string,
    public procedure: string,
    public procedurePrice: number,
    public finalPrice: number,
    public dateCreated: Date,
    public userCreated: string,
    public teeth: string
  ) { }
}
