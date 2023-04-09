import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomValidators } from '../../helpers/custom-validators';
import { EnumHandler } from '../../helpers/enum-handler';
import { RegisterUserRequest } from '../../models/users/api/register-user-request';
import { AuthApiService } from '../../services/api/auth-api.service';
import { AlertService } from '../../services/shared/alert.service';
import { Country } from '../../enums/country.enum';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.scss']
})
export class RegisterUserComponent implements OnInit {
  protected saving: boolean = false;
  protected formGroup = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    middleName: new FormControl<string | undefined | null>('', [Validators.maxLength(25)]),
    surname: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    secondSurname: new FormControl<string | undefined | null>('', [Validators.maxLength(25)]),
    document: new FormControl('', [Validators.required, Validators.maxLength(15), Validators.min(0)]),
    password: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    repeatPassword: new FormControl('', [Validators.required, Validators.maxLength(25)]),
    phone: new FormControl('', [Validators.required, Validators.maxLength(15), CustomValidators.checkPhoneValue()]),
    email: new FormControl('', [Validators.required, Validators.maxLength(40), Validators.email]),
    country: new FormControl<Country>(Country.Paraguay, [Validators.required])
  })
  protected countries: Map<string, string> = new Map<string, string>();

  constructor(
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly authApiService: AuthApiService
  ) {
    this.countries = EnumHandler.getCountries();
  }

  ngOnInit() {
    this.loadConfiguration();
  }

  protected register = (): void => {
    if (this.formGroup.invalid) { return; }
    this.saving = true;
    const request = new RegisterUserRequest(
      this.formGroup.value.name!,
      this.formGroup.value.surname!,
      this.formGroup.value.document!,
      this.formGroup.value.password!,
      this.formGroup.value.phone!,
      this.formGroup.value.email!,
      this.formGroup.value.country!,
      this.formGroup.value.secondSurname ?? undefined,
      this.formGroup.value.middleName ?? undefined
    )
    this.formGroup.disable()
    this.authApiService.register(request).subscribe({
      next: () => {
        this.formGroup.enable()
        this.alertService.showSuccess('Datos guardados.')
        this.router.navigate(['login'])
      },
      error: (error) => {
        this.formGroup.enable()
        this.saving = false
        throw error
      }
    });
  }

  protected close = (): void => {
    this.router.navigate(['login']);
  };

  private loadConfiguration = (): void => {
    this.formGroup.controls.repeatPassword.addValidators(this.checkRepeatPassword())
    this.formGroup.controls.password.valueChanges.subscribe({ next: () => { this.formGroup.controls.repeatPassword.updateValueAndValidity() }});
  }

  private checkRepeatPassword = (): ValidatorFn => {
    return (control: AbstractControl): ValidationErrors | null => {
      const repeatPassword = (control as FormControl<string | null>).value;
      if (!repeatPassword) { return null; }
      const isInvalid = !repeatPassword || this.formGroup.controls.password.value !== repeatPassword;
      return isInvalid ? { invalidRepeatPassword: isInvalid } : null;
    }
  }
}
