export function Tag({ onRemove, className = '', children }) {
  return (
    <span className={['nx-tag', className].filter(Boolean).join(' ')}>
      <span>{children}</span>
      {onRemove && (
        <span className="nx-tag__x" role="button" aria-label="Remove" onClick={onRemove}>
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2" strokeLinecap="round"><path d="M18 6 6 18M6 6l12 12" /></svg>
        </span>
      )}
    </span>
  );
}
