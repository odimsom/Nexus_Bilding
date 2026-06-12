export function IconButton({ size = 'md', label, className = '', children, ...rest }) {
  const cls = ['nx-iconbtn', size === 'sm' ? 'nx-iconbtn--sm' : '', className].filter(Boolean).join(' ');
  return (
    <button type="button" className={cls} aria-label={label} title={label} {...rest}>
      {children}
    </button>
  );
}
