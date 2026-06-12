export interface SwitchProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label?: string;
}

/** On/off toggle for settings & quick filters (e.g. “Show open only”). */
export function Switch(props: SwitchProps): JSX.Element;
