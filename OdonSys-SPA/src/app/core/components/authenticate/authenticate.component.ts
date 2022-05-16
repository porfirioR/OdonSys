import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

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

  constructor() {}

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

  public login = () => {};
}
