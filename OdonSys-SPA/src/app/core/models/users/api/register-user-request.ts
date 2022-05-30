import { Country } from "../../../../core/enums/country.enum";

export interface RegisterUserRequest {
    name: string;
    middleName?: string;
    lastName: string;
    middleLastName?: string;
    document: string;
    password: string;
    phone: string;
    email: string;
    country: Country
}
