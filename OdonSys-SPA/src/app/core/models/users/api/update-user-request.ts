import { Country } from '../../../enums/country.enum';

export class UpdateUserRequest {
  constructor(
    public id: string,
    public name: string,
    public middleName: string,
    public surname: string,
    public secondSurname: string,
    public document: string,
    public country: Country,
    public phone: string,
    public active: boolean
  ) {}
}
