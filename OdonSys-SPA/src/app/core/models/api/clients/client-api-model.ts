import { Country } from "../../../../core/enums/country.enum";

export interface ClientApiModel {
    id: string
    active: boolean
    dateCreated: Date
    dateModified: Date
    name: string
    middleName: string
    surname: string
    secondSurname: string
    document: string
    ruc: string
    country: Country
    debts: boolean
    phone: string
    email: string
}
