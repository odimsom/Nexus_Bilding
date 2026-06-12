export interface DataColumn {
  /** Key into the row object. */
  key: string;
  /** Column header label. */
  header: React.ReactNode;
  /** Cell treatment: `doc` = mono document number, `num` = right-aligned tabular. */
  variant?: 'doc' | 'num' | 'text';
  /** Header alignment; use `right` for numeric columns. */
  align?: 'left' | 'right';
  /** Fixed width (e.g. "160px"). */
  width?: string;
  /** Custom cell renderer — return a node (badges, drill links…). */
  render?: (value: any, row: any) => React.ReactNode;
}

export interface DataTableProps {
  columns: DataColumn[];
  rows: any[];
  /** Alternating row shading. */
  zebra?: boolean;
  /** Currently selected row id (sets the emerald selected state). */
  selectedId?: string | number | null;
  /** Derive a row id; defaults to row.id or index. */
  getRowId?: (row: any, index: number) => string | number;
  /** Row click handler — wire to drill-through navigation. */
  onRowClick?: (row: any, id: string | number) => void;
}

/**
 * The primary list surface of the ERP — sticky header, hover & selected rows,
 * mono document numbers and right-aligned tabular amounts.
 * @startingPoint section="Data" subtitle="Data table / document list" viewport="700x320"
 */
export function DataTable(props: DataTableProps): JSX.Element;
