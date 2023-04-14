import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthApiModel } from '../../models/users/api/auth-api-model';
import { LoginRequest } from '../../models/users/api/login-request';
import { AuthApiService } from '../../services/api/auth-api.service';
import { AlertService } from '../../services/shared/alert.service';
import { UserInfoService } from '../../services/shared/user-info.service';
import { selectRole } from '../../store/roles/roles.selectors';

@Component({
  selector: 'app-authenticate',
  templateUrl: './authenticate.component.html',
  styleUrls: ['./authenticate.component.scss'],
})
export class AuthenticateComponent implements OnInit {
  public formGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    type: new FormControl<boolean>(false, { nonNullable: true }),
  })
  public typeValue = { text: 'text', password: 'password', textMessage: 'Ocultar contraseña', passwordMessage: 'Mostrar contraseña' };
  public currentType = this.typeValue.password;
  public currentMessage = this.typeValue.passwordMessage;
  public load = false;

  constructor(
    private readonly router: Router,
    private readonly authApiService: AuthApiService,
    private readonly alertService: AlertService,
    private readonly userInfoService: UserInfoService,
  ) {}

  ngOnInit() {
    // this.userInfoService.clearAll();
    this.load = true;
    this.formGroup.controls.type.valueChanges.subscribe({
      next: (x: Boolean) => {
        this.currentType = x ? this.typeValue.text : this.typeValue.password;
        this.currentMessage = x ? this.typeValue.textMessage : this.typeValue.passwordMessage;
      }
    })
  }

  public login = (): void => {
    if (this.formGroup.invalid) { return }
    this.load = false
    const request = new LoginRequest(this.formGroup.value.email!, this.formGroup.value.password!)
    this.formGroup.disable()
    this.authApiService.login(request).subscribe({
      next: (response: AuthApiModel) => {
        this.formGroup.enable()
        this.alertService.showSuccess(`Bienvenido ${response.user.userName}`)
        this.router.navigate([''])
      }, error: (e) => {
        this.load = true
        this.formGroup.enable()
        throw e
      }
    })
  }
}
