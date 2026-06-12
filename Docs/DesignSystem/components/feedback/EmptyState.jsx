export function EmptyState({ icon = null, title, children, action = null, className = '' }) {
  return (
    <div className={['nx-empty', className].filter(Boolean).join(' ')}>
      {icon && <div className="nx-empty__icon">{icon}</div>}
      {title && <div className="nx-empty__title">{title}</div>}
      {children && <div className="nx-empty__text">{children}</div>}
      {action && <div style={{ marginTop: 'var(--space-2)' }}>{action}</div>}
    </div>
  );
}
