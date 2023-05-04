import { FormArray, FormControl, FormGroup } from "@angular/forms";
import { Country } from "../enums/country.enum";
import { PermissionModel } from "../models/view/permission-model";
import { EnumHandler } from "./enum-handler";
import { PermissionFormGroup } from "../forms/permission-form-group.form";
import { PermissionSubGroup } from "../enums/permission-sub-group";
import { SubGroupPermissions } from "../forms/sub-group-permissions.form";

export class MethodHandler {
  private static calculateParaguayanRuc = (document: string) => {
    let checkDigit = 0;
    if(document && document.length >= 6 && !isNaN(+document)) {
      let multiplier = 2;
      const module = 11;
      const reverseDocument = document.split('').reverse();
      let result = 0;
      reverseDocument.forEach(value => {
        result += multiplier * +value;
        multiplier++;
        if (multiplier > module) {
          multiplier = 2;
        }
      });
      const rest = result % module;
      checkDigit = rest > 1 ? module - rest : 0;
    }
    return checkDigit
  }

  public static calculateCheckDigit = (document: string, country: Country): number => {
    switch (country) {
      case Country.Paraguay: return MethodHandler.calculateParaguayanRuc(document);
      case Country.Argentina:
      default: return 0;
    }
  }

  public static setSubPermissions = (allPermissions: PermissionModel[], rolePermissions: string[], subGroupPermissionsFormArray: FormArray<FormGroup<SubGroupPermissions>>) => {
    const subGroup = [...new Set(allPermissions.map(x => x.subGroup))].sort((a, b) => a.localeCompare(b))
    subGroup.forEach(x => {
      const permissionsFormGroups = allPermissions.map(x => 
        new FormGroup<PermissionFormGroup>({
          name: new FormControl(x.name),
          code: new FormControl(x.code),
          group: new FormControl(x.group),
          subGroup: new FormControl(x.subGroup),
          value: new FormControl(rolePermissions.includes(x.code))
        })
      )
      const permissions = permissionsFormGroups.filter(formGroup => formGroup.value.subGroup! === x)
      const subGroupPermissions = new FormGroup({
        subGroup: new FormControl(EnumHandler.getValueByKey(PermissionSubGroup, x), { nonNullable: true}),
        permissions: new FormArray(permissions)
      })
      subGroupPermissionsFormArray!.push(subGroupPermissions)
    })
  }
}
