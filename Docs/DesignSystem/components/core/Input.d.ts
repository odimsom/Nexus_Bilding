export interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  /** Field label rendered above the control. */
  label?: string;
  /** Helper text below the control. */
  hint?: string;
  /** Error message — also sets the invalid state. Overrides `hint`. */
  error?: string;
  required?: boolean;
  /** Leading adornment node (icon, currency symbol). */
  adornment?: React.ReactNode;
  /** Use the monospace/tabular font (amounts, codes, document numbers). */
  mono?: boolean;
}

/**
 * Labeled text input — the foundation of every Nexus Billing form & filter.
 * @startingPoint section="Forms" subtitle="Inputs, selects, toggles" viewport="700x260"
 */
export function Input(props: InputProps): JSX.Element;
