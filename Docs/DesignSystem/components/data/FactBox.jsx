export function FactBox({ eyebrow = 'Related', title, sections = [], children, className = '' }) {
  return (
    <aside className={['nx-factbox', className].filter(Boolean).join(' ')}>
      <div className="nx-factbox__head">
        <div className="nx-factbox__eyebrow">{eyebrow}</div>
        {title && <div className="nx-factbox__title">{title}</div>}
      </div>
      {sections.map((s, i) => (
        <FactBoxSection key={i} section={s} index={i} baseKey={title || eyebrow} />
      ))}
      {children && <div className="nx-factbox__section">{children}</div>}
    </aside>
  );
}

function FactBoxSection({ section, index, baseKey }) {
  const collapseKey = section.collapseKey || ('fb-' + String(baseKey || '').replace(/\s+/g, '-').toLowerCase() + '-' + index);
  const defaultOpen = section.defaultOpen !== false;
  const [open, setOpen] = React.useState(() => {
    const v = localStorage.getItem('nx-collapse-' + collapseKey);
    return v === null ? defaultOpen : v !== 'false';
  });
  const toggle = () => setOpen(o => {
    const next = !o;
    localStorage.setItem('nx-collapse-' + collapseKey, String(next));
    return next;
  });

  if (!section.label) {
    // Non-collapsible plain section
    return <div className="nx-factbox__section">{section.content}</div>;
  }

  return (
    <div className="nx-factbox__section" style={{ padding: 0 }}>
      <button
        type="button"
        onClick={toggle}
        aria-expanded={open ? 'true' : 'false'}
        style={{
          display: 'flex', alignItems: 'center', justifyContent: 'space-between',
          width: '100%', background: 'none', border: 'none', cursor: 'pointer',
          padding: 'var(--space-3) var(--space-4) var(--space-2)', font: 'inherit',
        }}
      >
        <span className="nx-factbox__sectionlabel" style={{ marginBottom: 0 }}>{section.label}</span>
        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2"
             strokeLinecap="round" strokeLinejoin="round"
             style={{ width: 13, height: 13, color: 'var(--text-faint)', flexShrink: 0,
                      transition: 'transform 200ms ease', transform: open ? 'rotate(0deg)' : 'rotate(-90deg)' }}>
          <path d="m6 9 6 6 6-6" />
        </svg>
      </button>
      <div className="nx-collapse__body" {...(!open ? { 'data-closed': '' } : {})}>
        <div className="nx-collapse__inner" style={{ padding: '0 var(--space-4) var(--space-3)' }}>
          {section.content}
        </div>
      </div>
    </div>
  );
}
