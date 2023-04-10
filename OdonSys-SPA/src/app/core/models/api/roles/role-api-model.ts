import { DoctorApiModel } from "../doctor/doctor-api-model";

export interface RoleApiModel {
  name: string,
  code: string,
  rolePermission: string[],
  userRoles: DoctorApiModel
}
