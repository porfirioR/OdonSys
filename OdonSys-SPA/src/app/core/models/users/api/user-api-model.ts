import { Country } from "../../../../core/enums/country.enum";

export interface UserApiModel {
    id: string;
    userName: string;
    active: boolean;
    approved: boolean;
}
