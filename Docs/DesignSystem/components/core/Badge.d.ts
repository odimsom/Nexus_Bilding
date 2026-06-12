export interface BadgeProps {
  /** Semantic state → tint. */
  status?: 'success' | 'info' | 'warn' | 'danger' | 'neutral';
  /** Show a leading status dot. */
  dot?: boolean;
  /** Bordered, transparent style. */
  outline?: boolean;
  /** Solid ledger-emerald style. */
  solid?: boolean;
  children: React.ReactNode;
}

/**
 * Compact status indicator for document/record states (Posted, Open, Overdue…).
 * @startingPoint section="Core" subtitle="Status badges & tags" viewport="700x140"
 */
export function Badge(props: BadgeProps): JSX.Element;
