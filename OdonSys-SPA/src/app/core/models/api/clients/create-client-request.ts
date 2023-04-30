import { Country } from "../../../../core/enums/country.enum";

export class CreateClientRequest {
  constructor(
    protected name: string,
    protected middleName: string,
    protected surname: string,
    protected secondSurname: string,
    protected document: string,
    protected ruc: string,
    protected country: Country,
    protected phone: string,
    protected email: string
  ) { }
}
