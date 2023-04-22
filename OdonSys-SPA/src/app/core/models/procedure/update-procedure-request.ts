export class UpdateProcedureRequest {
  constructor(
    public id: string,
    public description: string,
    public price: number,
    public procedureTeeth: string[]
  ) {}
}
