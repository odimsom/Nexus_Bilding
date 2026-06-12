export function Badge({ status = 'neutral', dot = false, outline = false, solid = false, className = '', children }) {
  const map = { success: 'nx-badge--success', info: 'nx-badge--info', warn: 'nx-badge--warn', danger: 'nx-badge--danger', neutral: '' };
  const cls = ['nx-badge', map[status] || '', outline ? 'nx-badge--outline' : '', solid ? 'nx-badge--solid' : '', className].filter(Boolean).join(' ');
  return (
    <span className={cls}>
      {dot && <span className="nx-badge__dot" />}
      {children}
    </span>
  );
}
