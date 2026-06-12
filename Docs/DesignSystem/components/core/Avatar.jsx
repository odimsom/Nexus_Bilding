export function Avatar({ name = '', src = null, shape = 'rounded', size = 'md', className = '' }) {
  const initials = name.split(' ').filter(Boolean).slice(0, 2).map(w => w[0]).join('').toUpperCase();
  const cls = ['nx-avatar', shape === 'circle' ? 'nx-avatar--circle' : '', size !== 'md' ? `nx-avatar--${size}` : '', className].filter(Boolean).join(' ');
  return (
    <span className={cls} title={name || undefined}>
      {src ? <img src={src} alt={name} /> : (initials || '—')}
    </span>
  );
}
