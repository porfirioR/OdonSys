import { Country } from "../../enums/country.enum";

export class UpdateUserRequest {
    name!: string;
    secondName!: string;
    lastName!: string;
    secondLastName!: string;
    phone!: string;
    country!: Country
}
