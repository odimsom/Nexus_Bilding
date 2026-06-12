const NX_BANNER_ICONS = {
  info: <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><circle cx="12" cy="12" r="9" /><path d="M12 16v-4M12 8h.01" /></svg>,
  success: <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><circle cx="12" cy="12" r="9" /><path d="m8.5 12 2.5 2.5 4.5-5" /></svg>,
  warn: <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M10.3 3.9 1.8 18a2 2 0 0 0 1.7 3h17a2 2 0 0 0 1.7-3L13.7 3.9a2 2 0 0 0-3.4 0Z" /><path d="M12 9v4M12 17h.01" /></svg>,
  danger: <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><circle cx="12" cy="12" r="9" /><path d="M12 8v4M12 16h.01" /></svg>,
};

export function Banner({ status = 'info', title, icon = null, onDismiss, children, className = '' }) {
  const map = { info: '', success: 'nx-banner--success', warn: 'nx-banner--warn', danger: 'nx-banner--danger' };
  return (
    <div className={['nx-banner', map[status], className].filter(Boolean).join(' ')} role={status === 'danger' ? 'alert' : 'status'}>
      <span className="nx-banner__icon">
        {icon || NX_BANNER_ICONS[status]}
      </span>
      <div className="nx-banner__body">
        {title && <div className="nx-banner__title">{title}</div>}
        {children && <div className="nx-banner__text">{children}</div>}
      </div>
      {onDismiss && (
        <button type="button" className="nx-iconbtn nx-iconbtn--sm" aria-label="Dismiss" onClick={onDismiss}>
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round"><path d="M18 6 6 18M6 6l12 12" /></svg>
        </button>
      )}
    </div>
  );
}
