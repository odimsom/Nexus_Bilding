export interface IconButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  /** Accessible name — required since there is no visible label. Also shown as a native tooltip. */
  label: string;
  size?: 'sm' | 'md';
  /** The icon node (Lucide glyph). */
  children: React.ReactNode;
}

/** Square, chromeless-until-hover button for toolbar icons & row actions. */
export function IconButton(props: IconButtonProps): JSX.Element;
