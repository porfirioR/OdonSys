import { Country } from "../../../../core/enums/country.enum";

export interface ClientApiModel {
    id: string;
    active: boolean;
    dateCreated: Date;
    dateUpdated: Date;
    name: string;
    middleName: string;
    lastName: string;
    middleLastName: string;
    document: string;
    ruc: string;
    country: Country;
    debts: boolean;
    phone: string;
    email: string
}
