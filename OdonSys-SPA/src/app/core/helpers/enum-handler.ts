import { Country } from '../enums/country.enum';
import { SelectModel } from '../models/view/select-model';

export class EnumHandler {
  public static getCountries = (): SelectModel[] => {
    const countries: SelectModel[] = []
    Object.entries(Country).forEach(([key, value]) => {
      countries.push(new SelectModel(key as string, value.toString()))
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
  public static getKeyByValue = (enumType: Record<string, string | number>, value: string) => {
    const result = Object.entries(enumType).find(([enumKey, value]) => value === value)?.[0]!
    return result
  }
}
