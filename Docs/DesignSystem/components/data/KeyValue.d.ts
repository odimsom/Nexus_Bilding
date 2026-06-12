export interface KeyValueItem {
  key: React.ReactNode;
  value: React.ReactNode;
  /** Render the value in mono/tabular (amounts, numbers, codes). */
  mono?: boolean;
  /**
   * Mark this field as secondary — hidden by default, revealed via "Show all fields".
   * Use for infrequently-needed detail (address, GL group codes, dimensions, etc.).
   */
  secondary?: boolean;
}

export interface KeyValueProps {
  items: KeyValueItem[];
  /** Add a divider rule under each row. */
  ruled?: boolean;
  /**
   * localStorage key for persisting the show-more state.
   * Required if the component appears more than once on a page.
   */
  collapseKey?: string;
  /** Override the "Show all fields" label. */
  showMoreLabel?: string;
}

/** Two-column field list for record detail panes. Secondary fields are hidden until revealed. */
export function KeyValue(props: KeyValueProps): JSX.Element;
