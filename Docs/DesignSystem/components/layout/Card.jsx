export function Card({ title, eyebrow, actions = null, footer = null, children, padded = true, collapsible = false, collapseKey, defaultOpen = true, className = '' }) {
  const [open, setOpen] = React.useState(() => {
    if (!collapsible || !collapseKey) return defaultOpen;
    const v = localStorage.getItem('nx-collapse-' + collapseKey);
    return v === null ? defaultOpen : v !== 'false';
  });
  const toggle = () => {
    if (!collapsible) return;
    setOpen(o => {
      const next = !o;
      if (collapseKey) localStorage.setItem('nx-collapse-' + collapseKey, String(next));
      return next;
    });
  };

  const ChevronSVG = () => (
    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2" strokeLinecap="round" strokeLinejoin="round" style={{ width: 16, height: 16, display: 'block', transition: 'transform 200ms ease', transform: open ? 'rotate(0deg)' : 'rotate(-90deg)' }}>
      <path d="m6 9 6 6 6-6" />
    </svg>
  );

  return (
    <section className={['nx-card', className].filter(Boolean).join(' ')}>
      {(title || actions) && (
        <header
          className="nx-card__head"
          onClick={collapsible ? toggle : undefined}
          style={collapsible ? { cursor: 'pointer', userSelect: 'none' } : undefined}
          role={collapsible ? 'button' : undefined}
          aria-expanded={collapsible ? String(open) : undefined}
        >
          <div>
            {eyebrow && <div className="nx-factbox__eyebrow">{eyebrow}</div>}
            {title && <div className="nx-card__title">{title}</div>}
          </div>
          <div className="nx-card__actions" onClick={e => e.stopPropagation()}>
            {actions}
          </div>
          {collapsible && (
            <span style={{ marginLeft: 'var(--space-2)', color: 'var(--text-faint)' }}>
              <ChevronSVG />
            </span>
          )}
        </header>
      )}
      <div className="nx-collapse__body" {...(!open && collapsible ? { 'data-closed': '' } : {})}>
        <div className="nx-collapse__inner">
          <div className="nx-card__body" style={padded ? undefined : { padding: 0 }}>{children}</div>
          {footer && <footer className="nx-card__foot">{footer}</footer>}
        </div>
      </div>
    </section>
  );
}
