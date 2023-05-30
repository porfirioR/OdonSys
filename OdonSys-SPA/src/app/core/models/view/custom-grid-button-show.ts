import { ColorType } from "../../constants/color-type";

export class CustomGridButtonShow {
  constructor(
    public title: string,
    public icon: string,
    public isConditionalButton: boolean = false,
    public customColor: ColorType = 'info'
  ) { }
}
