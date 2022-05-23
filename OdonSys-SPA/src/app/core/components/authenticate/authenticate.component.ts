import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
  public formGroup: FormGroup = new FormGroup({});
  public typeValue = { text: 'text', password: 'password', textMessage: 'Ocultar contraseña', passwordMessage: 'Mostrar contraseña' };
  public currentType = this.typeValue.text;
  public currentMessage = this.typeValue.textMessage;

  constructor(
    private readonly router: Router,
    private readonly authApiService: AuthApiService,
    private readonly alertService: AlertService,
  ) {}

  ngOnInit() {
    this.formGroup = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      type: new FormControl(this.typeValue.text),
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
