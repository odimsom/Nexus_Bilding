import { Pipe, PipeTransform } from '@angular/core';

/** Formats a Dominican RNC (9 digits) as XXX-XXXXX-X. Passes through other values unchanged. */
@Pipe({ name: 'rnc', standalone: true, pure: true })
export class RncPipe implements PipeTransform {
  transform(value: string | null | undefined): string {
    if (!value) return '—';
    const digits = value.replace(/\D/g, '');
    if (digits.length !== 9) return value;
    return `${digits.slice(0, 3)}-${digits.slice(3, 8)}-${digits.slice(8)}`;
  }
}
