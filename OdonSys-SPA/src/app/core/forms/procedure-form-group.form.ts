import { FormControl } from "@angular/forms"

export interface ProcedureFormGroup {
  id: FormControl<string | null>
  name: FormControl<string | null>
  price: FormControl<number | null>
  finalPrice: FormControl<number | null>
  xRays: FormControl<boolean | null>
}
