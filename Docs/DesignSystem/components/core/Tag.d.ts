export interface TagProps {
  /** When provided, renders a removable “×”. */
  onRemove?: () => void;
  children: React.ReactNode;
}

/** Neutral chip for active filters and multi-value fields. */
export function Tag(props: TagProps): JSX.Element;
