export function sortBy<T>(items: T[], field: string, asc: boolean): T[] {
  return [...items].sort((a, b) => {
    const av = (a as any)[field] ?? '';
    const bv = (b as any)[field] ?? '';
    if (typeof av === 'number' && typeof bv === 'number') {
      return asc ? av - bv : bv - av;
    }
    const as = String(av).toLowerCase();
    const bs = String(bv).toLowerCase();
    return asc ? as.localeCompare(bs, 'es') : bs.localeCompare(as, 'es');
  });
}

export function sortIcon(sortField: string, field: string, sortAsc: boolean): string {
  return sortField === field ? (sortAsc ? '↑' : '↓') : '';
}
