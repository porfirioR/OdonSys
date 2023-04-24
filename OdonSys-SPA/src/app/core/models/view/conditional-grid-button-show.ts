import { ButtonGridActionType } from "../../enums/button-grid-action-type.enum";
import { OperationType } from "../../enums/operation-type.enum";

export class ConditionalGridButtonShow {
  constructor(
    public attributeAffected: string,
    public attributeValue: string,
    public buttonType: ButtonGridActionType,
    public operator: OperationType = OperationType.Equal,
    public principalAttributeAffected: string = '',
    public principalAttributeValue: string = '',
    public principalOperator: OperationType = OperationType.NotEqual,
  ) { }
}
