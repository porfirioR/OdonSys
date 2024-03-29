import { Country } from "../../enums/country.enum";
import { DoctorApiModel } from "../api/doctor/doctor-api-model";

export class ClientModel {
  constructor(
    public id: string,
    public active: boolean,
    public dateCreated: Date,
    public dateModified: Date,
    public name: string,
    public middleName: string,
    public surname: string,
    public secondSurname: string,
    public document: string,
    public ruc: string,
    public country: Country,
    public debts: boolean,
    public phone: string,
    public email: string,
    public doctors: DoctorApiModel[]
  ) {}
}
