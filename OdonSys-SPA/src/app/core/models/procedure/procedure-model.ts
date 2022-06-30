export class ProcedureModel {
  constructor(
    public id: string,
    public active: boolean,
    public dateCreate: Date,
    public dateModified: Date,
    public name: string,
    public description: string,
    public estimatedSessions: string,
    public procedureTeeth: string[]
  ) {}
}
