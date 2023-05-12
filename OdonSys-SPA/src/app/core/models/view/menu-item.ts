import { Permission } from "../../enums/permission.enum";

export class MenuItem {
  constructor(
    public title: string,
    public link: string,
    public permission: Permission
  ) { }
}
