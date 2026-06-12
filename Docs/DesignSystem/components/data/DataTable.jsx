export function DataTable({ columns = [], rows = [], zebra = false, selectedId = null, getRowId, onRowClick, className = '' }) {
  const rowId = getRowId || ((r, i) => r.id ?? i);
  return (
    <table className={['nx-table', zebra ? 'nx-table--zebra' : '', className].filter(Boolean).join(' ')}>
      <thead>
        <tr>
          {columns.map(col => (
            <th key={col.key} className={col.align === 'right' ? 'nx-th--num' : ''} style={col.width ? { width: col.width } : undefined}>
              {col.header}
            </th>
          ))}
        </tr>
      </thead>
      <tbody>
        {rows.map((row, i) => {
          const id = rowId(row, i);
          return (
            <tr key={id} aria-selected={selectedId != null && id === selectedId ? 'true' : undefined}
                onClick={onRowClick ? () => onRowClick(row, id) : undefined}
                style={onRowClick ? { cursor: 'pointer' } : undefined}>
              {columns.map(col => {
                const variant = col.variant === 'num' ? 'nx-td--num' : col.variant === 'doc' ? 'nx-td--doc' : '';
                const content = col.render ? col.render(row[col.key], row) : row[col.key];
                return <td key={col.key} className={variant}>{content}</td>;
              })}
            </tr>
          );
        })}
      </tbody>
    </table>
  );
}
