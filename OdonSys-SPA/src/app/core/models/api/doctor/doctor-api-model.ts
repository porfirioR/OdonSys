import { Country } from '../../../../core/enums/country.enum';

export interface DoctorApiModel {
  id: string
  name: string
  middleName: string
  surname: string
  secondSurname: string
  document: string
  country: Country
  email: string
  phone: string
  userName: string
  active: boolean
  approved: boolean
  roles: string[]
}
