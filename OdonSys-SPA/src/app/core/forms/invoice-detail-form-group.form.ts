import { FormArray, FormControl } from "@angular/forms";

export interface InvoiceDetailFormGroup {
  id: FormControl<string | null>
  procedure: FormControl<string | null>
  procedurePrice: FormControl<number | null>
  finalPrice: FormControl<number | null>
  toothIds?: FormArray<FormControl<string | null>>
  teethSelected: FormControl<string | null>;
}
