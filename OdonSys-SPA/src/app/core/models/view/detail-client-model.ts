import { ProcedureModel } from "../procedure/procedure-model";

export class DetailClientModel {
  procedures: ProcedureModel[]
  hasData: boolean

  constructor() {
    this.procedures = []
    this.hasData = false
  }
}
