export interface CollapsibleProps {
  /** Section heading shown in the trigger bar. */
  label: React.ReactNode;
  /** localStorage key for persisting open/closed state — must be unique per view. */
  collapseKey?: string;
  /** Default expanded state when no persisted value exists. */
  defaultOpen?: boolean;
  /** Right-aligned actions inside the trigger (e.g. an edit icon). Stop propagation handled automatically. */
  actions?: React.ReactNode;
  children: React.ReactNode;
}

/**
 * Animated collapsible section with persistent state.
 * Use to let users hide/show record sections they don't currently need.
 * @startingPoint section="Layout" subtitle="Collapsible record sections" viewport="700x320"
 */
export function Collapsible(props: CollapsibleProps): JSX.Element;
