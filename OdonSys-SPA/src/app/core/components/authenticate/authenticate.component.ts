import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-authenticate',
  templateUrl: './authenticate.component.html',
  styleUrls: ['./authenticate.component.scss']
})
export class AuthenticateComponent implements OnInit {
  public formGroup: FormGroup = new FormGroup({});

  constructor() { }

  ngOnInit() {
    

  }

}
