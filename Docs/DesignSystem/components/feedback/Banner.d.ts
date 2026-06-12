export interface BannerProps {
  status?: 'info' | 'success' | 'warn' | 'danger';
  /** Bold lead line. */
  title?: React.ReactNode;
  /** Override the default status icon. */
  icon?: React.ReactNode;
  /** Show a dismiss button and handle it. */
  onDismiss?: () => void;
  /** Supporting message. */
  children?: React.ReactNode;
}

/**
 * Inline contextual message — posting results, validation, reminders.
 * @startingPoint section="Feedback" subtitle="Banners, empty states" viewport="700x220"
 */
export function Banner(props: BannerProps): JSX.Element;
