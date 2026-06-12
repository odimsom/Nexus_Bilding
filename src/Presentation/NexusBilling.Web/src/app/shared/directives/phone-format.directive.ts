import { Directive, ElementRef, HostListener } from '@angular/core';
import { NgControl } from '@angular/forms';

/** Formats a Dominican phone number as (XXX) XXX-XXXX while the user types. */
@Directive({
  selector: '[nxPhone]',
  standalone: true,
})
export class PhoneFormatDirective {
  constructor(private el: ElementRef<HTMLInputElement>, private ctrl: NgControl) {}

  @HostListener('input')
  onInput(): void {
    const digits = this.el.nativeElement.value.replace(/\D/g, '').slice(0, 10);
    let formatted = digits;
    if (digits.length > 6) {
      formatted = `(${digits.slice(0, 3)}) ${digits.slice(3, 6)}-${digits.slice(6)}`;
    } else if (digits.length > 3) {
      formatted = `(${digits.slice(0, 3)}) ${digits.slice(3)}`;
    } else if (digits.length > 0) {
      formatted = `(${digits}`;
    }
    this.el.nativeElement.value = formatted;
    this.ctrl.control?.setValue(formatted, { emitEvent: false });
  }
}
