import { Country } from "../enums/country.enum";

export class EnumToMap {

    public static getCountries = () => {
        const countries = new Map<string, string>();
        Object.entries(Country).forEach(([key, value]) => {
          if (!isNaN(key as any)) {
            countries.set(key as string, value.toString());
          }
        });
        return countries;
    }
}
