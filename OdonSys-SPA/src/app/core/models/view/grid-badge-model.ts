import { ColorType } from "../../constants/color-type";
import { InvoiceStatus } from "../../enums/invoice-status.enum";

export class GridBadgeModel {

  constructor(
    public title: string,
    public type: InvoiceStatus | any,
    public badge: ColorType
  ) { }
}
