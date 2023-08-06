import { FormArray, FormControl } from "@angular/forms"

export interface ProcedureFormGroup {
  id: FormControl<string | null>
  name: FormControl<string | null>
  price: FormControl<number | null>
  finalPrice: FormControl<number | null>
  xRays: FormControl<boolean | null>
  toothIds?: FormArray<FormControl<string>>
  color?: FormControl<string | null>
  difficult?: FormControl<string | null>
}
