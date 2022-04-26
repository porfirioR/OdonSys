import { ButtonGridActionType } from "../../enums/button-grid-action-type.enum";

export class GridActionModel {
    buttonShow!: ButtonGridActionType[];
    clicked!: (action: ButtonGridActionType) => void;
}
