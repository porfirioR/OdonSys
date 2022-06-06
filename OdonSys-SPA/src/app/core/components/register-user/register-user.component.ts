import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { throwError } from 'rxjs/internal/observable/throwError';
import { catchError } from 'rxjs/internal/operators/catchError';
import { tap } from 'rxjs/internal/operators/tap';
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
    this.router.navigate(['']);
  };

  private loadConfiguration = () => {
    this.formGroup = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.maxLength(25)]),
      middleName: new FormControl('', [Validators.maxLength(25)]),
      lastName: new FormControl('', [Validators.required, Validators.maxLength(25)]),
      middleLastName: new FormControl('', [Validators.maxLength(25)]),
      document: new FormControl('', [Validators.required, Validators.maxLength(15)]),
      password: new FormControl('', [Validators.required, Validators.maxLength(25)]),
      repeatPassword: new FormControl('', [Validators.required, Validators.maxLength(25), this.checkRepeatPassword()]),
      phone: new FormControl('', [Validators.required, Validators.maxLength(15)]),
      email: new FormControl('', [Validators.required, Validators.maxLength(20), Validators.email]),
      country: new FormControl('', [Validators.required]),
    });
  }

  private checkRepeatPassword = (): ValidatorFn => {
    return (control: AbstractControl): ValidationErrors | null => {
      const repeatPassword = (control as FormControl).value;
      const isInvalid = this.formGroup.controls.password.value !== repeatPassword;
      return isInvalid ? { invalidRepeatPassword: isInvalid } : null;
    }
  }

}
