import { Country } from "../../enums/country.enum";

export class DoctorModel {
  constructor(
    public id: string,
    public name: string,
    public middleName: string,
    public surname: string,
    public secondSurname: string,
    public document: string,
    public country: Country,
    public email: string,
    public phone: string,
    public userName: string,
    public active: boolean,
    public approved: boolean,
    public roles: string[],
  ) { }
}
