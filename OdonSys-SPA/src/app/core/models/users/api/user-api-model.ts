import { Country } from "../../../../core/enums/country.enum";

export interface UserApiModel {
    id: string;
    name: string;
    lastName: string;
    document: string;
    email: string;
    phone: string;
    country: Country;
    active: boolean;
    approved: boolean;
}
