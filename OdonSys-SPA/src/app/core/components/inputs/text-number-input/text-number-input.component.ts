import { Component, Input, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { InputType } from '../../../../core/constants/input-type';

@Component({
  selector: 'app-text-number-input',
  templateUrl: './text-number-input.component.html',
  styleUrls: ['./text-number-input.component.scss']
})
export class TextNumberComponent implements ControlValueAccessor {
  @Input() label: string = ''
  @Input() type: InputType = 'text'
  @Input() colClass: string = 'col-lg-8'
  @Input() numberWithSeparator: boolean = false

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this
  }

  writeValue(obj: any): void { }

  registerOnChange(fn: any): void { }

  registerOnTouched(fn: any): void { }

}
