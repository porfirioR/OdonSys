import { Country } from '../enums/country.enum';

export class EnumHandler {
  public static getCountries = (): Map<string, string> => {
    const countries = new Map<string, string>();
    Object.entries(Country).forEach(([key, value]) => {
      if (!isNaN(key as any)) {
        countries.set(key as string, value.toString());
      }
    });
    return countries;
  };

}
