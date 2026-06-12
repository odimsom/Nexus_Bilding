export function Checkbox({ label, radio = false, className = '', ...rest }) {
  return (
    <label className={['nx-check', radio ? 'nx-check--radio' : '', className].filter(Boolean).join(' ')}>
      <input type={radio ? 'radio' : 'checkbox'} {...rest} />
      <span className="nx-check__box">
        {radio ? (
          <svg viewBox="0 0 24 24" fill="currentColor"><circle cx="12" cy="12" r="5" /></svg>
        ) : (
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="3.2" strokeLinecap="round" strokeLinejoin="round"><path d="M20 6 9 17l-5-5" /></svg>
        )}
      </span>
      {label && <span>{label}</span>}
    </label>
  );
}
