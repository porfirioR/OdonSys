import { ButtonGridActionType } from "../../enums/button-grid-action-type.enum";
import { ConditionalGridButtonShow } from "./conditional-grid-button-show";
import { CustomGridButtonShow } from "./custom-grid-button-show";

export class GridActionModel {
    buttonShow!: ButtonGridActionType[]
    clicked!: (action: ButtonGridActionType) => void
    conditionalButtons?: ConditionalGridButtonShow[]
    customButton?: CustomGridButtonShow
}
