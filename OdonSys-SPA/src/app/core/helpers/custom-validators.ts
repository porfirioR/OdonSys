import { AbstractControl, FormControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export class CustomValidators {

  public static checkPhoneValue = (): ValidatorFn => {
    return (control: AbstractControl): ValidationErrors | null => {
      const phone = (control as FormControl).value;
      if (!phone) { return null; }
      const reg = new RegExp(/^[+]{0,1}[0-9]+$/g);
      const isInvalid = !reg.test(phone);
      return isInvalid ? { invalidPhone: isInvalid } : null;
    }
  }
}
