import { FormControl } from "@angular/forms";

export interface PermissionFormGroup {
  name: FormControl<string | null>
  code: FormControl<string | null>
  group: FormControl<string | null>
  subGroup: FormControl<string | null>
  value: FormControl<boolean | null>
}
