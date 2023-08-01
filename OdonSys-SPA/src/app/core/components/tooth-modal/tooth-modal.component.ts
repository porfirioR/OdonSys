import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ProcedureFormGroup } from '../../forms/procedure-form-group.form';

@Component({
  selector: 'app-tooth-modal',
  templateUrl: './tooth-modal.component.html',
  styleUrls: ['./tooth-modal.component.scss']
})
export class ToothModalComponent implements OnInit {
  procedure: FormGroup<ProcedureFormGroup>
  constructor() { }

  ngOnInit() {
  }

}
