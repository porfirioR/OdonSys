import { FormArray, FormControl } from "@angular/forms";
import { PermissionFormGroup } from "./permission-form-group.form";
import { FormGroup } from "@angular/forms";

export interface SubGroupPermissions {
  subGroup: FormControl,
  permissions: FormArray<FormGroup<PermissionFormGroup>>
}
