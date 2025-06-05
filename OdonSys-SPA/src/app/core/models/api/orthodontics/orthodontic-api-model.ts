import { ClientApiModel } from "../clients/client-api-model"

export interface OrthodonticApiModel {
  id: string
  date: Date
  dateCreated: Date
  dateModified: Date
  description: string
  client: ClientApiModel
}
