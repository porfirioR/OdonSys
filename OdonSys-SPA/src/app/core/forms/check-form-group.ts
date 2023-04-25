import { FormControl } from "@angular/forms"

export interface CheckFormGroup {
  name: FormControl<string | null>
  code: FormControl<string | null>
  value: FormControl<boolean | null>
}
