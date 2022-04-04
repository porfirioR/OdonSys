import { ButtonGridActionType } from "../../enums/button-grid-action-type.enum";

export interface GridActionModel {
    buttonShow: ButtonGridActionType[];
    clicked: (action: ButtonGridActionType) => void;
}
