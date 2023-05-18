import { Country } from '../enums/country.enum';

export class EnumHandler {
  public static getCountries = (): Map<string, string> => {
    const countries = new Map<string, string>()
    Object.entries(Country).forEach(([key, value]) => {
      countries.set(key as string, value.toString())
      // if is a enum with key(string) value(number)
      // if (!isNaN(key as any)) {
      //   countries.set(key as string, value.toString())
      // }
    })
    return countries
  }

  /**
   * Receives an enum in the form of a record and returns the value assigned from the received key.
   * 
   * @param enumType is the enum to work with
   * @param key enum key of which you want its value
   * @returns enum value
   */
  public static getValueByKey = (enumType: Record<string, string | number>, key: string) => {
    const result = Object.entries(enumType).find(([enumKey, value]) => enumKey === key)?.[1]!
    return result
  }
}
