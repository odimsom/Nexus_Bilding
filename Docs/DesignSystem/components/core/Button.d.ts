export interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  /** Visual style. `primary` = ledger-emerald CTA, `secondary` = the bordered workhorse, `ghost` = chromeless, `subtle` = tinted brand, `danger` = destructive. */
  variant?: 'primary' | 'secondary' | 'ghost' | 'subtle' | 'danger';
  /** Control height. Default `md` (34px). */
  size?: 'sm' | 'md' | 'lg';
  /** Stretch to full container width. */
  block?: boolean;
  /** Icon node rendered before the label (16px Lucide recommended). */
  leftIcon?: React.ReactNode;
  /** Icon node rendered after the label. */
  rightIcon?: React.ReactNode;
}

/**
 * Primary action element across Nexus Billing. Exactly one `primary` button
 * per view region; `secondary` for everything else.
 * @startingPoint section="Core" subtitle="Buttons — primary, secondary, ghost, danger" viewport="700x180"
 */
export function Button(props: ButtonProps): JSX.Element;
