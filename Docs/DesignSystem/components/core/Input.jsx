export function Input({ label, hint, error, required = false, adornment = null, mono = false, id, className = '', ...rest }) {
  const inputId = id || (label ? `nx-${label.replace(/\s+/g, '-').toLowerCase()}` : undefined);
  const input = (
    <input
      id={inputId}
      className={['nx-input', mono ? 'nx-input--mono' : '', className].filter(Boolean).join(' ')}
      aria-invalid={error ? 'true' : undefined}
      {...rest}
    />
  );
  return (
    <div className="nx-field">
      {label && <label className="nx-label" htmlFor={inputId}>{label}{required && <span className="nx-req">*</span>}</label>}
      {adornment ? (
        <span className="nx-inputgroup"><span className="nx-adorn">{adornment}</span>{input}</span>
      ) : input}
      {error ? <span className="nx-hint nx-hint--error">{error}</span> : hint ? <span className="nx-hint">{hint}</span> : null}
    </div>
  );
}
