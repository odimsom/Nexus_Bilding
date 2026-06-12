export function Button({
  variant = 'secondary',
  size = 'md',
  block = false,
  leftIcon = null,
  rightIcon = null,
  type = 'button',
  className = '',
  children,
  ...rest
}) {
  const cls = [
    'nx-btn',
    `nx-btn--${variant}`,
    size !== 'md' ? `nx-btn--${size}` : '',
    block ? 'nx-btn--block' : '',
    className,
  ].filter(Boolean).join(' ');
  return (
    <button type={type} className={cls} {...rest}>
      {leftIcon}
      {children != null && <span>{children}</span>}
      {rightIcon}
    </button>
  );
}
