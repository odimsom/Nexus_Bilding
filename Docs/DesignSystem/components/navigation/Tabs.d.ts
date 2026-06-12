export interface TabItem {
  id: string;
  label: React.ReactNode;
  /** Optional count pill (e.g. number of open documents). */
  count?: number;
}

export interface TabsProps {
  /** Tabs as strings or {id,label,count}. */
  tabs: (string | TabItem)[];
  /** Selected tab id. */
  value: string;
  onChange?: (id: string) => void;
}

/**
 * Underline tabs for switching views within a record or list (e.g. All / Open / Posted).
 * @startingPoint section="Navigation" subtitle="Tabs & breadcrumb" viewport="700x120"
 */
export function Tabs(props: TabsProps): JSX.Element;
