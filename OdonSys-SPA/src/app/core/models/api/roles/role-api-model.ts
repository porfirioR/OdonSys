import { DoctorApiModel } from "../doctor/doctor-api-model";

export interface RoleApiModel {
  name: string
  code: string
  userCreated: string
  userUpdated: string
  dateCreated: Date
  dateModified: Date
  rolePermissions: string[]
  userRoles: DoctorApiModel[]
}
