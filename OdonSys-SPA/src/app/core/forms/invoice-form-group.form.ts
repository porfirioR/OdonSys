import { FormArray, FormControl, FormGroup } from "@angular/forms";
import { InvoiceDetailFormGroup } from "./invoice-detail-form-group.form";

export interface InvoiceFormGroup {
  id: FormControl<string | null>
  iva10: FormControl<number | null>
  totalIva: FormControl<number | null>
  subTotal: FormControl<number | null>
  total: FormControl<number | null>
  invoiceDetails?: FormArray<FormGroup<InvoiceDetailFormGroup>>
}
