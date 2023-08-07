import { ToothModalModel } from "./tooth-modal-model";

export class ProcedureToothModalModel {
  /**
   * @color Difficult of procedure
   * @teethIds List of teeth ids
   */
  constructor(
    public color: string,
    public teethIds: ToothModalModel[]
  ) { }
}
