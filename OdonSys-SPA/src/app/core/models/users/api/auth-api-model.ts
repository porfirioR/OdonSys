import { UserApiModel } from "./user-api-model";

export interface AuthApiModel {
    user: UserApiModel
    token: string
    expirationDate: string
}
