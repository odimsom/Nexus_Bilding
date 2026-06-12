export interface EmptyStateProps {
  /** Icon node (Lucide), shown in a soft tile. */
  icon?: React.ReactNode;
  title?: React.ReactNode;
  /** Explanatory copy. */
  children?: React.ReactNode;
  /** Primary action (usually a Button). */
  action?: React.ReactNode;
}

/** Centered placeholder for empty lists, no results, or unstarted records. */
export function EmptyState(props: EmptyStateProps): JSX.Element;
