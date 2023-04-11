import { DoctorModel } from "./doctor-model";

export class RoleModel {
  constructor(
    public name: string,
    public code: string,
    public rolePermissions: string[],
    public userRoles?: DoctorModel[]
  ) { }
}
