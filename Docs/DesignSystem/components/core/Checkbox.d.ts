export interface CheckboxProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label?: string;
  /** Render as a radio (single-choice) instead of a checkbox. */
  radio?: boolean;
}

/** Checkbox / radio with the emerald checked state. */
export function Checkbox(props: CheckboxProps): JSX.Element;
