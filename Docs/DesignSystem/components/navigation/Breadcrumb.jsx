export function Breadcrumb({ items = [], className = '' }) {
  const Sep = () => (
    <span className="nx-crumbs__sep" aria-hidden="true">
      <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="m9 18 6-6-6-6" /></svg>
    </span>
  );
  return (
    <nav className={['nx-crumbs', className].filter(Boolean).join(' ')} aria-label="Breadcrumb">
      {items.map((it, i) => {
        const last = i === items.length - 1;
        return (
          <React.Fragment key={i}>
            {last
              ? <span className="nx-crumb nx-crumb--current" aria-current="page">{it.label}</span>
              : <a className="nx-crumb" href={it.href || '#'} onClick={it.onClick}>{it.label}</a>}
            {!last && <Sep />}
          </React.Fragment>
        );
      })}
    </nav>
  );
}
