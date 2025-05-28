import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-check-input',
  templateUrl: './check-input.component.html',
  styleUrls: ['./check-input.component.scss']
})
export class CheckInputComponent implements ControlValueAccessor {
  @Input() label = ''
  @Input() colClass = 'col-lg-6'
  @Input() id: string

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void { }

  registerOnChange(fn: any): void { }

  registerOnTouched(fn: any): void { }

}
