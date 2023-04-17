import { DoctorModel } from "./doctor-model";

export class RoleModel {
  constructor(
    public name: string,
    public code: string,
    public userCreated: string,
    public userUpdated: string,
    public dateCreated: Date,
    public dateModified: Date,
    public rolePermissions: string[],
    public userRoles?: DoctorModel[]
  ) { }
}
