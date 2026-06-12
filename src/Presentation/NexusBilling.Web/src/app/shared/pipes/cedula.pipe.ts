import { Pipe, PipeTransform } from '@angular/core';

/** Formats a Dominican cédula (11 digits) as XXX-XXXXXXX-X. Passes through other values unchanged. */
@Pipe({ name: 'cedula', standalone: true, pure: true })
export class CedulaPipe implements PipeTransform {
  transform(value: string | null | undefined): string {
    if (!value) return '—';
    const digits = value.replace(/\D/g, '');
    if (digits.length !== 11) return value;
    return `${digits.slice(0, 3)}-${digits.slice(3, 10)}-${digits.slice(10)}`;
  }
}
