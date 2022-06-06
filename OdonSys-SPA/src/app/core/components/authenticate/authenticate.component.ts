import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { LoginRequest } from '../../models/users/api/login-request';
import { AuthApiService } from '../../services/api/auth-api.service';
import { AlertService } from '../../services/shared/alert.service';

@Component({
  selector: 'app-authenticate',
  templateUrl: './authenticate.component.html',
  styleUrls: ['./authenticate.component.scss'],
})
export class AuthenticateComponent implements OnInit {
  public formGroup: UntypedFormGroup = new UntypedFormGroup({});
  public typeValue = { text: 'text', password: 'password', textMessage: 'Ocultar contraseña', passwordMessage: 'Mostrar contraseña' };
  public currentType = this.typeValue.password;
  public currentMessage = this.typeValue.passwordMessage;

  constructor(
    private readonly router: Router,
    private readonly authApiService: AuthApiService,
    private readonly alertService: AlertService,
  ) {}

  ngOnInit() {
    this.formGroup = new UntypedFormGroup({
      email: new UntypedFormControl('', [Validators.required, Validators.email]),
      password: new UntypedFormControl('', [Validators.required]),
      type: new UntypedFormControl(false),
    });
    this.formGroup.controls.type.valueChanges.subscribe((x: Boolean) => {
      this.currentType = x ? this.typeValue.text : this.typeValue.password;
      this.currentMessage = x ? this.typeValue.textMessage : this.typeValue.passwordMessage;
    });
  }

  public login = () => {
    const request = this.formGroup.getRawValue() as LoginRequest;
    this.formGroup.disable();
    this.authApiService.login(request).pipe(tap(x => {
      this.formGroup.enable();
      this.alertService.showSuccess('Bienvenido');
      this.router.navigate(['']);
    }), catchError(err => {
      this.formGroup.enable();
      return throwError(err);
    })).subscribe()
  };
}
