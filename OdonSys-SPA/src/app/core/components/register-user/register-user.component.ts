import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Country } from '../../enums/country.enum';
import { CustomValidators } from '../../helpers/custom-validators';
import { RegisterUserRequest } from '../../models/users/api/register-user-request';
import { AuthApiService } from '../../services/api/auth-api.service';
import { AlertService } from '../../services/shared/alert.service';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.scss']
})
export class RegisterUserComponent implements OnInit {
  public saving: boolean = false;
  public formGroup: FormGroup = new FormGroup({});
  public countries: Map<string, string> = new Map<string, string>();

  constructor(
    private readonly router: Router,
    private readonly alertService: AlertService,
    private readonly authApiService: AuthApiService
  ) {
    Object.keys(Country).map((key) => this.countries.set(key as string, Country[key as keyof typeof Country]));
  }

  ngOnInit() {
    this.loadConfiguration();
  }

  public register = (): void => {
    if (this.formGroup.invalid) { return; }
    this.saving = true;
    const request = this.formGroup.getRawValue() as RegisterUserRequest;
    this.formGroup.disable();
    this.authApiService.register(request).subscribe({
      next: () => {
        this.formGroup.enable();
        this.alertService.showSuccess('Datos guardados.');
        this.router.navigate(['login']);
      },
      error: (error) => {
        this.formGroup.enable();
        this.saving = false;
        throw new Error(error);
      }
    });
  };

  public close = (): void => {
    this.router.navigate(['login']);
  };

  private loadConfiguration = (): void => {
    this.formGroup = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(25)]),
      middleName: new FormControl('', [Validators.maxLength(25)]),
      lastName: new FormControl('', [Validators.required, Validators.maxLength(25)]),
      middleLastName: new FormControl('', [Validators.maxLength(25)]),
      document: new FormControl('', [Validators.required, Validators.maxLength(15), Validators.min(0)]),
      password: new FormControl('', [Validators.required, Validators.maxLength(25)]),
      repeatPassword: new FormControl('', [Validators.required, Validators.maxLength(25), this.checkRepeatPassword()]),
      phone: new FormControl('', [Validators.required, Validators.maxLength(15), CustomValidators.checkPhoneValue()]),
      email: new FormControl('', [Validators.required, Validators.maxLength(20), Validators.email]),
      country: new FormControl('', [Validators.required])
    });
    this.formGroup.controls.password.valueChanges.subscribe({ next: () => { this.formGroup.controls.repeatPassword.updateValueAndValidity() }});
  }

  private checkRepeatPassword = (): ValidatorFn => {
    return (control: AbstractControl): ValidationErrors | null => {
      const repeatPassword = (control as FormControl).value;
      if (!repeatPassword) { return null; }
      const isInvalid = !repeatPassword || this.formGroup.controls.password.value !== repeatPassword;
      return isInvalid ? { invalidRepeatPassword: isInvalid } : null;
    }
  }


}
