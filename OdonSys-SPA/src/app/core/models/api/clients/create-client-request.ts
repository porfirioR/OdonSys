import { Country } from "../../../../core/enums/country.enum";

export class CreateClientRequest {
  constructor(
    protected name: string,
    protected middleName: string,
    protected surname: string,
    protected secondSurname: string,
    protected phone: string,
    protected country: Country,
    protected email: string,
    protected document: string,
    protected ruc: string
  ) { }
}
