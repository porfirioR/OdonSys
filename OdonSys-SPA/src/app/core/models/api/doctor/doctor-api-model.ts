import { Country } from "../../../../core/enums/country.enum";

export interface DoctorApiModel {
    id: string;
    name: string;
    middleName: string;
    lastName: string;
    middleLastName: string;
    document: string;
    country: Country;
    email: string;
    phone: string;
    active: boolean;
    approved: boolean;
}
