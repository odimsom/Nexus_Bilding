export interface FactBoxSection {
  /** Uppercase micro-label for the section. */
  label?: string;
  /** Section body — stats, key/values, or a list of drill links. */
  content: React.ReactNode;
}

export interface FactBoxProps {
  /** Small overline above the title. Default "Related". */
  eyebrow?: string;
  /** FactBox title (usually the focused record's name). */
  title?: React.ReactNode;
  sections?: FactBoxSection[];
  /** Extra content rendered as a trailing section. */
  children?: React.ReactNode;
}

/**
 * The signature Nexus pattern: a right-rail panel showing everything related
 * to the focused record — statistics, links to its sales, ledger, attachments —
 * so any entity is reachable from any other.
 * @startingPoint section="Data" subtitle="FactBox related-info rail" viewport="340x420"
 */
export function FactBox(props: FactBoxProps): JSX.Element;
