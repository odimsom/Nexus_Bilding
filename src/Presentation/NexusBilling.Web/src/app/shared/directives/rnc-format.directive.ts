import { Directive, ElementRef, HostListener } from '@angular/core';
import { NgControl } from '@angular/forms';

/** Formats a Dominican RNC (9 digits) as XXX-XXXXX-X while the user types. */
@Directive({
  selector: '[nxRnc]',
  standalone: true,
})
export class RncFormatDirective {
  constructor(private el: ElementRef<HTMLInputElement>, private ctrl: NgControl) {}

  @HostListener('input')
  onInput(): void {
    const digits = this.el.nativeElement.value.replace(/\D/g, '').slice(0, 9);
    let formatted = digits;
    if (digits.length > 8) {
      formatted = `${digits.slice(0, 3)}-${digits.slice(3, 8)}-${digits.slice(8)}`;
    } else if (digits.length > 3) {
      formatted = `${digits.slice(0, 3)}-${digits.slice(3)}`;
    }
    this.el.nativeElement.value = formatted;
    this.ctrl.control?.setValue(formatted, { emitEvent: false });
  }
}
