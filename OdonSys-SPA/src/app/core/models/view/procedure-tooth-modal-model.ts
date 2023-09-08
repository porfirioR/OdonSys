import { ToothModalModel } from "./tooth-modal-model";

export class ProcedureToothModalModel {
  /**
   * @teethIds List of tooth ids
   */
  constructor(
    public teethIds: ToothModalModel[]
  ) { }
}
