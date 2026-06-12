// Shared collapse hook — persists open/closed state to localStorage.
// key: unique string that identifies this collapsible; defaultOpen: boolean.
function useCollapsible(key, defaultOpen = true) {
  const storageKey = key ? 'nx-collapse-' + key : null;
  const [open, setOpen] = React.useState(() => {
    if (!storageKey) return defaultOpen;
    const v = localStorage.getItem(storageKey);
    return v === null ? defaultOpen : v !== 'false';
  });
  const toggle = () => setOpen(o => {
    const next = !o;
    if (storageKey) localStorage.setItem(storageKey, String(next));
    return next;
  });
  return [open, toggle];
}

// Chevron icon (reusable)
function ChevronIcon() {
  return (
    <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2"
         strokeLinecap="round" strokeLinejoin="round">
      <path d="m6 9 6 6 6-6" />
    </svg>
  );
}

export function Collapsible({
  label,
  collapseKey,
  defaultOpen = true,
  actions = null,
  className = '',
  children,
}) {
  const [open, toggle] = useCollapsible(collapseKey, defaultOpen);
  return (
    <div className={['nx-collapse', className].filter(Boolean).join(' ')}>
      <button
        type="button"
        className="nx-collapse__trigger"
        aria-expanded={open ? 'true' : 'false'}
        onClick={toggle}
      >
        <span className="nx-collapse__label">{label}</span>
        {actions && <span onClick={e => e.stopPropagation()}>{actions}</span>}
        <span className="nx-collapse__chevron"><ChevronIcon /></span>
      </button>
      <div className="nx-collapse__body" {...(!open ? { 'data-closed': '' } : {})}>
        <div className="nx-collapse__inner">{children}</div>
      </div>
    </div>
  );
}
