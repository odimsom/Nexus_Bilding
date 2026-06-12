export function Tabs({ tabs = [], value, onChange, className = '' }) {
  return (
    <div className={['nx-tabs', className].filter(Boolean).join(' ')} role="tablist">
      {tabs.map(t => {
        const id = typeof t === 'string' ? t : t.id;
        const label = typeof t === 'string' ? t : t.label;
        const count = typeof t === 'string' ? undefined : t.count;
        const selected = id === value;
        return (
          <button key={id} role="tab" type="button" aria-selected={selected ? 'true' : 'false'}
                  className="nx-tab" onClick={() => onChange && onChange(id)}>
            {label}
            {count != null && <span className="nx-tab__count">{count}</span>}
          </button>
        );
      })}
    </div>
  );
}
