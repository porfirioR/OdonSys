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
   * @param searchKey enum key of which you want its value
   * @returns enum value
   */
  public static getValueByKey = (enumType: Record<string, string | number>, searchKey: string): string | number => {
    const value = Object.entries(enumType).find(([enumKey, _]) => enumKey === searchKey)?.[1]!
    return value
  }

  /**
   * Receives an enum in the form of a record and returns the value assigned from the received key.
   * 
   * @param enumType is the enum to work with
   * @param searchValue enum value of which you want its key
   * @returns enum value
   */
  public static getKeyByValue = (enumType: Record<string, string | number>, searchValue: string): string => {
    const key = Object.entries(enumType).find(([_, value]) => value === searchValue)?.[0]!
    return key
  }
}
