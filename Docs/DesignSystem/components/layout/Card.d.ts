export interface CardProps {
  title?: React.ReactNode;
  /** Small overline above the title. */
  eyebrow?: React.ReactNode;
  /** Right-aligned header actions (buttons, icon buttons). */
  actions?: React.ReactNode;
  /** Footer band content. */
  footer?: React.ReactNode;
  /** Pad the body. Set false for flush tables. */
  padded?: boolean;
  children: React.ReactNode;
}

/**
 * Bordered container with optional header/footer. Wraps record sections,
 * tables and panels. Resting surfaces use borders, not shadow.
 * @startingPoint section="Layout" subtitle="Card container" viewport="700x260"
 */
export function Card(props: CardProps): JSX.Element;
