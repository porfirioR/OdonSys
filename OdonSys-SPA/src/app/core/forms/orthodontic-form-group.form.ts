import { FormControl } from "@angular/forms"

export interface OrthodonticFormGroup {
  description: FormControl<string | null>
  clientId: FormControl<string | null>
  date: FormControl<Date | null>
}
