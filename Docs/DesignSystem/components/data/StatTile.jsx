export function StatTile({ label, value, delta = null, trend = null, className = '' }) {
  return (
    <div className={['nx-stat', className].filter(Boolean).join(' ')}>
      <span className="nx-stat__label">{label}</span>
      <span className="nx-stat__value">{value}</span>
      {delta != null && (
        <span className={`nx-stat__delta nx-stat__delta--${trend === 'down' ? 'down' : 'up'}`}>
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.4" strokeLinecap="round" strokeLinejoin="round">
            {trend === 'down' ? <path d="M7 7l10 10M17 7v10H7" /> : <path d="M7 17 17 7M7 7h10v10" />}
          </svg>
          {delta}
        </span>
      )}
    </div>
  );
}
