import { Pipe, PipeTransform } from '@angular/core';

/** Formats a Dominican phone number (10 digits) as (XXX) XXX-XXXX. Passes through other values unchanged. */
@Pipe({ name: 'phone', standalone: true, pure: true })
export class PhonePipe implements PipeTransform {
  transform(value: string | null | undefined): string {
    if (!value) return '—';
    const digits = value.replace(/\D/g, '');
    if (digits.length === 10) {
      return `(${digits.slice(0, 3)}) ${digits.slice(3, 6)}-${digits.slice(6)}`;
    }
    if (digits.length === 11 && digits[0] === '1') {
      return `(${digits.slice(1, 4)}) ${digits.slice(4, 7)}-${digits.slice(7)}`;
    }
    return value;
  }
}
