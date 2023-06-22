export class ProcedureModel {
  constructor(
    public id: string,
    public active: boolean,
    public dateCreated: Date,
    public dateModified: Date,
    public name: string,
    public description: string,
    public procedureTeeth: string[],
    public price: number,
    public xRays: boolean
  ) {}
}
