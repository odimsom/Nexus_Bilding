export function KeyValue({ items = [], ruled = false, collapseKey, showMoreLabel = 'Show all fields', className = '' }) {
  // Separate primary (always shown) from secondary (hidden until toggled)
  const primary = items.filter(it => !it.secondary);
  const secondary = items.filter(it => it.secondary);
  const hasSecondary = secondary.length > 0;

  const storageKey = collapseKey ? 'nx-kv-more-' + collapseKey : null;
  const [showAll, setShowAll] = React.useState(() => {
    if (!storageKey) return false;
    return localStorage.getItem(storageKey) === 'true';
  });
  const toggleMore = () => setShowAll(v => {
    const next = !v;
    if (storageKey) localStorage.setItem(storageKey, String(next));
    return next;
  });

  const renderRow = (it, i) => (
    <React.Fragment key={i}>
      <div className="nx-kv__k">{it.key}</div>
      <div className={['nx-kv__v', it.mono ? 'nx-kv__v--mono' : ''].filter(Boolean).join(' ')}>
        {it.value ?? <span style={{ color: 'var(--text-faint)' }}>—</span>}
      </div>
    </React.Fragment>
  );

  return (
    <div className={['nx-kv', ruled ? 'nx-kv--ruled' : '', className].filter(Boolean).join(' ')}>
      {primary.map(renderRow)}
      {hasSecondary && (
        <>
          <div className="nx-kv__secondary" {...(!showAll ? { 'data-closed': '' } : {})}
               style={{ gridColumn: '1 / -1' }}>
            <div className="nx-kv__inner" style={{ display: 'grid', gridTemplateColumns: 'minmax(120px,38%) 1fr', gap: '1px var(--space-4)' }}>
              {secondary.map(renderRow)}
            </div>
          </div>
          <div style={{ gridColumn: '1 / -1', paddingTop: 'var(--space-1)' }}>
            <button
              type="button"
              className="nx-showmore"
              {...(showAll ? { 'data-open': '' } : {})}
              onClick={toggleMore}
            >
              <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2" strokeLinecap="round" strokeLinejoin="round">
                <path d="m6 9 6 6 6-6" />
              </svg>
              {showAll ? 'Show fewer fields' : showMoreLabel + ` (${secondary.length})`}
            </button>
          </div>
        </>
      )}
    </div>
  );
}
