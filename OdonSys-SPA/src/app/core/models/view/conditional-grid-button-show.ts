import { ButtonGridActionType } from "../../enums/button-grid-action-type.enum";

export class ConditionalGridButtonShow {
  constructor(
    public attributeAffected: string,
    public attributeValue: string,
    public buttonType: ButtonGridActionType) {
  }
}
