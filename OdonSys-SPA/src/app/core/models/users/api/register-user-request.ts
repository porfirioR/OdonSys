import { Country } from '../../../../core/enums/country.enum';

export class RegisterUserRequest {
  constructor(
    public name: string,
    public surname: string,
    public document: string,
    public password: string,
    public phone: string,
    public email: string,
    public country: Country,
    public secondSurname?: string,
    public middleName?: string,
    public userId?: string,
    public userName?: string
  ) {}
}
