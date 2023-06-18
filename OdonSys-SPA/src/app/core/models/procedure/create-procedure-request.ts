export class CreateProcedureRequest {
  constructor(
    public name: string,
    public description: string,
    public price: number,
    public procedureTeeth: string[],
    public xRay: boolean
  ) {}
}
