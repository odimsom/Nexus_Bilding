export function Select({ label, hint, error, required = false, options = [], placeholder, id, className = '', children, ...rest }) {
  const selId = id || (label ? `nx-${label.replace(/\s+/g, '-').toLowerCase()}` : undefined);
  return (
    <div className="nx-field">
      {label && <label className="nx-label" htmlFor={selId}>{label}{required && <span className="nx-req">*</span>}</label>}
      <select id={selId} className={['nx-select', className].filter(Boolean).join(' ')} aria-invalid={error ? 'true' : undefined} {...rest}>
        {placeholder && <option value="" disabled>{placeholder}</option>}
        {options.map(o => {
          const value = typeof o === 'string' ? o : o.value;
          const text = typeof o === 'string' ? o : o.label;
          return <option key={value} value={value}>{text}</option>;
        })}
        {children}
      </select>
      {error ? <span className="nx-hint nx-hint--error">{error}</span> : hint ? <span className="nx-hint">{hint}</span> : null}
    </div>
  );
}
