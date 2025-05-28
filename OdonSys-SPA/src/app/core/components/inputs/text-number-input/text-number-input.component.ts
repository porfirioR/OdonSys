import { AfterViewInit, Component, ElementRef, Input, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { InputType } from '../../../../core/constants/input-type';

@Component({
  selector: 'app-text-number-input',
  templateUrl: './text-number-input.component.html',
  styleUrls: ['./text-number-input.component.scss']
})
export class TextNumberComponent implements ControlValueAccessor, AfterViewInit {
  @ViewChild("numberInput") numberInput?: ElementRef
  @ViewChild("principalTextNumberInput") principalTextNumberInput?: ElementRef
  @Input() label = ''
  @Input() id = ''
  @Input() type: InputType = 'text'
  @Input() colClass = 'col-lg-8'
  @Input() numberWithSeparator = false
  @Input() showLabel = true
  @Input() autofocus = false

  ngAfterViewInit() {
    if (this.autofocus) {
      this.numberInput?.nativeElement.focus()
      this.principalTextNumberInput?.nativeElement.focus()
    }
  }

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this
  }

  writeValue(obj: any): void { }

  registerOnChange(fn: any): void { }

  registerOnTouched(fn: any): void { }

}
