import { FormControl } from "@angular/forms"
import { Country } from "../enums/country.enum"

export interface UserFormGroup {
  name: FormControl<string | null>
  middleName: FormControl<string | null>
  surname: FormControl<string | null>
  secondSurname: FormControl<string | null>
  document: FormControl<string | null>
  ruc: FormControl<string | number | null>
  phone: FormControl<string | null>
  country: FormControl<Country | null>
  email: FormControl<string | null>
}
