import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { SelectModel } from '../../../../core/models/view/select-model';

@Component({
  selector: 'app-select-input',
  templateUrl: './select-input.component.html',
  styleUrls: ['./select-input.component.scss']
})
export class SelectInputComponent implements ControlValueAccessor {
  @Input() label: string = ''
  @Input() id: string = ''
  @Input() colClass: string = 'col-lg-8'
  @Input() options: SelectModel[] = []

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this
  }

  writeValue(obj: any): void { }

  registerOnChange(fn: any): void { }

  registerOnTouched(fn: any): void { }

}
