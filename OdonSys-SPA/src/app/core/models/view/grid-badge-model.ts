import { BadgeType } from "../../constants/badge-type";
import { InvoiceStatus } from "../../enums/invoice-status.enum";

export class GridBadgeModel {

  constructor(
    public title: string,
    public type: InvoiceStatus | any,
    public badge: BadgeType
  ) { }
}
