export interface SelectOption { value: string; label: string; }

export interface SelectProps extends React.SelectHTMLAttributes<HTMLSelectElement> {
  label?: string;
  hint?: string;
  error?: string;
  required?: boolean;
  /** Options as strings or {value,label}. Alternatively pass <option> children. */
  options?: (string | SelectOption)[];
  /** Disabled leading placeholder option. */
  placeholder?: string;
}

/** Native select styled to match inputs, with a custom chevron. */
export function Select(props: SelectProps): JSX.Element;
