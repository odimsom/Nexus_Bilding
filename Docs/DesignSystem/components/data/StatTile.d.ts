export interface StatTileProps {
  /** Uppercase metric label. */
  label: string;
  /** Primary figure (string, pre-formatted — mono/tabular is applied). */
  value: React.ReactNode;
  /** Optional change indicator, e.g. "+12.4%". */
  delta?: React.ReactNode;
  /** Direction of the delta arrow & color. */
  trend?: 'up' | 'down';
}

/**
 * KPI tile for dashboards & record statistics.
 * @startingPoint section="Data" subtitle="KPI stat tiles" viewport="700x150"
 */
export function StatTile(props: StatTileProps): JSX.Element;
