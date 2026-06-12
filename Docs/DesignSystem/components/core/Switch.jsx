export function Switch({ label, className = '', ...rest }) {
  return (
    <label className={['nx-switch', className].filter(Boolean).join(' ')}>
      <input type="checkbox" {...rest} />
      <span className="nx-switch__track"><span className="nx-switch__thumb" /></span>
      {label && <span>{label}</span>}
    </label>
  );
}
