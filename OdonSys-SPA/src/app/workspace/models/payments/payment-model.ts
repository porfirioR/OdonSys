export class PaymentModel {
  constructor(
    public user: string,
    public dateCreated: Date,
    public amount: number,
    public totalDebt: number
  ) { }
}
