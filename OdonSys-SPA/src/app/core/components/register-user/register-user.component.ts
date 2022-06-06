import { Component, OnInit } from '@angular/core';
import { AbstractControl, UntypedFormControl, UntypedFormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { Country } from '../../enums/country.enum';
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
  public formGroup: UntypedFormGroup = new UntypedFormGroup({});
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

  public register = () => {
    this.saving = true;
    const request = this.formGroup.getRawValue() as RegisterUserRequest;
    this.authApiService
      .register(request)
      .pipe(
        tap(() => {
          this.alertService.showSuccess('Datos guardados.');
          this.close();
        }),
        catchError((e) => {
          this.saving = false;
          return throwError(e);
        })
      )
      .subscribe();
  };

  public close = () => {
    this.router.navigate(['login']);
  };

  private loadConfiguration = () => {
    this.formGroup = new UntypedFormGroup({
      name: new UntypedFormControl('', [Validators.required, Validators.maxLength(25)]),
      middleName: new UntypedFormControl('', [Validators.maxLength(25)]),
      lastName: new UntypedFormControl('', [Validators.required, Validators.maxLength(25)]),
      middleLastName: new UntypedFormControl('', [Validators.maxLength(25)]),
      document: new UntypedFormControl('', [Validators.required, Validators.maxLength(15)]),
      password: new UntypedFormControl('', [Validators.required, Validators.maxLength(25)]),
      repeatPassword: new UntypedFormControl('', [Validators.required, Validators.maxLength(25), this.checkRepeatPassword()]),
      phone: new UntypedFormControl('', [Validators.required, Validators.maxLength(15)]),
      email: new UntypedFormControl('', [Validators.required, Validators.maxLength(20), Validators.email]),
      country: new UntypedFormControl('', [Validators.required]),
    });
  }

  private checkRepeatPassword = (): ValidatorFn => {
    return (control: AbstractControl): ValidationErrors | null => {
      const repeatPassword = (control as UntypedFormControl).value;
      const isInvalid = !repeatPassword || this.formGroup.controls.password.value !== repeatPassword;
      return isInvalid ? { invalidRepeatPassword: isInvalid } : null;
    }
  }

}
