import { ColorCustomType } from "../../constants/color-custom-type";

export class CustomGridButtonShow {
  constructor(
    public title: string,
    public icon: string,
    public isConditionalButton: boolean = false,
    public customColor: ColorCustomType = 'info'
  ) { }
}
