import { ClientModel } from "./client-model";

export class OrthodonticModel {
  constructor(
    public id: string,
    public date: Date,
    public dateCreated: Date,
    public dateModified: Date,
    public description: string,
    public client: ClientModel
  ) {}
}
