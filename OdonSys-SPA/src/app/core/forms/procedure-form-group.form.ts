import { FormArray, FormControl, FormGroup } from "@angular/forms"
import { ProcedureToothFormGroup } from "./procedure-tooth-form-group.form"

export interface ProcedureFormGroup {
  id: FormControl<string | null>
  name: FormControl<string | null>
  price: FormControl<number | null>
  finalPrice: FormControl<number | null>
  xRays: FormControl<boolean | null>
  toothIds?: FormArray<FormGroup<ProcedureToothFormGroup>>
  color?: FormControl<string | null>
  difficult?: FormControl<string | null>
}
