export class UpdateProcedureRequest {
  constructor(
    public id: string,
    public description: string,
    public price: number,
    public active: boolean,
    public procedureTeeth: string[],
    public xRays: boolean
    ) {}
}
