import { Country } from "../../../../core/enums/country.enum";

export interface ClientApiModel {
    id: string;
    active: boolean;
    middleName: string;
    lastName: string;
    middleLastName: string;
    phone: string;
    country: Country;
    email: string
}
