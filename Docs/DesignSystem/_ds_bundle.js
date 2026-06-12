/* @ds-bundle: {"format":3,"namespace":"NexusBillingDesignSystem_f29fd0","components":[{"name":"Avatar","sourcePath":"components/core/Avatar.jsx"},{"name":"Badge","sourcePath":"components/core/Badge.jsx"},{"name":"Button","sourcePath":"components/core/Button.jsx"},{"name":"Checkbox","sourcePath":"components/core/Checkbox.jsx"},{"name":"IconButton","sourcePath":"components/core/IconButton.jsx"},{"name":"Input","sourcePath":"components/core/Input.jsx"},{"name":"Select","sourcePath":"components/core/Select.jsx"},{"name":"Switch","sourcePath":"components/core/Switch.jsx"},{"name":"Tag","sourcePath":"components/core/Tag.jsx"},{"name":"DataTable","sourcePath":"components/data/DataTable.jsx"},{"name":"FactBox","sourcePath":"components/data/FactBox.jsx"},{"name":"KeyValue","sourcePath":"components/data/KeyValue.jsx"},{"name":"StatTile","sourcePath":"components/data/StatTile.jsx"},{"name":"Banner","sourcePath":"components/feedback/Banner.jsx"},{"name":"EmptyState","sourcePath":"components/feedback/EmptyState.jsx"},{"name":"Card","sourcePath":"components/layout/Card.jsx"},{"name":"Collapsible","sourcePath":"components/layout/Collapsible.jsx"},{"name":"Breadcrumb","sourcePath":"components/navigation/Breadcrumb.jsx"},{"name":"Tabs","sourcePath":"components/navigation/Tabs.jsx"}],"sourceHashes":{"components/core/Avatar.jsx":"03c3dfb9526c","components/core/Badge.jsx":"ee4ebdde9539","components/core/Button.jsx":"e52e968e12a6","components/core/Checkbox.jsx":"060a34a4df77","components/core/IconButton.jsx":"de2e5eaf4ef4","components/core/Input.jsx":"e06052cef110","components/core/Select.jsx":"0836894110ab","components/core/Switch.jsx":"64d504daadd8","components/core/Tag.jsx":"21964a73162a","components/data/DataTable.jsx":"0c2d262c6fcc","components/data/FactBox.jsx":"746e019e8b7e","components/data/KeyValue.jsx":"df0e38c57393","components/data/StatTile.jsx":"d0cff62e0e16","components/feedback/Banner.jsx":"9d8733c58dc6","components/feedback/EmptyState.jsx":"b2aab2d7a2a0","components/layout/Card.jsx":"b8c23a124544","components/layout/Collapsible.jsx":"ae0d2d8ff75f","components/navigation/Breadcrumb.jsx":"93e240b3726c","components/navigation/Tabs.jsx":"b26737bf01d6","ui_kits/erp/data.js":"0c5b5a25ad55","ui_kits/erp/screens.jsx":"a2920fafdee0","ui_kits/erp/shell.jsx":"be4574e1fdbc"},"inlinedExternals":[],"unexposedExports":[]} */

(() => {

const __ds_ns = (window.NexusBillingDesignSystem_f29fd0 = window.NexusBillingDesignSystem_f29fd0 || {});

const __ds_scope = {};

(__ds_ns.__errors = __ds_ns.__errors || []);

// components/core/Avatar.jsx
try { (() => {
function Avatar({
  name = '',
  src = null,
  shape = 'rounded',
  size = 'md',
  className = ''
}) {
  const initials = name.split(' ').filter(Boolean).slice(0, 2).map(w => w[0]).join('').toUpperCase();
  const cls = ['nx-avatar', shape === 'circle' ? 'nx-avatar--circle' : '', size !== 'md' ? `nx-avatar--${size}` : '', className].filter(Boolean).join(' ');
  return /*#__PURE__*/React.createElement("span", {
    className: cls,
    title: name || undefined
  }, src ? /*#__PURE__*/React.createElement("img", {
    src: src,
    alt: name
  }) : initials || '—');
}
Object.assign(__ds_scope, { Avatar });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/core/Avatar.jsx", error: String((e && e.message) || e) }); }

// components/core/Badge.jsx
try { (() => {
function Badge({
  status = 'neutral',
  dot = false,
  outline = false,
  solid = false,
  className = '',
  children
}) {
  const map = {
    success: 'nx-badge--success',
    info: 'nx-badge--info',
    warn: 'nx-badge--warn',
    danger: 'nx-badge--danger',
    neutral: ''
  };
  const cls = ['nx-badge', map[status] || '', outline ? 'nx-badge--outline' : '', solid ? 'nx-badge--solid' : '', className].filter(Boolean).join(' ');
  return /*#__PURE__*/React.createElement("span", {
    className: cls
  }, dot && /*#__PURE__*/React.createElement("span", {
    className: "nx-badge__dot"
  }), children);
}
Object.assign(__ds_scope, { Badge });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/core/Badge.jsx", error: String((e && e.message) || e) }); }

// components/core/Button.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
function Button({
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
  const cls = ['nx-btn', `nx-btn--${variant}`, size !== 'md' ? `nx-btn--${size}` : '', block ? 'nx-btn--block' : '', className].filter(Boolean).join(' ');
  return /*#__PURE__*/React.createElement("button", _extends({
    type: type,
    className: cls
  }, rest), leftIcon, children != null && /*#__PURE__*/React.createElement("span", null, children), rightIcon);
}
Object.assign(__ds_scope, { Button });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/core/Button.jsx", error: String((e && e.message) || e) }); }

// components/core/Checkbox.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
function Checkbox({
  label,
  radio = false,
  className = '',
  ...rest
}) {
  return /*#__PURE__*/React.createElement("label", {
    className: ['nx-check', radio ? 'nx-check--radio' : '', className].filter(Boolean).join(' ')
  }, /*#__PURE__*/React.createElement("input", _extends({
    type: radio ? 'radio' : 'checkbox'
  }, rest)), /*#__PURE__*/React.createElement("span", {
    className: "nx-check__box"
  }, radio ? /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "currentColor"
  }, /*#__PURE__*/React.createElement("circle", {
    cx: "12",
    cy: "12",
    r: "5"
  })) : /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "none",
    stroke: "currentColor",
    strokeWidth: "3.2",
    strokeLinecap: "round",
    strokeLinejoin: "round"
  }, /*#__PURE__*/React.createElement("path", {
    d: "M20 6 9 17l-5-5"
  }))), label && /*#__PURE__*/React.createElement("span", null, label));
}
Object.assign(__ds_scope, { Checkbox });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/core/Checkbox.jsx", error: String((e && e.message) || e) }); }

// components/core/IconButton.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
function IconButton({
  size = 'md',
  label,
  className = '',
  children,
  ...rest
}) {
  const cls = ['nx-iconbtn', size === 'sm' ? 'nx-iconbtn--sm' : '', className].filter(Boolean).join(' ');
  return /*#__PURE__*/React.createElement("button", _extends({
    type: "button",
    className: cls,
    "aria-label": label,
    title: label
  }, rest), children);
}
Object.assign(__ds_scope, { IconButton });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/core/IconButton.jsx", error: String((e && e.message) || e) }); }

// components/core/Input.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
function Input({
  label,
  hint,
  error,
  required = false,
  adornment = null,
  mono = false,
  id,
  className = '',
  ...rest
}) {
  const inputId = id || (label ? `nx-${label.replace(/\s+/g, '-').toLowerCase()}` : undefined);
  const input = /*#__PURE__*/React.createElement("input", _extends({
    id: inputId,
    className: ['nx-input', mono ? 'nx-input--mono' : '', className].filter(Boolean).join(' '),
    "aria-invalid": error ? 'true' : undefined
  }, rest));
  return /*#__PURE__*/React.createElement("div", {
    className: "nx-field"
  }, label && /*#__PURE__*/React.createElement("label", {
    className: "nx-label",
    htmlFor: inputId
  }, label, required && /*#__PURE__*/React.createElement("span", {
    className: "nx-req"
  }, "*")), adornment ? /*#__PURE__*/React.createElement("span", {
    className: "nx-inputgroup"
  }, /*#__PURE__*/React.createElement("span", {
    className: "nx-adorn"
  }, adornment), input) : input, error ? /*#__PURE__*/React.createElement("span", {
    className: "nx-hint nx-hint--error"
  }, error) : hint ? /*#__PURE__*/React.createElement("span", {
    className: "nx-hint"
  }, hint) : null);
}
Object.assign(__ds_scope, { Input });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/core/Input.jsx", error: String((e && e.message) || e) }); }

// components/core/Select.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
function Select({
  label,
  hint,
  error,
  required = false,
  options = [],
  placeholder,
  id,
  className = '',
  children,
  ...rest
}) {
  const selId = id || (label ? `nx-${label.replace(/\s+/g, '-').toLowerCase()}` : undefined);
  return /*#__PURE__*/React.createElement("div", {
    className: "nx-field"
  }, label && /*#__PURE__*/React.createElement("label", {
    className: "nx-label",
    htmlFor: selId
  }, label, required && /*#__PURE__*/React.createElement("span", {
    className: "nx-req"
  }, "*")), /*#__PURE__*/React.createElement("select", _extends({
    id: selId,
    className: ['nx-select', className].filter(Boolean).join(' '),
    "aria-invalid": error ? 'true' : undefined
  }, rest), placeholder && /*#__PURE__*/React.createElement("option", {
    value: "",
    disabled: true
  }, placeholder), options.map(o => {
    const value = typeof o === 'string' ? o : o.value;
    const text = typeof o === 'string' ? o : o.label;
    return /*#__PURE__*/React.createElement("option", {
      key: value,
      value: value
    }, text);
  }), children), error ? /*#__PURE__*/React.createElement("span", {
    className: "nx-hint nx-hint--error"
  }, error) : hint ? /*#__PURE__*/React.createElement("span", {
    className: "nx-hint"
  }, hint) : null);
}
Object.assign(__ds_scope, { Select });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/core/Select.jsx", error: String((e && e.message) || e) }); }

// components/core/Switch.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
function Switch({
  label,
  className = '',
  ...rest
}) {
  return /*#__PURE__*/React.createElement("label", {
    className: ['nx-switch', className].filter(Boolean).join(' ')
  }, /*#__PURE__*/React.createElement("input", _extends({
    type: "checkbox"
  }, rest)), /*#__PURE__*/React.createElement("span", {
    className: "nx-switch__track"
  }, /*#__PURE__*/React.createElement("span", {
    className: "nx-switch__thumb"
  })), label && /*#__PURE__*/React.createElement("span", null, label));
}
Object.assign(__ds_scope, { Switch });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/core/Switch.jsx", error: String((e && e.message) || e) }); }

// components/core/Tag.jsx
try { (() => {
function Tag({
  onRemove,
  className = '',
  children
}) {
  return /*#__PURE__*/React.createElement("span", {
    className: ['nx-tag', className].filter(Boolean).join(' ')
  }, /*#__PURE__*/React.createElement("span", null, children), onRemove && /*#__PURE__*/React.createElement("span", {
    className: "nx-tag__x",
    role: "button",
    "aria-label": "Remove",
    onClick: onRemove
  }, /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "none",
    stroke: "currentColor",
    strokeWidth: "2.2",
    strokeLinecap: "round"
  }, /*#__PURE__*/React.createElement("path", {
    d: "M18 6 6 18M6 6l12 12"
  }))));
}
Object.assign(__ds_scope, { Tag });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/core/Tag.jsx", error: String((e && e.message) || e) }); }

// components/data/DataTable.jsx
try { (() => {
function DataTable({
  columns = [],
  rows = [],
  zebra = false,
  selectedId = null,
  getRowId,
  onRowClick,
  className = ''
}) {
  const rowId = getRowId || ((r, i) => r.id ?? i);
  return /*#__PURE__*/React.createElement("table", {
    className: ['nx-table', zebra ? 'nx-table--zebra' : '', className].filter(Boolean).join(' ')
  }, /*#__PURE__*/React.createElement("thead", null, /*#__PURE__*/React.createElement("tr", null, columns.map(col => /*#__PURE__*/React.createElement("th", {
    key: col.key,
    className: col.align === 'right' ? 'nx-th--num' : '',
    style: col.width ? {
      width: col.width
    } : undefined
  }, col.header)))), /*#__PURE__*/React.createElement("tbody", null, rows.map((row, i) => {
    const id = rowId(row, i);
    return /*#__PURE__*/React.createElement("tr", {
      key: id,
      "aria-selected": selectedId != null && id === selectedId ? 'true' : undefined,
      onClick: onRowClick ? () => onRowClick(row, id) : undefined,
      style: onRowClick ? {
        cursor: 'pointer'
      } : undefined
    }, columns.map(col => {
      const variant = col.variant === 'num' ? 'nx-td--num' : col.variant === 'doc' ? 'nx-td--doc' : '';
      const content = col.render ? col.render(row[col.key], row) : row[col.key];
      return /*#__PURE__*/React.createElement("td", {
        key: col.key,
        className: variant
      }, content);
    }));
  })));
}
Object.assign(__ds_scope, { DataTable });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/data/DataTable.jsx", error: String((e && e.message) || e) }); }

// components/data/FactBox.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
function FactBox({
  eyebrow = 'Related',
  title,
  sections = [],
  children,
  className = ''
}) {
  return /*#__PURE__*/React.createElement("aside", {
    className: ['nx-factbox', className].filter(Boolean).join(' ')
  }, /*#__PURE__*/React.createElement("div", {
    className: "nx-factbox__head"
  }, /*#__PURE__*/React.createElement("div", {
    className: "nx-factbox__eyebrow"
  }, eyebrow), title && /*#__PURE__*/React.createElement("div", {
    className: "nx-factbox__title"
  }, title)), sections.map((s, i) => /*#__PURE__*/React.createElement(FactBoxSection, {
    key: i,
    section: s,
    index: i,
    baseKey: title || eyebrow
  })), children && /*#__PURE__*/React.createElement("div", {
    className: "nx-factbox__section"
  }, children));
}
function FactBoxSection({
  section,
  index,
  baseKey
}) {
  const collapseKey = section.collapseKey || 'fb-' + String(baseKey || '').replace(/\s+/g, '-').toLowerCase() + '-' + index;
  const defaultOpen = section.defaultOpen !== false;
  const [open, setOpen] = React.useState(() => {
    const v = localStorage.getItem('nx-collapse-' + collapseKey);
    return v === null ? defaultOpen : v !== 'false';
  });
  const toggle = () => setOpen(o => {
    const next = !o;
    localStorage.setItem('nx-collapse-' + collapseKey, String(next));
    return next;
  });
  if (!section.label) {
    // Non-collapsible plain section
    return /*#__PURE__*/React.createElement("div", {
      className: "nx-factbox__section"
    }, section.content);
  }
  return /*#__PURE__*/React.createElement("div", {
    className: "nx-factbox__section",
    style: {
      padding: 0
    }
  }, /*#__PURE__*/React.createElement("button", {
    type: "button",
    onClick: toggle,
    "aria-expanded": open ? 'true' : 'false',
    style: {
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'space-between',
      width: '100%',
      background: 'none',
      border: 'none',
      cursor: 'pointer',
      padding: 'var(--space-3) var(--space-4) var(--space-2)',
      font: 'inherit'
    }
  }, /*#__PURE__*/React.createElement("span", {
    className: "nx-factbox__sectionlabel",
    style: {
      marginBottom: 0
    }
  }, section.label), /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "none",
    stroke: "currentColor",
    strokeWidth: "2.2",
    strokeLinecap: "round",
    strokeLinejoin: "round",
    style: {
      width: 13,
      height: 13,
      color: 'var(--text-faint)',
      flexShrink: 0,
      transition: 'transform 200ms ease',
      transform: open ? 'rotate(0deg)' : 'rotate(-90deg)'
    }
  }, /*#__PURE__*/React.createElement("path", {
    d: "m6 9 6 6 6-6"
  }))), /*#__PURE__*/React.createElement("div", _extends({
    className: "nx-collapse__body"
  }, !open ? {
    'data-closed': ''
  } : {}), /*#__PURE__*/React.createElement("div", {
    className: "nx-collapse__inner",
    style: {
      padding: '0 var(--space-4) var(--space-3)'
    }
  }, section.content)));
}
Object.assign(__ds_scope, { FactBox });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/data/FactBox.jsx", error: String((e && e.message) || e) }); }

// components/data/KeyValue.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
function KeyValue({
  items = [],
  ruled = false,
  collapseKey,
  showMoreLabel = 'Show all fields',
  className = ''
}) {
  // Separate primary (always shown) from secondary (hidden until toggled)
  const primary = items.filter(it => !it.secondary);
  const secondary = items.filter(it => it.secondary);
  const hasSecondary = secondary.length > 0;
  const storageKey = collapseKey ? 'nx-kv-more-' + collapseKey : null;
  const [showAll, setShowAll] = React.useState(() => {
    if (!storageKey) return false;
    return localStorage.getItem(storageKey) === 'true';
  });
  const toggleMore = () => setShowAll(v => {
    const next = !v;
    if (storageKey) localStorage.setItem(storageKey, String(next));
    return next;
  });
  const renderRow = (it, i) => /*#__PURE__*/React.createElement(React.Fragment, {
    key: i
  }, /*#__PURE__*/React.createElement("div", {
    className: "nx-kv__k"
  }, it.key), /*#__PURE__*/React.createElement("div", {
    className: ['nx-kv__v', it.mono ? 'nx-kv__v--mono' : ''].filter(Boolean).join(' ')
  }, it.value ?? /*#__PURE__*/React.createElement("span", {
    style: {
      color: 'var(--text-faint)'
    }
  }, "\u2014")));
  return /*#__PURE__*/React.createElement("div", {
    className: ['nx-kv', ruled ? 'nx-kv--ruled' : '', className].filter(Boolean).join(' ')
  }, primary.map(renderRow), hasSecondary && /*#__PURE__*/React.createElement(React.Fragment, null, /*#__PURE__*/React.createElement("div", _extends({
    className: "nx-kv__secondary"
  }, !showAll ? {
    'data-closed': ''
  } : {}, {
    style: {
      gridColumn: '1 / -1'
    }
  }), /*#__PURE__*/React.createElement("div", {
    className: "nx-kv__inner",
    style: {
      display: 'grid',
      gridTemplateColumns: 'minmax(120px,38%) 1fr',
      gap: '1px var(--space-4)'
    }
  }, secondary.map(renderRow))), /*#__PURE__*/React.createElement("div", {
    style: {
      gridColumn: '1 / -1',
      paddingTop: 'var(--space-1)'
    }
  }, /*#__PURE__*/React.createElement("button", _extends({
    type: "button",
    className: "nx-showmore"
  }, showAll ? {
    'data-open': ''
  } : {}, {
    onClick: toggleMore
  }), /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "none",
    stroke: "currentColor",
    strokeWidth: "2.2",
    strokeLinecap: "round",
    strokeLinejoin: "round"
  }, /*#__PURE__*/React.createElement("path", {
    d: "m6 9 6 6 6-6"
  })), showAll ? 'Show fewer fields' : showMoreLabel + ` (${secondary.length})`))));
}
Object.assign(__ds_scope, { KeyValue });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/data/KeyValue.jsx", error: String((e && e.message) || e) }); }

// components/data/StatTile.jsx
try { (() => {
function StatTile({
  label,
  value,
  delta = null,
  trend = null,
  className = ''
}) {
  return /*#__PURE__*/React.createElement("div", {
    className: ['nx-stat', className].filter(Boolean).join(' ')
  }, /*#__PURE__*/React.createElement("span", {
    className: "nx-stat__label"
  }, label), /*#__PURE__*/React.createElement("span", {
    className: "nx-stat__value"
  }, value), delta != null && /*#__PURE__*/React.createElement("span", {
    className: `nx-stat__delta nx-stat__delta--${trend === 'down' ? 'down' : 'up'}`
  }, /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "none",
    stroke: "currentColor",
    strokeWidth: "2.4",
    strokeLinecap: "round",
    strokeLinejoin: "round"
  }, trend === 'down' ? /*#__PURE__*/React.createElement("path", {
    d: "M7 7l10 10M17 7v10H7"
  }) : /*#__PURE__*/React.createElement("path", {
    d: "M7 17 17 7M7 7h10v10"
  })), delta));
}
Object.assign(__ds_scope, { StatTile });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/data/StatTile.jsx", error: String((e && e.message) || e) }); }

// components/feedback/Banner.jsx
try { (() => {
const NX_BANNER_ICONS = {
  info: /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "none",
    stroke: "currentColor",
    strokeWidth: "2",
    strokeLinecap: "round",
    strokeLinejoin: "round"
  }, /*#__PURE__*/React.createElement("circle", {
    cx: "12",
    cy: "12",
    r: "9"
  }), /*#__PURE__*/React.createElement("path", {
    d: "M12 16v-4M12 8h.01"
  })),
  success: /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "none",
    stroke: "currentColor",
    strokeWidth: "2",
    strokeLinecap: "round",
    strokeLinejoin: "round"
  }, /*#__PURE__*/React.createElement("circle", {
    cx: "12",
    cy: "12",
    r: "9"
  }), /*#__PURE__*/React.createElement("path", {
    d: "m8.5 12 2.5 2.5 4.5-5"
  })),
  warn: /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "none",
    stroke: "currentColor",
    strokeWidth: "2",
    strokeLinecap: "round",
    strokeLinejoin: "round"
  }, /*#__PURE__*/React.createElement("path", {
    d: "M10.3 3.9 1.8 18a2 2 0 0 0 1.7 3h17a2 2 0 0 0 1.7-3L13.7 3.9a2 2 0 0 0-3.4 0Z"
  }), /*#__PURE__*/React.createElement("path", {
    d: "M12 9v4M12 17h.01"
  })),
  danger: /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "none",
    stroke: "currentColor",
    strokeWidth: "2",
    strokeLinecap: "round",
    strokeLinejoin: "round"
  }, /*#__PURE__*/React.createElement("circle", {
    cx: "12",
    cy: "12",
    r: "9"
  }), /*#__PURE__*/React.createElement("path", {
    d: "M12 8v4M12 16h.01"
  }))
};
function Banner({
  status = 'info',
  title,
  icon = null,
  onDismiss,
  children,
  className = ''
}) {
  const map = {
    info: '',
    success: 'nx-banner--success',
    warn: 'nx-banner--warn',
    danger: 'nx-banner--danger'
  };
  return /*#__PURE__*/React.createElement("div", {
    className: ['nx-banner', map[status], className].filter(Boolean).join(' '),
    role: status === 'danger' ? 'alert' : 'status'
  }, /*#__PURE__*/React.createElement("span", {
    className: "nx-banner__icon"
  }, icon || NX_BANNER_ICONS[status]), /*#__PURE__*/React.createElement("div", {
    className: "nx-banner__body"
  }, title && /*#__PURE__*/React.createElement("div", {
    className: "nx-banner__title"
  }, title), children && /*#__PURE__*/React.createElement("div", {
    className: "nx-banner__text"
  }, children)), onDismiss && /*#__PURE__*/React.createElement("button", {
    type: "button",
    className: "nx-iconbtn nx-iconbtn--sm",
    "aria-label": "Dismiss",
    onClick: onDismiss
  }, /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "none",
    stroke: "currentColor",
    strokeWidth: "2",
    strokeLinecap: "round"
  }, /*#__PURE__*/React.createElement("path", {
    d: "M18 6 6 18M6 6l12 12"
  }))));
}
Object.assign(__ds_scope, { Banner });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/feedback/Banner.jsx", error: String((e && e.message) || e) }); }

// components/feedback/EmptyState.jsx
try { (() => {
function EmptyState({
  icon = null,
  title,
  children,
  action = null,
  className = ''
}) {
  return /*#__PURE__*/React.createElement("div", {
    className: ['nx-empty', className].filter(Boolean).join(' ')
  }, icon && /*#__PURE__*/React.createElement("div", {
    className: "nx-empty__icon"
  }, icon), title && /*#__PURE__*/React.createElement("div", {
    className: "nx-empty__title"
  }, title), children && /*#__PURE__*/React.createElement("div", {
    className: "nx-empty__text"
  }, children), action && /*#__PURE__*/React.createElement("div", {
    style: {
      marginTop: 'var(--space-2)'
    }
  }, action));
}
Object.assign(__ds_scope, { EmptyState });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/feedback/EmptyState.jsx", error: String((e && e.message) || e) }); }

// components/layout/Card.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
function Card({
  title,
  eyebrow,
  actions = null,
  footer = null,
  children,
  padded = true,
  collapsible = false,
  collapseKey,
  defaultOpen = true,
  className = ''
}) {
  const [open, setOpen] = React.useState(() => {
    if (!collapsible || !collapseKey) return defaultOpen;
    const v = localStorage.getItem('nx-collapse-' + collapseKey);
    return v === null ? defaultOpen : v !== 'false';
  });
  const toggle = () => {
    if (!collapsible) return;
    setOpen(o => {
      const next = !o;
      if (collapseKey) localStorage.setItem('nx-collapse-' + collapseKey, String(next));
      return next;
    });
  };
  const ChevronSVG = () => /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "none",
    stroke: "currentColor",
    strokeWidth: "2.2",
    strokeLinecap: "round",
    strokeLinejoin: "round",
    style: {
      width: 16,
      height: 16,
      display: 'block',
      transition: 'transform 200ms ease',
      transform: open ? 'rotate(0deg)' : 'rotate(-90deg)'
    }
  }, /*#__PURE__*/React.createElement("path", {
    d: "m6 9 6 6 6-6"
  }));
  return /*#__PURE__*/React.createElement("section", {
    className: ['nx-card', className].filter(Boolean).join(' ')
  }, (title || actions) && /*#__PURE__*/React.createElement("header", {
    className: "nx-card__head",
    onClick: collapsible ? toggle : undefined,
    style: collapsible ? {
      cursor: 'pointer',
      userSelect: 'none'
    } : undefined,
    role: collapsible ? 'button' : undefined,
    "aria-expanded": collapsible ? String(open) : undefined
  }, /*#__PURE__*/React.createElement("div", null, eyebrow && /*#__PURE__*/React.createElement("div", {
    className: "nx-factbox__eyebrow"
  }, eyebrow), title && /*#__PURE__*/React.createElement("div", {
    className: "nx-card__title"
  }, title)), /*#__PURE__*/React.createElement("div", {
    className: "nx-card__actions",
    onClick: e => e.stopPropagation()
  }, actions), collapsible && /*#__PURE__*/React.createElement("span", {
    style: {
      marginLeft: 'var(--space-2)',
      color: 'var(--text-faint)'
    }
  }, /*#__PURE__*/React.createElement(ChevronSVG, null))), /*#__PURE__*/React.createElement("div", _extends({
    className: "nx-collapse__body"
  }, !open && collapsible ? {
    'data-closed': ''
  } : {}), /*#__PURE__*/React.createElement("div", {
    className: "nx-collapse__inner"
  }, /*#__PURE__*/React.createElement("div", {
    className: "nx-card__body",
    style: padded ? undefined : {
      padding: 0
    }
  }, children), footer && /*#__PURE__*/React.createElement("footer", {
    className: "nx-card__foot"
  }, footer))));
}
Object.assign(__ds_scope, { Card });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/layout/Card.jsx", error: String((e && e.message) || e) }); }

// components/layout/Collapsible.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
// Shared collapse hook — persists open/closed state to localStorage.
// key: unique string that identifies this collapsible; defaultOpen: boolean.
function useCollapsible(key, defaultOpen = true) {
  const storageKey = key ? 'nx-collapse-' + key : null;
  const [open, setOpen] = React.useState(() => {
    if (!storageKey) return defaultOpen;
    const v = localStorage.getItem(storageKey);
    return v === null ? defaultOpen : v !== 'false';
  });
  const toggle = () => setOpen(o => {
    const next = !o;
    if (storageKey) localStorage.setItem(storageKey, String(next));
    return next;
  });
  return [open, toggle];
}

// Chevron icon (reusable)
function ChevronIcon() {
  return /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "none",
    stroke: "currentColor",
    strokeWidth: "2.2",
    strokeLinecap: "round",
    strokeLinejoin: "round"
  }, /*#__PURE__*/React.createElement("path", {
    d: "m6 9 6 6 6-6"
  }));
}
function Collapsible({
  label,
  collapseKey,
  defaultOpen = true,
  actions = null,
  className = '',
  children
}) {
  const [open, toggle] = useCollapsible(collapseKey, defaultOpen);
  return /*#__PURE__*/React.createElement("div", {
    className: ['nx-collapse', className].filter(Boolean).join(' ')
  }, /*#__PURE__*/React.createElement("button", {
    type: "button",
    className: "nx-collapse__trigger",
    "aria-expanded": open ? 'true' : 'false',
    onClick: toggle
  }, /*#__PURE__*/React.createElement("span", {
    className: "nx-collapse__label"
  }, label), actions && /*#__PURE__*/React.createElement("span", {
    onClick: e => e.stopPropagation()
  }, actions), /*#__PURE__*/React.createElement("span", {
    className: "nx-collapse__chevron"
  }, /*#__PURE__*/React.createElement(ChevronIcon, null))), /*#__PURE__*/React.createElement("div", _extends({
    className: "nx-collapse__body"
  }, !open ? {
    'data-closed': ''
  } : {}), /*#__PURE__*/React.createElement("div", {
    className: "nx-collapse__inner"
  }, children)));
}
Object.assign(__ds_scope, { Collapsible });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/layout/Collapsible.jsx", error: String((e && e.message) || e) }); }

// components/navigation/Breadcrumb.jsx
try { (() => {
function Breadcrumb({
  items = [],
  className = ''
}) {
  const Sep = () => /*#__PURE__*/React.createElement("span", {
    className: "nx-crumbs__sep",
    "aria-hidden": "true"
  }, /*#__PURE__*/React.createElement("svg", {
    viewBox: "0 0 24 24",
    fill: "none",
    stroke: "currentColor",
    strokeWidth: "2",
    strokeLinecap: "round",
    strokeLinejoin: "round"
  }, /*#__PURE__*/React.createElement("path", {
    d: "m9 18 6-6-6-6"
  })));
  return /*#__PURE__*/React.createElement("nav", {
    className: ['nx-crumbs', className].filter(Boolean).join(' '),
    "aria-label": "Breadcrumb"
  }, items.map((it, i) => {
    const last = i === items.length - 1;
    return /*#__PURE__*/React.createElement(React.Fragment, {
      key: i
    }, last ? /*#__PURE__*/React.createElement("span", {
      className: "nx-crumb nx-crumb--current",
      "aria-current": "page"
    }, it.label) : /*#__PURE__*/React.createElement("a", {
      className: "nx-crumb",
      href: it.href || '#',
      onClick: it.onClick
    }, it.label), !last && /*#__PURE__*/React.createElement(Sep, null));
  }));
}
Object.assign(__ds_scope, { Breadcrumb });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/navigation/Breadcrumb.jsx", error: String((e && e.message) || e) }); }

// components/navigation/Tabs.jsx
try { (() => {
function Tabs({
  tabs = [],
  value,
  onChange,
  className = ''
}) {
  return /*#__PURE__*/React.createElement("div", {
    className: ['nx-tabs', className].filter(Boolean).join(' '),
    role: "tablist"
  }, tabs.map(t => {
    const id = typeof t === 'string' ? t : t.id;
    const label = typeof t === 'string' ? t : t.label;
    const count = typeof t === 'string' ? undefined : t.count;
    const selected = id === value;
    return /*#__PURE__*/React.createElement("button", {
      key: id,
      role: "tab",
      type: "button",
      "aria-selected": selected ? 'true' : 'false',
      className: "nx-tab",
      onClick: () => onChange && onChange(id)
    }, label, count != null && /*#__PURE__*/React.createElement("span", {
      className: "nx-tab__count"
    }, count));
  }));
}
Object.assign(__ds_scope, { Tabs });
})(); } catch (e) { __ds_ns.__errors.push({ path: "components/navigation/Tabs.jsx", error: String((e && e.message) || e) }); }

// ui_kits/erp/data.js
try { (() => {
// Nexus Billing — mock data (RD market flavor; matches the domain entities)
window.NXDATA = function () {
  const customers = [{
    no: 'C-001847',
    name: 'Altagracia Comercial, S.R.L.',
    rnc: '1-31-12345-6',
    city: 'Santo Domingo',
    contact: 'María Altagracia',
    salesperson: 'José Ramírez',
    balance: 84120.00,
    overdue: 12400.00,
    creditLimit: 250000,
    terms: 'NET-30',
    status: 'active',
    blocked: false
  }, {
    no: 'C-001902',
    name: 'Distribuidora Caribe',
    rnc: '1-30-56789-1',
    city: 'Santiago',
    contact: 'Pedro Núñez',
    salesperson: 'Laura Méndez',
    balance: 208750.00,
    overdue: 0,
    creditLimit: 500000,
    terms: 'NET-45',
    status: 'active',
    blocked: false
  }, {
    no: 'C-002013',
    name: 'Ferretería del Sur',
    rnc: '1-32-98765-3',
    city: 'San Cristóbal',
    contact: 'Juan de los Santos',
    salesperson: 'José Ramírez',
    balance: 1905.40,
    overdue: 1905.40,
    creditLimit: 75000,
    terms: 'NET-15',
    status: 'active',
    blocked: false
  }, {
    no: 'C-002044',
    name: 'Supermercado Nacional',
    rnc: '1-01-23456-7',
    city: 'Santo Domingo',
    contact: 'Rosa Jiménez',
    salesperson: 'Laura Méndez',
    balance: 46000.00,
    overdue: 46000.00,
    creditLimit: 120000,
    terms: 'NET-30',
    status: 'overdue',
    blocked: false
  }, {
    no: 'C-002101',
    name: 'Constructora Bávaro',
    rnc: '1-23-65432-2',
    city: 'Punta Cana',
    contact: 'Luis Tavárez',
    salesperson: 'Carla Objío',
    balance: 0,
    overdue: 0,
    creditLimit: 1000000,
    terms: 'NET-60',
    status: 'active',
    blocked: false
  }, {
    no: 'C-002130',
    name: 'Colmadón La Esquina',
    rnc: '1-45-11111-5',
    city: 'La Vega',
    contact: 'Frank Polanco',
    salesperson: 'José Ramírez',
    balance: 12300.00,
    overdue: 0,
    creditLimit: 40000,
    terms: 'NET-15',
    status: 'blocked',
    blocked: true
  }];
  const items = [{
    no: 'IT-1001',
    description: 'Cemento Gris Portland 42.5kg',
    uom: 'BAG',
    price: 415.00,
    cost: 332.00,
    onHand: 1240,
    blocked: false
  }, {
    no: 'IT-1002',
    description: 'Varilla de Acero 1/2" x 30ft',
    uom: 'PCS',
    price: 690.00,
    cost: 548.00,
    onHand: 860,
    blocked: false
  }, {
    no: 'IT-1140',
    description: 'Bloque de Concreto 6"',
    uom: 'PCS',
    price: 38.50,
    cost: 27.10,
    onHand: 9800,
    blocked: false
  }, {
    no: 'IT-2210',
    description: 'Pintura Acrílica Blanca (Galón)',
    uom: 'GAL',
    price: 1190.00,
    cost: 845.00,
    onHand: 320,
    blocked: false
  }, {
    no: 'IT-3300',
    description: 'Tubería PVC 4" Sanitaria',
    uom: 'PCS',
    price: 540.00,
    cost: 410.00,
    onHand: 0,
    blocked: true
  }];
  const invoices = [{
    no: 'SI-2026-001847',
    type: 'Invoice',
    ncf: 'B01-00000847',
    vatPct: 18,
    customerNo: 'C-001847',
    billTo: 'Altagracia Comercial, S.R.L.',
    postingDate: '2026-06-11',
    dueDate: '2026-07-11',
    status: 'open',
    amount: 12400.00
  }, {
    no: 'SI-2026-001848',
    type: 'Invoice',
    ncf: 'B01-00000848',
    vatPct: 18,
    customerNo: 'C-001902',
    billTo: 'Distribuidora Caribe',
    postingDate: '2026-06-10',
    dueDate: '2026-07-25',
    status: 'posted',
    amount: 208750.00
  }, {
    no: 'CM-2026-000219',
    type: 'Credit Memo',
    ncf: 'B04-00000219',
    vatPct: 18,
    customerNo: 'C-002013',
    billTo: 'Ferretería del Sur',
    postingDate: '2026-06-09',
    dueDate: '2026-06-24',
    status: 'pending',
    amount: 1905.40
  }, {
    no: 'SI-2026-001851',
    type: 'Invoice',
    ncf: 'B01-00000851',
    vatPct: 18,
    customerNo: 'C-002044',
    billTo: 'Supermercado Nacional',
    postingDate: '2026-05-02',
    dueDate: '2026-06-01',
    status: 'overdue',
    amount: 46000.00
  }, {
    no: 'SI-2026-001853',
    type: 'Invoice',
    ncf: 'B01-00000853',
    vatPct: 18,
    customerNo: 'C-002101',
    billTo: 'Constructora Bávaro',
    postingDate: '2026-06-11',
    dueDate: '2026-08-10',
    status: 'draft',
    amount: 0
  }, {
    no: 'SI-2026-001844',
    type: 'Invoice',
    ncf: 'B01-00000844',
    vatPct: 18,
    customerNo: 'C-001902',
    billTo: 'Distribuidora Caribe',
    postingDate: '2026-06-04',
    dueDate: '2026-07-19',
    status: 'posted',
    amount: 98250.00
  }];

  // Lines for the focus invoice SI-2026-001847
  const invoiceLines = [{
    itemNo: 'IT-1001',
    description: 'Cemento Gris Portland 42.5kg',
    qty: 20,
    uom: 'BAG',
    price: 415.00,
    line: 8300.00
  }, {
    itemNo: 'IT-1140',
    description: 'Bloque de Concreto 6"',
    qty: 80,
    uom: 'PCS',
    price: 38.50,
    line: 3080.00
  }, {
    itemNo: 'IT-2210',
    description: 'Pintura Acrílica Blanca (Galón)',
    qty: 1,
    uom: 'GAL',
    price: 1190.00,
    line: 1190.00
  }];
  const statusTone = {
    open: 'info',
    posted: 'success',
    pending: 'warn',
    overdue: 'danger',
    draft: 'neutral',
    active: 'success',
    blocked: 'danger'
  };
  const statusLabel = {
    open: 'Open',
    posted: 'Posted',
    pending: 'Pending',
    overdue: 'Overdue',
    draft: 'Draft',
    active: 'Active',
    blocked: 'Blocked'
  };
  const aging = [{
    cap: 'Current',
    val: 1196132,
    color: 'var(--viz-1)'
  }, {
    cap: '1–30',
    val: 412000,
    color: 'var(--viz-3)'
  }, {
    cap: '31–60',
    val: 142300,
    color: 'var(--amber-500)'
  }, {
    cap: '61–90',
    val: 58400,
    color: 'var(--red-500)'
  }, {
    cap: '90+',
    val: 25700,
    color: 'var(--red-700)'
  }];
  const money = n => 'RD$ ' + n.toLocaleString('en-US', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2
  });
  const moneyShort = n => {
    if (n >= 1e6) return 'RD$ ' + (n / 1e6).toFixed(2) + 'M';
    if (n >= 1e3) return 'RD$ ' + (n / 1e3).toFixed(1) + 'K';
    return 'RD$ ' + n.toFixed(0);
  };
  return {
    customers,
    items,
    invoices,
    invoiceLines,
    statusTone,
    statusLabel,
    aging,
    money,
    moneyShort
  };
}();
})(); } catch (e) { __ds_ns.__errors.push({ path: "ui_kits/erp/data.js", error: String((e && e.message) || e) }); }

// ui_kits/erp/screens.jsx
try { (() => {
/* Nexus Billing ERP — screens. Composes design-system primitives (window.NS). */
(function () {
  const NS = window.NexusBillingDesignSystem_f29fd0 || {};
  const {
    Button,
    IconButton,
    Badge,
    Tag,
    Avatar,
    Input,
    Select,
    DataTable,
    StatTile,
    KeyValue,
    FactBox,
    Card,
    Tabs
  } = NS;
  const Icon = window.NXIcon;
  const {
    useState
  } = React;
  const D = window.NXDATA;
  const StatusBadge = ({
    s
  }) => /*#__PURE__*/React.createElement(Badge, {
    status: D.statusTone[s],
    dot: true
  }, D.statusLabel[s]);
  const Money = ({
    n,
    sign
  }) => /*#__PURE__*/React.createElement("span", {
    className: "erp-num",
    style: sign ? {
      color: n < 0 ? 'var(--money-negative)' : 'var(--money-positive)'
    } : undefined
  }, D.money(n));

  /* ============ DASHBOARD ============ */
  function Dashboard({
    go
  }) {
    const max = Math.max(...D.aging.map(a => a.val));
    return /*#__PURE__*/React.createElement("div", {
      className: "erp-stack"
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-pagehead"
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        flex: 1
      }
    }, /*#__PURE__*/React.createElement("h1", null, "Good morning, Jos\xE9"), /*#__PURE__*/React.createElement("div", {
      className: "erp-pagehead__meta"
    }, "Thursday, 11 June 2026 \xB7 Fiscal period 2026-06 is open"))), /*#__PURE__*/React.createElement("div", {
      className: "erp-grid-3"
    }, /*#__PURE__*/React.createElement(StatTile, {
      label: "Outstanding AR",
      value: D.moneyShort(1834532),
      delta: "+12.4%",
      trend: "up"
    }), /*#__PURE__*/React.createElement(StatTile, {
      label: "Overdue",
      value: D.moneyShort(84105),
      delta: "-3.1%",
      trend: "down"
    }), /*#__PURE__*/React.createElement(StatTile, {
      label: "Posted today",
      value: "37",
      delta: "+6",
      trend: "up"
    })), /*#__PURE__*/React.createElement("div", {
      className: "erp-grid-2"
    }, /*#__PURE__*/React.createElement(Card, {
      title: "AR aging",
      actions: /*#__PURE__*/React.createElement(Button, {
        variant: "ghost",
        size: "sm",
        rightIcon: /*#__PURE__*/React.createElement(Icon, {
          n: "arrow-up-right"
        }),
        onClick: () => go({
          screen: 'gl'
        })
      }, "Ledger")
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-bars"
    }, D.aging.map(a => /*#__PURE__*/React.createElement("div", {
      className: "col",
      key: a.cap
    }, /*#__PURE__*/React.createElement("span", {
      className: "val"
    }, D.moneyShort(a.val)), /*#__PURE__*/React.createElement("div", {
      className: "bar",
      style: {
        height: a.val / max * 100 + '%',
        background: a.color
      }
    }), /*#__PURE__*/React.createElement("span", {
      className: "cap"
    }, a.cap))))), /*#__PURE__*/React.createElement(Card, {
      title: "Activity"
    }, /*#__PURE__*/React.createElement("ul", {
      className: "nx-linklist",
      style: {
        gap: 0
      }
    }, [{
      ic: 'check-circle-2',
      c: 'var(--emerald-500)',
      t: 'Invoice SI-2026-001848 posted',
      m: 'Distribuidora Caribe · RD$ 208,750.00',
      time: '08:24'
    }, {
      ic: 'plus-circle',
      c: 'var(--blue-500)',
      t: 'Customer C-002101 created',
      m: 'Constructora Bávaro',
      time: '07:55'
    }, {
      ic: 'alert-triangle',
      c: 'var(--amber-500)',
      t: 'Credit memo CM-2026-000219 awaiting approval',
      m: 'Ferretería del Sur · RD$ 1,905.40',
      time: 'Yesterday'
    }, {
      ic: 'ban',
      c: 'var(--red-500)',
      t: 'Customer C-002130 blocked',
      m: 'Colmadón La Esquina · credit hold',
      time: 'Yesterday'
    }].map((a, i) => /*#__PURE__*/React.createElement("li", {
      key: i,
      style: {
        display: 'flex',
        gap: 12,
        alignItems: 'flex-start',
        padding: '11px 0',
        borderBottom: i < 3 ? '1px solid var(--border-subtle)' : 'none'
      }
    }, /*#__PURE__*/React.createElement("span", {
      style: {
        color: a.c,
        marginTop: 1
      }
    }, /*#__PURE__*/React.createElement(Icon, {
      n: a.ic
    })), /*#__PURE__*/React.createElement("div", {
      style: {
        flex: 1,
        minWidth: 0
      }
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        fontSize: 13.5,
        fontWeight: 600,
        color: 'var(--text-strong)'
      }
    }, a.t), /*#__PURE__*/React.createElement("div", {
      style: {
        fontSize: 12.5,
        color: 'var(--text-muted)'
      }
    }, a.m)), /*#__PURE__*/React.createElement("span", {
      style: {
        fontSize: 12,
        color: 'var(--text-faint)',
        fontFamily: 'var(--font-mono)'
      }
    }, a.time)))))), /*#__PURE__*/React.createElement(Card, {
      title: "Open sales invoices",
      padded: false,
      actions: /*#__PURE__*/React.createElement(Button, {
        variant: "primary",
        size: "sm",
        leftIcon: /*#__PURE__*/React.createElement(Icon, {
          n: "plus"
        }),
        onClick: () => go({
          screen: 'invoice',
          no: 'SI-2026-001847'
        })
      }, "New invoice")
    }, /*#__PURE__*/React.createElement(InvoiceTable, {
      go: go,
      rows: D.invoices.filter(i => ['open', 'overdue', 'pending'].includes(i.status))
    })));
  }

  /* ============ SHARED TABLES ============ */
  function InvoiceTable({
    rows,
    go,
    selectedId
  }) {
    return /*#__PURE__*/React.createElement(DataTable, {
      selectedId: selectedId,
      getRowId: r => r.no,
      onRowClick: r => go({
        screen: 'invoice',
        no: r.no
      }),
      columns: [{
        key: 'no',
        header: 'Document No.',
        variant: 'doc',
        width: '170px'
      }, {
        key: 'billTo',
        header: 'Bill-to'
      }, {
        key: 'type',
        header: 'Type',
        width: '120px',
        render: v => /*#__PURE__*/React.createElement("span", {
          style: {
            color: 'var(--text-muted)'
          }
        }, v)
      }, {
        key: 'postingDate',
        header: 'Posting date',
        width: '130px',
        render: v => /*#__PURE__*/React.createElement("span", {
          className: "erp-num",
          style: {
            fontSize: 13
          }
        }, v)
      }, {
        key: 'status',
        header: 'Status',
        width: '120px',
        render: v => /*#__PURE__*/React.createElement(StatusBadge, {
          s: v
        })
      }, {
        key: 'amount',
        header: 'Amount',
        variant: 'num',
        align: 'right',
        render: v => D.money(v)
      }],
      rows: rows
    });
  }

  /* ============ CUSTOMER LIST ============ */
  function CustomerList({
    go
  }) {
    const [q, setQ] = useState('');
    const rows = D.customers.filter(c => c.name.toLowerCase().includes(q.toLowerCase()) || c.no.toLowerCase().includes(q.toLowerCase()));
    return /*#__PURE__*/React.createElement("div", {
      className: "erp-stack"
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-pagehead"
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        flex: 1
      }
    }, /*#__PURE__*/React.createElement("h1", null, "Customers"), /*#__PURE__*/React.createElement("div", {
      className: "erp-pagehead__meta"
    }, D.customers.length, " records \xB7 RD$ 353,075.40 total balance"))), /*#__PURE__*/React.createElement(Card, {
      padded: false
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-toolbar"
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        width: 260
      }
    }, /*#__PURE__*/React.createElement("span", {
      className: "nx-inputgroup"
    }, /*#__PURE__*/React.createElement("span", {
      className: "nx-adorn"
    }, /*#__PURE__*/React.createElement(Icon, {
      n: "search"
    })), /*#__PURE__*/React.createElement("input", {
      className: "nx-input",
      placeholder: "Search customers\u2026",
      value: q,
      onChange: e => setQ(e.target.value)
    }))), /*#__PURE__*/React.createElement(Tag, {
      onRemove: () => {}
    }, "Balance > 0"), /*#__PURE__*/React.createElement(Button, {
      variant: "ghost",
      size: "sm",
      leftIcon: /*#__PURE__*/React.createElement(Icon, {
        n: "sliders-horizontal"
      })
    }, "Filters"), /*#__PURE__*/React.createElement("div", {
      className: "spacer"
    }), /*#__PURE__*/React.createElement(Button, {
      variant: "secondary",
      size: "sm",
      leftIcon: /*#__PURE__*/React.createElement(Icon, {
        n: "download"
      })
    }, "Export"), /*#__PURE__*/React.createElement(Button, {
      variant: "primary",
      size: "sm",
      leftIcon: /*#__PURE__*/React.createElement(Icon, {
        n: "plus"
      })
    }, "New customer")), /*#__PURE__*/React.createElement(DataTable, {
      getRowId: r => r.no,
      onRowClick: r => go({
        screen: 'customer',
        no: r.no
      }),
      columns: [{
        key: 'no',
        header: 'No.',
        variant: 'doc',
        width: '120px'
      }, {
        key: 'name',
        header: 'Name',
        render: (v, r) => /*#__PURE__*/React.createElement("span", {
          style: {
            display: 'flex',
            alignItems: 'center',
            gap: 10
          }
        }, /*#__PURE__*/React.createElement(Avatar, {
          name: r.name,
          size: "sm"
        }), /*#__PURE__*/React.createElement("span", {
          style: {
            fontWeight: 600,
            color: 'var(--text-strong)'
          }
        }, v))
      }, {
        key: 'city',
        header: 'City',
        width: '150px'
      }, {
        key: 'salesperson',
        header: 'Salesperson',
        width: '150px'
      }, {
        key: 'status',
        header: 'Status',
        width: '120px',
        render: v => /*#__PURE__*/React.createElement(StatusBadge, {
          s: v
        })
      }, {
        key: 'balance',
        header: 'Balance (LCY)',
        variant: 'num',
        align: 'right',
        render: v => D.money(v)
      }],
      rows: rows
    })));
  }

  /* ============ CUSTOMER CARD (signature drill-through) ============ */
  function CustomerCard({
    no,
    go
  }) {
    const c = D.customers.find(x => x.no === no) || D.customers[0];
    const [tab, setTab] = useState('general');
    const custInvoices = D.invoices.filter(i => i.customerNo === c.no);
    return /*#__PURE__*/React.createElement("div", {
      className: "erp-stack"
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-pagehead"
    }, /*#__PURE__*/React.createElement(Avatar, {
      name: c.name,
      size: "lg"
    }), /*#__PURE__*/React.createElement("div", {
      style: {
        flex: 1
      }
    }, /*#__PURE__*/React.createElement("h1", {
      style: {
        display: 'flex',
        alignItems: 'center',
        gap: 12
      }
    }, c.name, " ", /*#__PURE__*/React.createElement(StatusBadge, {
      s: c.status
    })), /*#__PURE__*/React.createElement("div", {
      className: "erp-pagehead__meta erp-num"
    }, c.no, " \xB7 ", c.city, " \xB7 Terms ", c.terms))), /*#__PURE__*/React.createElement("div", {
      className: "erp-record"
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-stack"
    }, /*#__PURE__*/React.createElement(Card, {
      padded: false
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        padding: '0 14px'
      }
    }, /*#__PURE__*/React.createElement(Tabs, {
      value: tab,
      onChange: setTab,
      tabs: [{
        id: 'general',
        label: 'General'
      }, {
        id: 'invoicing',
        label: 'Invoicing'
      }, {
        id: 'documents',
        label: 'Documents',
        count: custInvoices.length
      }]
    })), /*#__PURE__*/React.createElement("div", {
      style: {
        padding: 18
      }
    }, tab === 'general' && /*#__PURE__*/React.createElement("div", {
      className: "erp-grid-2"
    }, /*#__PURE__*/React.createElement(KeyValue, {
      ruled: true,
      collapseKey: `cust-gen-left-${c.no}`,
      items: [{
        key: 'No.',
        value: c.no,
        mono: true
      }, {
        key: 'Name',
        value: c.name
      }, {
        key: 'RNC',
        value: c.rnc || '—',
        mono: true
      }, {
        key: 'Contact',
        value: c.contact
      }, {
        key: 'City',
        value: c.city
      }, {
        key: 'Address',
        value: '27 Calle El Conde',
        secondary: true
      }, {
        key: 'Country',
        value: 'Dominican Republic',
        secondary: true
      }, {
        key: 'Phone',
        value: '+1 809 555 1234',
        secondary: true,
        mono: true
      }]
    }), /*#__PURE__*/React.createElement(KeyValue, {
      ruled: true,
      collapseKey: `cust-gen-right-${c.no}`,
      items: [{
        key: 'Salesperson',
        value: c.salesperson
      }, {
        key: 'Payment terms',
        value: c.terms
      }, {
        key: 'Credit limit (LCY)',
        value: D.money(c.creditLimit),
        mono: true
      }, {
        key: 'Blocked',
        value: c.blocked ? 'Yes — credit hold' : 'No'
      }, {
        key: 'Customer price group',
        value: 'WHOLESALE',
        secondary: true,
        mono: true
      }, {
        key: 'Gen. posting group',
        value: 'DOMESTIC',
        secondary: true,
        mono: true
      }, {
        key: 'Tax area code',
        value: 'DOM-ITBIS',
        secondary: true,
        mono: true
      }]
    })), tab === 'invoicing' && /*#__PURE__*/React.createElement(KeyValue, {
      ruled: true,
      items: [{
        key: 'Balance (LCY)',
        value: D.money(c.balance),
        mono: true
      }, {
        key: 'Overdue (LCY)',
        value: D.money(c.overdue),
        mono: true
      }, {
        key: 'Credit limit (LCY)',
        value: D.money(c.creditLimit),
        mono: true
      }, {
        key: 'Available credit',
        value: D.money(c.creditLimit - c.balance),
        mono: true
      }]
    }), tab === 'documents' && (custInvoices.length ? /*#__PURE__*/React.createElement("div", {
      style: {
        margin: -18
      }
    }, /*#__PURE__*/React.createElement(InvoiceTable, {
      rows: custInvoices,
      go: go
    })) : /*#__PURE__*/React.createElement("div", {
      className: "nx-empty"
    }, /*#__PURE__*/React.createElement("div", {
      className: "nx-empty__icon"
    }, /*#__PURE__*/React.createElement(Icon, {
      n: "file-text"
    })), /*#__PURE__*/React.createElement("div", {
      className: "nx-empty__title"
    }, "No documents")))))), /*#__PURE__*/React.createElement(FactBox, {
      eyebrow: "Customer",
      title: c.name,
      sections: [{
        label: 'Statistics',
        content: /*#__PURE__*/React.createElement(KeyValue, {
          items: [{
            key: 'Balance',
            value: D.money(c.balance),
            mono: true
          }, {
            key: 'Overdue',
            value: D.money(c.overdue),
            mono: true
          }, {
            key: 'Credit limit',
            value: D.money(c.creditLimit),
            mono: true
          }]
        })
      }, {
        label: 'Related',
        content: /*#__PURE__*/React.createElement("ul", {
          className: "nx-linklist"
        }, /*#__PURE__*/React.createElement("li", null, /*#__PURE__*/React.createElement("span", {
          className: "nx-link",
          onClick: () => go({
            screen: 'invoices'
          })
        }, "Posted invoices (", custInvoices.length, ") ", /*#__PURE__*/React.createElement(Icon, {
          n: "arrow-up-right"
        }))), /*#__PURE__*/React.createElement("li", null, /*#__PURE__*/React.createElement("span", {
          className: "nx-link",
          onClick: () => go({
            screen: 'gl'
          })
        }, "Customer ledger entries ", /*#__PURE__*/React.createElement(Icon, {
          n: "arrow-up-right"
        }))), /*#__PURE__*/React.createElement("li", null, /*#__PURE__*/React.createElement("span", {
          className: "nx-link",
          onClick: () => go({
            screen: 'items'
          })
        }, "Items sold ", /*#__PURE__*/React.createElement(Icon, {
          n: "arrow-up-right"
        }))))
      }, {
        label: 'Salesperson',
        content: /*#__PURE__*/React.createElement("div", {
          style: {
            display: 'flex',
            alignItems: 'center',
            gap: 10
          }
        }, /*#__PURE__*/React.createElement(Avatar, {
          name: c.salesperson,
          shape: "circle",
          size: "sm"
        }), /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("div", {
          style: {
            fontSize: 13,
            fontWeight: 600,
            color: 'var(--text-strong)'
          }
        }, c.salesperson), /*#__PURE__*/React.createElement("div", {
          style: {
            fontSize: 12,
            color: 'var(--text-muted)'
          }
        }, "Sales \xB7 RD market")))
      }]
    })));
  }

  /* ============ SALES INVOICE (document) ============ */
  function SalesInvoice({
    no,
    go
  }) {
    const inv = D.invoices.find(x => x.no === no) || D.invoices[0];
    const cust = D.customers.find(c => c.no === inv.customerNo);
    const lines = D.invoiceLines;
    const subtotal = lines.reduce((s, l) => s + l.line, 0);
    const itbis = subtotal * 0.18;
    const total = subtotal + itbis;
    return /*#__PURE__*/React.createElement("div", {
      className: "erp-record"
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-doc"
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-doc__head"
    }, /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("div", {
      className: "nx-factbox__eyebrow"
    }, inv.type), /*#__PURE__*/React.createElement("h1", {
      className: "erp-num",
      style: {
        fontSize: 26,
        marginTop: 2
      }
    }, inv.no), /*#__PURE__*/React.createElement("div", {
      style: {
        display: 'flex',
        gap: 8,
        alignItems: 'center',
        marginTop: 8
      }
    }, /*#__PURE__*/React.createElement(StatusBadge, {
      s: inv.status
    }), inv.ncf && /*#__PURE__*/React.createElement("span", {
      style: {
        fontFamily: 'var(--font-mono)',
        fontSize: 12,
        color: 'var(--text-muted)',
        background: 'var(--surface-sunken)',
        padding: '2px 8px',
        borderRadius: 'var(--radius-xs)',
        border: '1px solid var(--border-subtle)'
      }
    }, "NCF: ", inv.ncf))), /*#__PURE__*/React.createElement("div", {
      style: {
        minWidth: 260
      }
    }, /*#__PURE__*/React.createElement(KeyValue, {
      items: [{
        key: 'Sell-to customer',
        value: /*#__PURE__*/React.createElement("span", {
          className: "nx-link",
          onClick: () => go({
            screen: 'customer',
            no: inv.customerNo
          })
        }, inv.billTo)
      }, {
        key: 'Customer No.',
        value: inv.customerNo,
        mono: true
      }, {
        key: 'Posting date',
        value: inv.postingDate,
        mono: true
      }, {
        key: 'Due date',
        value: inv.dueDate,
        mono: true
      }]
    }))), /*#__PURE__*/React.createElement(DataTable, {
      columns: [{
        key: 'itemNo',
        header: 'Item No.',
        variant: 'doc',
        width: '120px',
        render: v => /*#__PURE__*/React.createElement("span", {
          className: "nx-link",
          onClick: () => go({
            screen: 'items'
          })
        }, v)
      }, {
        key: 'description',
        header: 'Description'
      }, {
        key: 'qty',
        header: 'Qty',
        variant: 'num',
        align: 'right',
        width: '80px'
      }, {
        key: 'uom',
        header: 'Unit',
        width: '80px'
      }, {
        key: 'price',
        header: 'Unit price',
        variant: 'num',
        align: 'right',
        width: '120px',
        render: v => D.money(v)
      }, {
        key: 'line',
        header: 'Line amount',
        variant: 'num',
        align: 'right',
        width: '140px',
        render: v => D.money(v)
      }],
      rows: lines,
      getRowId: r => r.itemNo
    }), /*#__PURE__*/React.createElement("div", {
      className: "erp-doc__totals"
    }, /*#__PURE__*/React.createElement("div", {
      className: "box"
    }, /*#__PURE__*/React.createElement("div", {
      className: "ln"
    }, /*#__PURE__*/React.createElement("span", {
      style: {
        color: 'var(--text-muted)'
      }
    }, "Subtotal"), /*#__PURE__*/React.createElement("span", {
      className: "erp-num"
    }, D.money(subtotal))), /*#__PURE__*/React.createElement("div", {
      className: "ln"
    }, /*#__PURE__*/React.createElement("span", {
      style: {
        color: 'var(--text-muted)'
      }
    }, "ITBIS (", inv.vatPct || 18, "%)"), /*#__PURE__*/React.createElement("span", {
      className: "erp-num"
    }, D.money(itbis))), /*#__PURE__*/React.createElement("div", {
      className: "ln grand"
    }, /*#__PURE__*/React.createElement("span", null, "Total"), /*#__PURE__*/React.createElement("span", {
      className: "erp-num"
    }, D.money(total)))))), /*#__PURE__*/React.createElement(FactBox, {
      eyebrow: "Bill-to customer",
      title: inv.billTo,
      sections: [{
        label: 'Customer',
        content: /*#__PURE__*/React.createElement(KeyValue, {
          items: [{
            key: 'No.',
            value: inv.customerNo,
            mono: true
          }, {
            key: 'Balance',
            value: D.money(cust ? cust.balance : 0),
            mono: true
          }, {
            key: 'Credit limit',
            value: D.money(cust ? cust.creditLimit : 0),
            mono: true
          }]
        })
      }, {
        label: 'Related',
        content: /*#__PURE__*/React.createElement("ul", {
          className: "nx-linklist"
        }, /*#__PURE__*/React.createElement("li", null, /*#__PURE__*/React.createElement("span", {
          className: "nx-link",
          onClick: () => go({
            screen: 'customer',
            no: inv.customerNo
          })
        }, "Open customer card ", /*#__PURE__*/React.createElement(Icon, {
          n: "arrow-up-right"
        }))), /*#__PURE__*/React.createElement("li", null, /*#__PURE__*/React.createElement("span", {
          className: "nx-link",
          onClick: () => go({
            screen: 'items'
          })
        }, "Item availability ", /*#__PURE__*/React.createElement(Icon, {
          n: "arrow-up-right"
        }))))
      }]
    }));
  }

  /* ============ ITEM LIST ============ */
  function ItemList({
    go
  }) {
    return /*#__PURE__*/React.createElement("div", {
      className: "erp-stack"
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-pagehead"
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        flex: 1
      }
    }, /*#__PURE__*/React.createElement("h1", null, "Items"), /*#__PURE__*/React.createElement("div", {
      className: "erp-pagehead__meta"
    }, D.items.length, " records \xB7 Inventory module"))), /*#__PURE__*/React.createElement(Card, {
      padded: false
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-toolbar"
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        width: 260
      }
    }, /*#__PURE__*/React.createElement("span", {
      className: "nx-inputgroup"
    }, /*#__PURE__*/React.createElement("span", {
      className: "nx-adorn"
    }, /*#__PURE__*/React.createElement(Icon, {
      n: "search"
    })), /*#__PURE__*/React.createElement("input", {
      className: "nx-input",
      placeholder: "Search items\u2026"
    }))), /*#__PURE__*/React.createElement("div", {
      className: "spacer"
    }), /*#__PURE__*/React.createElement(Button, {
      variant: "primary",
      size: "sm",
      leftIcon: /*#__PURE__*/React.createElement(Icon, {
        n: "plus"
      })
    }, "New item")), /*#__PURE__*/React.createElement(DataTable, {
      getRowId: r => r.no,
      columns: [{
        key: 'no',
        header: 'No.',
        variant: 'doc',
        width: '120px'
      }, {
        key: 'description',
        header: 'Description'
      }, {
        key: 'uom',
        header: 'Base UoM',
        width: '110px'
      }, {
        key: 'onHand',
        header: 'On hand',
        variant: 'num',
        align: 'right',
        width: '120px',
        render: (v, r) => /*#__PURE__*/React.createElement("span", {
          style: {
            color: v === 0 ? 'var(--money-negative)' : 'var(--text-body)'
          },
          className: "erp-num"
        }, v.toLocaleString())
      }, {
        key: 'price',
        header: 'Unit price',
        variant: 'num',
        align: 'right',
        width: '130px',
        render: v => D.money(v)
      }, {
        key: 'blocked',
        header: 'Status',
        width: '110px',
        render: v => v ? /*#__PURE__*/React.createElement(Badge, {
          status: "danger",
          dot: true
        }, "Blocked") : /*#__PURE__*/React.createElement(Badge, {
          status: "success",
          dot: true
        }, "Active")
      }],
      rows: D.items
    })));
  }

  /* ============ GENERIC PLACEHOLDER for un-built modules ============ */
  function Placeholder({
    title,
    go
  }) {
    return /*#__PURE__*/React.createElement("div", {
      className: "erp-stack"
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-pagehead"
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        flex: 1
      }
    }, /*#__PURE__*/React.createElement("h1", null, title))), /*#__PURE__*/React.createElement(Card, null, /*#__PURE__*/React.createElement("div", {
      className: "nx-empty"
    }, /*#__PURE__*/React.createElement("div", {
      className: "nx-empty__icon"
    }, /*#__PURE__*/React.createElement(Icon, {
      n: "hammer"
    })), /*#__PURE__*/React.createElement("div", {
      className: "nx-empty__title"
    }, "Module preview not included in this kit"), /*#__PURE__*/React.createElement("div", {
      className: "nx-empty__text"
    }, "This UI kit recreates the Sales & Inventory surfaces. ", title, " follows the same shell, list and FactBox patterns."), /*#__PURE__*/React.createElement(Button, {
      variant: "secondary",
      onClick: () => go({
        screen: 'dashboard'
      })
    }, "Back to dashboard"))));
  }

  /* ============ SETTINGS ============ */
  function Settings({
    go
  }) {
    const [tab, setTab] = useState('empresa');
    const {
      Switch,
      Checkbox
    } = NS;

    /* --- mock users with per-permission state --- */
    const [users, setUsers] = useState([{
      id: 1,
      name: 'José Ramírez',
      email: 'jramirez@acmedist.do',
      initials: 'JR',
      active: true,
      perms: {
        ver_ventas: true,
        editar_ventas: true,
        contabilizar: true,
        ver_compras: true,
        editar_compras: false,
        ver_contabilidad: true,
        editar_contabilidad: false,
        ver_usuarios: false,
        configuracion: false
      }
    }, {
      id: 2,
      name: 'Laura Méndez',
      email: 'lmendez@acmedist.do',
      initials: 'LM',
      active: true,
      perms: {
        ver_ventas: true,
        editar_ventas: true,
        contabilizar: false,
        ver_compras: false,
        editar_compras: false,
        ver_contabilidad: false,
        editar_contabilidad: false,
        ver_usuarios: false,
        configuracion: false
      }
    }, {
      id: 3,
      name: 'Carla Objío',
      email: 'cobjio@acmedist.do',
      initials: 'CO',
      active: true,
      perms: {
        ver_ventas: true,
        editar_ventas: false,
        contabilizar: false,
        ver_compras: true,
        editar_compras: true,
        ver_contabilidad: false,
        editar_contabilidad: false,
        ver_usuarios: false,
        configuracion: false
      }
    }]);

    /* --- mock modules state --- */
    const [modules, setModules] = useState([{
      id: 'ventas',
      label: 'Ventas',
      desc: 'Clientes, facturas, notas de crédito y cobros.',
      icon: 'file-text',
      on: true
    }, {
      id: 'compras',
      label: 'Compras',
      desc: 'Proveedores, órdenes de compra y cuentas por pagar.',
      icon: 'shopping-cart',
      on: true
    }, {
      id: 'inventario',
      label: 'Inventario',
      desc: 'Artículos, stock por almacén y movimientos.',
      icon: 'package',
      on: true
    }, {
      id: 'contabilidad',
      label: 'Contabilidad',
      desc: 'Plan de cuentas, asientos y estados financieros.',
      icon: 'book-open',
      on: true
    }, {
      id: 'bancos',
      label: 'Bancos',
      desc: 'Cuentas bancarias, conciliación y pagos.',
      icon: 'landmark',
      on: false
    }, {
      id: 'pos',
      label: 'Punto de venta',
      desc: 'Caja registradora para ventas en mostrador.',
      icon: 'receipt',
      on: false
    }]);
    const togglePerm = (userId, perm) => {
      setUsers(us => us.map(u => u.id !== userId ? u : {
        ...u,
        perms: {
          ...u.perms,
          [perm]: !u.perms[perm]
        }
      }));
    };
    const toggleModule = id => setModules(ms => ms.map(m => m.id === id ? {
      ...m,
      on: !m.on
    } : m));
    const PERMS = [{
      id: 'ver_ventas',
      label: 'Ver ventas',
      group: 'Ventas'
    }, {
      id: 'editar_ventas',
      label: 'Crear y editar facturas',
      group: 'Ventas'
    }, {
      id: 'contabilizar',
      label: 'Contabilizar documentos',
      group: 'Ventas'
    }, {
      id: 'ver_compras',
      label: 'Ver compras',
      group: 'Compras'
    }, {
      id: 'editar_compras',
      label: 'Crear órdenes de compra',
      group: 'Compras'
    }, {
      id: 'ver_contabilidad',
      label: 'Ver contabilidad',
      group: 'Finanzas'
    }, {
      id: 'editar_contabilidad',
      label: 'Registrar asientos',
      group: 'Finanzas'
    }, {
      id: 'ver_usuarios',
      label: 'Ver otros usuarios',
      group: 'Configuración'
    }, {
      id: 'configuracion',
      label: 'Cambiar configuración',
      group: 'Configuración'
    }];
    const permGroups = [...new Set(PERMS.map(p => p.group))];
    const MENU = [{
      id: 'empresa',
      label: 'Mi empresa',
      icon: 'building-2'
    }, {
      id: 'modulos',
      label: 'Módulos activos',
      icon: 'layout-grid'
    }, {
      id: 'usuarios',
      label: 'Usuarios y permisos',
      icon: 'users'
    }, {
      id: 'numeracion',
      label: 'Numeración',
      icon: 'hash'
    }, {
      id: 'impuestos',
      label: 'Impuestos',
      icon: 'percent'
    }];
    return /*#__PURE__*/React.createElement("div", {
      className: "erp-stack"
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-pagehead"
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        flex: 1
      }
    }, /*#__PURE__*/React.createElement("h1", null, "Configuraci\xF3n"), /*#__PURE__*/React.createElement("div", {
      className: "erp-pagehead__meta"
    }, "Acme Distribuci\xF3n \xB7 RNC 1-01-12345-0"))), /*#__PURE__*/React.createElement("div", {
      style: {
        display: 'grid',
        gridTemplateColumns: '210px 1fr',
        gap: 24,
        alignItems: 'start'
      }
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        display: 'flex',
        flexDirection: 'column',
        gap: 1
      }
    }, MENU.map(m => /*#__PURE__*/React.createElement("button", {
      key: m.id,
      onClick: () => setTab(m.id),
      style: {
        display: 'flex',
        alignItems: 'center',
        gap: 10,
        padding: '9px 12px',
        borderRadius: 'var(--radius-sm)',
        border: 'none',
        cursor: 'pointer',
        fontSize: 13.5,
        fontWeight: 500,
        textAlign: 'left',
        transition: 'all 120ms',
        background: tab === m.id ? 'var(--surface-selected)' : 'transparent',
        color: tab === m.id ? 'var(--action)' : 'var(--text-body)'
      }
    }, /*#__PURE__*/React.createElement(Icon, {
      n: m.icon,
      style: {
        width: 16,
        height: 16,
        opacity: tab === m.id ? 1 : 0.6
      }
    }), m.label))), /*#__PURE__*/React.createElement("div", null, tab === 'empresa' && /*#__PURE__*/React.createElement(Card, {
      title: "Mi empresa"
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-grid-2",
      style: {
        gap: 20
      }
    }, /*#__PURE__*/React.createElement(Input, {
      label: "Nombre de la empresa",
      defaultValue: "Acme Distribuci\xF3n, S.R.L."
    }), /*#__PURE__*/React.createElement(Input, {
      label: "RNC",
      mono: true,
      defaultValue: "1-01-12345-0"
    }), /*#__PURE__*/React.createElement(Input, {
      label: "Correo principal",
      defaultValue: "admin@acmedist.do"
    }), /*#__PURE__*/React.createElement(Input, {
      label: "Tel\xE9fono",
      defaultValue: "(809) 555-0100"
    }), /*#__PURE__*/React.createElement(Input, {
      label: "Direcci\xF3n",
      defaultValue: "Av. Winston Churchill, Santo Domingo"
    }), /*#__PURE__*/React.createElement(Input, {
      label: "Ciudad",
      defaultValue: "Santo Domingo"
    })), /*#__PURE__*/React.createElement("div", {
      style: {
        marginTop: 20,
        paddingTop: 16,
        borderTop: '1px solid var(--border-subtle)',
        display: 'flex',
        justifyContent: 'flex-end',
        gap: 10
      }
    }, /*#__PURE__*/React.createElement(Button, {
      variant: "secondary"
    }, "Cancelar"), /*#__PURE__*/React.createElement(Button, {
      variant: "primary"
    }, "Guardar cambios"))), tab === 'modulos' && /*#__PURE__*/React.createElement(Card, {
      title: "M\xF3dulos activos",
      eyebrow: "Activ\xE1 solo lo que us\xE1s"
    }, /*#__PURE__*/React.createElement("p", {
      style: {
        fontSize: 13.5,
        color: 'var(--text-muted)',
        marginBottom: 20,
        lineHeight: 1.65
      }
    }, "El sistema se adapta a tu operaci\xF3n. Desactiv\xE1 los m\xF3dulos que no us\xE1s \u2014 no aparecer\xE1n en el men\xFA ni en la b\xFAsqueda. Pod\xE9s reactivarlos en cualquier momento."), /*#__PURE__*/React.createElement("div", {
      style: {
        display: 'flex',
        flexDirection: 'column',
        gap: 1
      }
    }, modules.map(m => /*#__PURE__*/React.createElement("div", {
      key: m.id,
      style: {
        display: 'flex',
        alignItems: 'center',
        gap: 14,
        padding: '14px 16px',
        background: 'var(--surface-sunken)',
        borderRadius: 'var(--radius-sm)',
        border: '1px solid var(--border-subtle)',
        marginBottom: 8
      }
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        width: 36,
        height: 36,
        borderRadius: 8,
        background: m.on ? 'var(--action-tint)' : 'var(--slate-100)',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        flex: 'none'
      }
    }, /*#__PURE__*/React.createElement(Icon, {
      n: m.icon,
      style: {
        width: 17,
        height: 17,
        color: m.on ? 'var(--action)' : 'var(--text-faint)'
      }
    })), /*#__PURE__*/React.createElement("div", {
      style: {
        flex: 1,
        minWidth: 0
      }
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        fontSize: 14,
        fontWeight: 600,
        color: 'var(--text-strong)'
      }
    }, m.label), /*#__PURE__*/React.createElement("div", {
      style: {
        fontSize: 12.5,
        color: 'var(--text-muted)',
        marginTop: 2
      }
    }, m.desc)), /*#__PURE__*/React.createElement(Switch, {
      checked: m.on,
      onChange: () => toggleModule(m.id)
    }))))), tab === 'usuarios' && /*#__PURE__*/React.createElement(Card, {
      title: "Usuarios y permisos",
      eyebrow: "Qu\xE9 puede ver y hacer cada persona",
      padded: false
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        padding: '12px 18px 14px',
        borderBottom: '1px solid var(--border-subtle)'
      }
    }, /*#__PURE__*/React.createElement("p", {
      style: {
        fontSize: 13,
        color: 'var(--text-muted)',
        lineHeight: 1.65
      }
    }, "Aqu\xED no hay \"tipos de usuario\". Cada persona tiene sus propios permisos \u2014 control\xE1s exactamente qu\xE9 puede ver y qu\xE9 puede hacer, sin depender de categor\xEDas fijas.")), /*#__PURE__*/React.createElement("div", {
      style: {
        overflowX: 'auto'
      }
    }, /*#__PURE__*/React.createElement("table", {
      style: {
        width: '100%',
        borderCollapse: 'collapse',
        fontSize: 13
      }
    }, /*#__PURE__*/React.createElement("thead", null, /*#__PURE__*/React.createElement("tr", null, /*#__PURE__*/React.createElement("th", {
      style: {
        textAlign: 'left',
        padding: '10px 18px',
        fontWeight: 600,
        fontSize: 12,
        color: 'var(--text-muted)',
        borderBottom: '1px solid var(--border-default)',
        background: 'var(--surface-sunken)',
        position: 'sticky',
        left: 0,
        minWidth: 200
      }
    }, "Usuario"), permGroups.map(g => /*#__PURE__*/React.createElement("th", {
      key: g,
      colSpan: PERMS.filter(p => p.group === g).length,
      style: {
        textAlign: 'center',
        padding: '10px 8px',
        fontWeight: 700,
        fontSize: 10,
        letterSpacing: '0.06em',
        textTransform: 'uppercase',
        color: 'var(--text-faint)',
        borderBottom: '1px solid var(--border-default)',
        background: 'var(--surface-sunken)',
        whiteSpace: 'nowrap',
        borderLeft: '1px solid var(--border-subtle)'
      }
    }, g))), /*#__PURE__*/React.createElement("tr", null, /*#__PURE__*/React.createElement("th", {
      style: {
        background: 'var(--surface-sunken)',
        borderBottom: '1px solid var(--border-default)',
        position: 'sticky',
        left: 0
      }
    }), PERMS.map((p, i) => /*#__PURE__*/React.createElement("th", {
      key: p.id,
      style: {
        padding: '6px 10px',
        fontSize: 11,
        fontWeight: 500,
        color: 'var(--text-muted)',
        borderBottom: '1px solid var(--border-default)',
        background: 'var(--surface-sunken)',
        whiteSpace: 'nowrap',
        textAlign: 'center',
        borderLeft: i === 0 || PERMS[i - 1]?.group !== p.group ? '1px solid var(--border-subtle)' : 'none',
        maxWidth: 110
      }
    }, p.label)))), /*#__PURE__*/React.createElement("tbody", null, users.map((u, ui) => /*#__PURE__*/React.createElement("tr", {
      key: u.id,
      style: {
        borderBottom: '1px solid var(--border-subtle)'
      }
    }, /*#__PURE__*/React.createElement("td", {
      style: {
        padding: '12px 18px',
        background: 'var(--surface)',
        position: 'sticky',
        left: 0
      }
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        display: 'flex',
        alignItems: 'center',
        gap: 10
      }
    }, /*#__PURE__*/React.createElement(Avatar, {
      name: u.name,
      shape: "circle",
      size: "sm"
    }), /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("div", {
      style: {
        fontWeight: 600,
        color: 'var(--text-strong)',
        fontSize: 13
      }
    }, u.name), /*#__PURE__*/React.createElement("div", {
      style: {
        fontSize: 11.5,
        color: 'var(--text-muted)'
      }
    }, u.email)))), PERMS.map((p, pi) => /*#__PURE__*/React.createElement("td", {
      key: p.id,
      style: {
        textAlign: 'center',
        padding: '12px 10px',
        borderLeft: pi === 0 || PERMS[pi - 1]?.group !== p.group ? '1px solid var(--border-subtle)' : 'none'
      }
    }, /*#__PURE__*/React.createElement(Checkbox, {
      checked: u.perms[p.id],
      onChange: () => togglePerm(u.id, p.id),
      style: {
        justifyContent: 'center'
      }
    })))))))), /*#__PURE__*/React.createElement("div", {
      style: {
        padding: '12px 18px',
        borderTop: '1px solid var(--border-subtle)',
        display: 'flex',
        justifyContent: 'flex-end'
      }
    }, /*#__PURE__*/React.createElement(Button, {
      variant: "primary",
      leftIcon: /*#__PURE__*/React.createElement(Icon, {
        n: "user-plus"
      })
    }, "Agregar usuario"))), tab === 'numeracion' && /*#__PURE__*/React.createElement(Card, {
      title: "Numeraci\xF3n de documentos"
    }, /*#__PURE__*/React.createElement("p", {
      style: {
        fontSize: 13.5,
        color: 'var(--text-muted)',
        marginBottom: 20,
        lineHeight: 1.65
      }
    }, "Configur\xE1 las series de NCF y los secuenciales de tus documentos. El sistema asigna el n\xFAmero siguiente autom\xE1ticamente."), /*#__PURE__*/React.createElement("div", {
      style: {
        display: 'flex',
        flexDirection: 'column',
        gap: 12
      }
    }, [{
      tipo: 'Facturas de venta',
      serie: 'B01',
      desde: '00000001',
      hasta: '00009999',
      actual: '00000847'
    }, {
      tipo: 'Notas de crédito',
      serie: 'B04',
      desde: '00000001',
      hasta: '00001999',
      actual: '00000219'
    }, {
      tipo: 'Compras y gastos',
      serie: 'B11',
      desde: '00000001',
      hasta: '00001999',
      actual: '00000042'
    }].map(row => /*#__PURE__*/React.createElement("div", {
      key: row.serie,
      style: {
        display: 'grid',
        gridTemplateColumns: '1fr 90px 130px 130px 130px',
        gap: 12,
        alignItems: 'end',
        paddingBottom: 16,
        borderBottom: '1px solid var(--border-subtle)'
      }
    }, /*#__PURE__*/React.createElement(Input, {
      label: "Tipo de comprobante",
      defaultValue: row.tipo
    }), /*#__PURE__*/React.createElement(Input, {
      label: "Serie",
      mono: true,
      defaultValue: row.serie
    }), /*#__PURE__*/React.createElement(Input, {
      label: "Desde",
      mono: true,
      defaultValue: row.desde
    }), /*#__PURE__*/React.createElement(Input, {
      label: "Hasta",
      mono: true,
      defaultValue: row.hasta
    }), /*#__PURE__*/React.createElement(Input, {
      label: "N\xFAmero actual",
      mono: true,
      defaultValue: row.actual,
      hint: "Pr\xF3ximo: +1"
    })))), /*#__PURE__*/React.createElement("div", {
      style: {
        marginTop: 20,
        display: 'flex',
        justifyContent: 'flex-end',
        gap: 10
      }
    }, /*#__PURE__*/React.createElement(Button, {
      variant: "secondary"
    }, "Cancelar"), /*#__PURE__*/React.createElement(Button, {
      variant: "primary"
    }, "Guardar"))), tab === 'impuestos' && /*#__PURE__*/React.createElement(Card, {
      title: "Impuestos"
    }, /*#__PURE__*/React.createElement("p", {
      style: {
        fontSize: 13.5,
        color: 'var(--text-muted)',
        marginBottom: 20,
        lineHeight: 1.65
      }
    }, "Configur\xE1 los grupos de impuestos que se aplican a tus art\xEDculos y servicios. El ITBIS se calcula autom\xE1ticamente en cada l\xEDnea de documento."), /*#__PURE__*/React.createElement("div", {
      style: {
        display: 'flex',
        flexDirection: 'column',
        gap: 10
      }
    }, [{
      codigo: 'ITBIS-18',
      nombre: 'ITBIS 18%',
      tasa: '18.00',
      aplica: 'Mayoría de bienes y servicios'
    }, {
      codigo: 'ITBIS-16',
      nombre: 'ITBIS 16%',
      tasa: '16.00',
      aplica: 'Servicios financieros y de seguros'
    }, {
      codigo: 'EXENTO',
      nombre: 'Exento',
      tasa: '0.00',
      aplica: 'Alimentos básicos, medicamentos, exportaciones'
    }].map(row => /*#__PURE__*/React.createElement("div", {
      key: row.codigo,
      style: {
        display: 'grid',
        gridTemplateColumns: '110px 1fr 100px 1fr',
        gap: 12,
        alignItems: 'end',
        paddingBottom: 16,
        borderBottom: '1px solid var(--border-subtle)'
      }
    }, /*#__PURE__*/React.createElement(Input, {
      label: "C\xF3digo",
      mono: true,
      defaultValue: row.codigo
    }), /*#__PURE__*/React.createElement(Input, {
      label: "Nombre",
      defaultValue: row.nombre
    }), /*#__PURE__*/React.createElement(Input, {
      label: "Tasa %",
      mono: true,
      defaultValue: row.tasa
    }), /*#__PURE__*/React.createElement(Input, {
      label: "Aplicable a",
      defaultValue: row.aplica
    })))), /*#__PURE__*/React.createElement("div", {
      style: {
        marginTop: 20,
        display: 'flex',
        justifyContent: 'flex-end',
        gap: 10
      }
    }, /*#__PURE__*/React.createElement(Button, {
      variant: "secondary"
    }, "Cancelar"), /*#__PURE__*/React.createElement(Button, {
      variant: "primary"
    }, "Guardar"))))));
  }
  Object.assign(window, {
    NXDashboard: Dashboard,
    NXCustomerList: CustomerList,
    NXCustomerCard: CustomerCard,
    NXSalesInvoice: SalesInvoice,
    NXItemList: ItemList,
    NXPlaceholder: Placeholder,
    NXSettings: Settings
  });
})();
})(); } catch (e) { __ds_ns.__errors.push({ path: "ui_kits/erp/screens.jsx", error: String((e && e.message) || e) }); }

// ui_kits/erp/shell.jsx
try { (() => {
function _extends() { return _extends = Object.assign ? Object.assign.bind() : function (n) { for (var e = 1; e < arguments.length; e++) { var t = arguments[e]; for (var r in t) ({}).hasOwnProperty.call(t, r) && (n[r] = t[r]); } return n; }, _extends.apply(null, arguments); }
/* Nexus Billing ERP — shell: sidebar (with quick nav + pinned shortcuts),
 * command palette (⌘K), topbar, command bar, login. */
(function () {
  const NS = window.NexusBillingDesignSystem_f29fd0 || {};
  const {
    Button,
    IconButton,
    Avatar,
    Breadcrumb
  } = NS;
  const {
    useEffect,
    useState,
    useRef,
    useCallback
  } = React;

  /* ---- Theme ---- */
  const THEME_KEY = 'nx-theme';
  const applyTheme = dark => {
    document.documentElement.setAttribute('data-theme', dark ? 'dark' : 'light');
    localStorage.setItem(THEME_KEY, dark ? 'dark' : 'light');
  };
  const isDarkStored = () => {
    const s = localStorage.getItem(THEME_KEY);
    return s ? s === 'dark' : window.matchMedia('(prefers-color-scheme: dark)').matches;
  };
  applyTheme(isDarkStored());
  window.NXApplyTheme = applyTheme;
  window.NXIsDarkStored = isDarkStored;
  const Icon = ({
    n,
    ...p
  }) => /*#__PURE__*/React.createElement("i", _extends({
    "data-lucide": n
  }, p));
  window.NXIcon = Icon;
  function useLucide() {
    useEffect(() => {
      const t = setTimeout(() => window.lucide && window.lucide.createIcons(), 0);
      return () => clearTimeout(t);
    });
  }
  window.useLucide = useLucide;

  /* ---- Pinned shortcuts persistence ---- */
  const PINS_KEY = 'nx-pinned';
  const DEFAULT_PINS = ['dashboard', 'invoices'];
  const loadPins = () => {
    try {
      return JSON.parse(localStorage.getItem(PINS_KEY)) || DEFAULT_PINS;
    } catch {
      return DEFAULT_PINS;
    }
  };
  const savePins = p => localStorage.setItem(PINS_KEY, JSON.stringify(p));

  /* ---- All searchable views ---- */
  const ALL_VIEWS = [{
    id: 'dashboard',
    label: 'Dashboard',
    icon: 'layout-dashboard',
    group: 'Vistas',
    desc: 'KPIs, actividad reciente, AR aging'
  }, {
    id: 'customers',
    label: 'Clientes',
    icon: 'users',
    group: 'Ventas',
    desc: 'Fichas de clientes, balance y cobros'
  }, {
    id: 'invoices',
    label: 'Facturas de venta',
    icon: 'file-text',
    group: 'Ventas',
    desc: 'Facturas, notas de crédito, NCF'
  }, {
    id: 'vendors',
    label: 'Proveedores',
    icon: 'truck',
    group: 'Compras',
    desc: 'Fichas de proveedores, cuentas por pagar'
  }, {
    id: 'porders',
    label: 'Órdenes de compra',
    icon: 'shopping-cart',
    group: 'Compras',
    desc: 'Órdenes, recepciones de mercancía'
  }, {
    id: 'items',
    label: 'Artículos',
    icon: 'package',
    group: 'Inventario',
    desc: 'Artículos, precios, stock disponible'
  }, {
    id: 'locations',
    label: 'Almacenes',
    icon: 'map-pin',
    group: 'Inventario',
    desc: 'Ubicaciones y stock por almacén'
  }, {
    id: 'gl',
    label: 'Plan de cuentas',
    icon: 'book-open',
    group: 'Contabilidad',
    desc: 'Cuentas contables y asientos'
  }];
  const ALL_ACTIONS = [{
    id: 'act-newinv',
    label: 'Nueva factura',
    icon: 'plus-circle',
    group: 'Acciones',
    screen: 'invoice',
    no: 'SI-2026-001847',
    desc: 'Crear una nueva factura de venta'
  }, {
    id: 'act-newcust',
    label: 'Nuevo cliente',
    icon: 'user-plus',
    group: 'Acciones',
    screen: 'customers',
    desc: 'Registrar un nuevo cliente'
  }, {
    id: 'act-newitem',
    label: 'Nuevo artículo',
    icon: 'package-plus',
    group: 'Acciones',
    screen: 'items',
    desc: 'Crear un artículo en inventario'
  }];

  /* ---- Sidebar nav tree ---- */
  const NAV = [{
    group: null,
    items: [{
      id: 'dashboard',
      label: 'Dashboard',
      icon: 'layout-dashboard'
    }]
  }, {
    group: 'Ventas',
    items: [{
      id: 'customers',
      label: 'Clientes',
      icon: 'users',
      count: '6'
    }, {
      id: 'invoices',
      label: 'Facturas',
      icon: 'file-text',
      count: '18'
    }]
  }, {
    group: 'Compras',
    items: [{
      id: 'vendors',
      label: 'Proveedores',
      icon: 'truck'
    }, {
      id: 'porders',
      label: 'Ó. de compra',
      icon: 'shopping-cart'
    }]
  }, {
    group: 'Inventario',
    items: [{
      id: 'items',
      label: 'Artículos',
      icon: 'package'
    }, {
      id: 'locations',
      label: 'Almacenes',
      icon: 'map-pin'
    }]
  }, {
    group: 'Contabilidad',
    items: [{
      id: 'gl',
      label: 'Plan de cuentas',
      icon: 'book-open'
    }]
  }, {
    group: null,
    items: [{
      id: 'settings',
      label: 'Configuración',
      icon: 'settings-2'
    }]
  }];
  const allNavItems = NAV.flatMap(s => s.items);
  const viewMeta = id => ALL_VIEWS.find(v => v.id === id) || allNavItems.find(n => n.id === id) || {
    label: id,
    icon: 'circle'
  };

  /* ============================================================
   * COMMAND PALETTE
   * ============================================================ */
  function CommandPalette({
    onClose,
    go,
    pins,
    onPinToggle
  }) {
    const [q, setQ] = useState('');
    const [cursor, setCursor] = useState(0);
    const inputRef = useRef(null);
    useLucide();
    useEffect(() => {
      setTimeout(() => inputRef.current?.focus(), 30);
    }, []);

    // Build results
    const lq = q.toLowerCase();
    const matchViews = ALL_VIEWS.filter(v => !lq || v.label.toLowerCase().includes(lq) || v.desc.toLowerCase().includes(lq));
    const matchActions = ALL_ACTIONS.filter(a => !lq || a.label.toLowerCase().includes(lq) || a.desc.toLowerCase().includes(lq));
    const results = [...matchViews, ...matchActions];
    const select = useCallback(item => {
      if (item.screen) go({
        screen: item.screen,
        no: item.no
      });else go({
        screen: item.id
      });
      onClose();
    }, [go, onClose]);
    useEffect(() => {
      setCursor(0);
    }, [q]);
    const onKey = e => {
      if (e.key === 'Escape') {
        onClose();
        return;
      }
      if (e.key === 'ArrowDown') {
        e.preventDefault();
        setCursor(c => Math.min(c + 1, results.length - 1));
      }
      if (e.key === 'ArrowUp') {
        e.preventDefault();
        setCursor(c => Math.max(c - 1, 0));
      }
      if (e.key === 'Enter' && results[cursor]) select(results[cursor]);
    };
    const grouped = {};
    results.forEach(r => {
      (grouped[r.group] = grouped[r.group] || []).push(r);
    });
    return /*#__PURE__*/React.createElement("div", {
      style: {
        position: 'fixed',
        inset: 0,
        background: 'var(--scrim)',
        zIndex: 500,
        display: 'flex',
        alignItems: 'flex-start',
        justifyContent: 'center',
        paddingTop: 80
      },
      onClick: onClose
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        width: '100%',
        maxWidth: 560,
        background: 'var(--surface)',
        border: '1px solid var(--border-default)',
        borderRadius: 'var(--radius-lg)',
        boxShadow: 'var(--shadow-xl)',
        overflow: 'hidden'
      },
      onClick: e => e.stopPropagation()
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        display: 'flex',
        alignItems: 'center',
        gap: 10,
        padding: '0 16px',
        borderBottom: '1px solid var(--border-subtle)',
        height: 52
      }
    }, /*#__PURE__*/React.createElement("svg", {
      width: "16",
      height: "16",
      viewBox: "0 0 24 24",
      fill: "none",
      stroke: "var(--text-faint)",
      strokeWidth: "2",
      strokeLinecap: "round"
    }, /*#__PURE__*/React.createElement("circle", {
      cx: "11",
      cy: "11",
      r: "8"
    }), /*#__PURE__*/React.createElement("path", {
      d: "m21 21-4.35-4.35"
    })), /*#__PURE__*/React.createElement("input", {
      ref: inputRef,
      value: q,
      onChange: e => setQ(e.target.value),
      onKeyDown: onKey,
      placeholder: "Buscar vistas, clientes, facturas\u2026",
      style: {
        flex: 1,
        border: 'none',
        background: 'none',
        outline: 'none',
        font: 'inherit',
        fontSize: 15,
        color: 'var(--text-strong)'
      }
    }), q && /*#__PURE__*/React.createElement("button", {
      onClick: () => setQ(''),
      style: {
        background: 'none',
        border: 'none',
        cursor: 'pointer',
        color: 'var(--text-faint)',
        display: 'flex',
        padding: 2
      }
    }, /*#__PURE__*/React.createElement("svg", {
      width: "14",
      height: "14",
      viewBox: "0 0 24 24",
      fill: "none",
      stroke: "currentColor",
      strokeWidth: "2",
      strokeLinecap: "round"
    }, /*#__PURE__*/React.createElement("path", {
      d: "M18 6 6 18M6 6l12 12"
    }))), /*#__PURE__*/React.createElement("kbd", {
      style: {
        fontFamily: 'var(--font-mono)',
        fontSize: 11,
        background: 'var(--surface-sunken)',
        border: '1px solid var(--border-subtle)',
        borderRadius: 4,
        padding: '2px 6px',
        color: 'var(--text-faint)'
      }
    }, "Esc")), /*#__PURE__*/React.createElement("div", {
      style: {
        maxHeight: 400,
        overflowY: 'auto'
      }
    }, results.length === 0 && /*#__PURE__*/React.createElement("div", {
      style: {
        padding: '32px 20px',
        textAlign: 'center',
        color: 'var(--text-muted)',
        fontSize: 13
      }
    }, "Sin resultados para \"", q, "\""), Object.entries(grouped).map(([grp, items]) => /*#__PURE__*/React.createElement("div", {
      key: grp
    }, /*#__PURE__*/React.createElement("div", {
      style: {
        padding: '10px 16px 4px',
        fontSize: 10.5,
        fontWeight: 700,
        letterSpacing: '0.08em',
        textTransform: 'uppercase',
        color: 'var(--text-faint)'
      }
    }, grp), items.map(item => {
      const idx = results.indexOf(item);
      const isPinned = pins.includes(item.id);
      const isView = item.group !== 'Acciones';
      return /*#__PURE__*/React.createElement("div", {
        key: item.id,
        style: {
          display: 'flex',
          alignItems: 'center',
          gap: 12,
          padding: '9px 16px',
          cursor: 'pointer',
          background: idx === cursor ? 'var(--surface-hover)' : 'transparent',
          transition: 'background 80ms'
        },
        onMouseEnter: () => setCursor(idx),
        onClick: () => select(item)
      }, /*#__PURE__*/React.createElement("div", {
        style: {
          width: 30,
          height: 30,
          borderRadius: 'var(--radius-sm)',
          background: 'var(--surface-sunken)',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          color: 'var(--text-muted)',
          flex: 'none'
        }
      }, /*#__PURE__*/React.createElement("i", {
        "data-lucide": item.icon,
        style: {
          width: 15,
          height: 15
        }
      })), /*#__PURE__*/React.createElement("div", {
        style: {
          flex: 1,
          minWidth: 0
        }
      }, /*#__PURE__*/React.createElement("div", {
        style: {
          fontSize: 13.5,
          fontWeight: 600,
          color: 'var(--text-strong)'
        }
      }, item.label), /*#__PURE__*/React.createElement("div", {
        style: {
          fontSize: 12,
          color: 'var(--text-muted)',
          marginTop: 1
        }
      }, item.desc)), isView && /*#__PURE__*/React.createElement("button", {
        onClick: e => {
          e.stopPropagation();
          onPinToggle(item.id);
        },
        title: isPinned ? 'Quitar atajo' : 'Agregar como atajo',
        style: {
          background: isPinned ? 'var(--action-tint)' : 'none',
          border: '1px solid ' + (isPinned ? 'var(--action-ring)' : 'var(--border-subtle)'),
          borderRadius: 'var(--radius-xs)',
          cursor: 'pointer',
          color: isPinned ? 'var(--action)' : 'var(--text-faint)',
          padding: '3px 8px',
          fontSize: 11,
          fontWeight: 600,
          display: 'flex',
          alignItems: 'center',
          gap: 4,
          whiteSpace: 'nowrap',
          transition: 'all 120ms'
        }
      }, /*#__PURE__*/React.createElement("svg", {
        width: "11",
        height: "11",
        viewBox: "0 0 24 24",
        fill: isPinned ? 'currentColor' : 'none',
        stroke: "currentColor",
        strokeWidth: "2",
        strokeLinecap: "round"
      }, /*#__PURE__*/React.createElement("path", {
        d: "M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"
      })), isPinned ? 'Anclado' : 'Anclar'));
    })))), /*#__PURE__*/React.createElement("div", {
      style: {
        padding: '8px 16px',
        borderTop: '1px solid var(--border-subtle)',
        display: 'flex',
        gap: 16,
        alignItems: 'center'
      }
    }, [['↑↓', 'navegar'], ['↵', 'abrir'], ['Esc', 'cerrar']].map(([k, v]) => /*#__PURE__*/React.createElement("span", {
      key: k,
      style: {
        display: 'flex',
        gap: 5,
        alignItems: 'center',
        fontSize: 11,
        color: 'var(--text-faint)'
      }
    }, /*#__PURE__*/React.createElement("kbd", {
      style: {
        fontFamily: 'var(--font-mono)',
        fontSize: 10,
        background: 'var(--surface-sunken)',
        border: '1px solid var(--border-subtle)',
        borderRadius: 3,
        padding: '1px 5px'
      }
    }, k), v)))));
  }

  /* ============================================================
   * SIDEBAR
   * ============================================================ */
  function Sidebar({
    route,
    go,
    pins,
    onPinToggle,
    onOpenPalette
  }) {
    const active = route.screen;
    const map = {
      customer: 'customers',
      invoice: 'invoices'
    };
    const sel = map[active] || active;
    useLucide();
    return /*#__PURE__*/React.createElement("nav", {
      className: "erp-side"
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-brand erp-clickable",
      onClick: () => go({
        screen: 'dashboard'
      })
    }, /*#__PURE__*/React.createElement("img", {
      src: "../../assets/logo-mark.svg",
      width: "26",
      height: "26",
      alt: ""
    }), /*#__PURE__*/React.createElement("b", null, "Nexus", /*#__PURE__*/React.createElement("span", {
      className: "l"
    }, " Billing"))), /*#__PURE__*/React.createElement("div", {
      style: {
        padding: '10px 10px 0'
      }
    }, /*#__PURE__*/React.createElement("button", {
      onClick: onOpenPalette,
      style: {
        width: '100%',
        display: 'flex',
        alignItems: 'center',
        gap: 8,
        padding: '7px 10px',
        background: 'rgba(255,255,255,0.05)',
        border: '1px solid rgba(255,255,255,0.08)',
        borderRadius: 'var(--radius-sm)',
        cursor: 'pointer',
        color: 'var(--slate-400)',
        fontSize: 13
      }
    }, /*#__PURE__*/React.createElement("i", {
      "data-lucide": "search",
      style: {
        width: 14,
        height: 14
      }
    }), /*#__PURE__*/React.createElement("span", {
      style: {
        flex: 1,
        textAlign: 'left'
      }
    }, "Buscar vistas\u2026"), /*#__PURE__*/React.createElement("kbd", {
      style: {
        fontFamily: 'var(--font-mono)',
        fontSize: 10,
        background: 'rgba(255,255,255,0.07)',
        border: '1px solid rgba(255,255,255,0.08)',
        borderRadius: 3,
        padding: '1px 5px',
        color: 'var(--slate-500)'
      }
    }, "\u2318K"))), /*#__PURE__*/React.createElement("div", {
      className: "erp-nav"
    }, pins.length > 0 && /*#__PURE__*/React.createElement(React.Fragment, null, /*#__PURE__*/React.createElement("div", {
      className: "erp-navgroup",
      style: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'space-between'
      }
    }, /*#__PURE__*/React.createElement("span", null, "Atajos")), pins.map(id => {
      const v = viewMeta(id);
      return /*#__PURE__*/React.createElement("div", {
        key: id,
        style: {
          display: 'flex',
          alignItems: 'center'
        }
      }, /*#__PURE__*/React.createElement("button", {
        className: 'erp-navitem' + (sel === id ? ' active' : ''),
        style: {
          flex: 1
        },
        onClick: () => go({
          screen: id
        })
      }, /*#__PURE__*/React.createElement("i", {
        "data-lucide": v.icon,
        style: {
          width: 17,
          height: 17
        }
      }), /*#__PURE__*/React.createElement("span", null, v.label)), /*#__PURE__*/React.createElement("button", {
        onClick: () => onPinToggle(id),
        title: "Quitar atajo",
        style: {
          background: 'none',
          border: 'none',
          cursor: 'pointer',
          color: 'var(--slate-600)',
          padding: '0 8px 0 0',
          display: 'flex',
          alignItems: 'center',
          transition: 'color 120ms'
        },
        onMouseEnter: e => e.currentTarget.style.color = 'var(--slate-400)',
        onMouseLeave: e => e.currentTarget.style.color = 'var(--slate-600)'
      }, /*#__PURE__*/React.createElement("svg", {
        width: "13",
        height: "13",
        viewBox: "0 0 24 24",
        fill: "none",
        stroke: "currentColor",
        strokeWidth: "2.2",
        strokeLinecap: "round"
      }, /*#__PURE__*/React.createElement("path", {
        d: "M18 6 6 18M6 6l12 12"
      }))));
    }), /*#__PURE__*/React.createElement("div", {
      style: {
        height: 1,
        background: 'rgba(255,255,255,0.06)',
        margin: '6px 10px'
      }
    })), NAV.map((sec, i) => /*#__PURE__*/React.createElement(React.Fragment, {
      key: i
    }, sec.group && /*#__PURE__*/React.createElement("div", {
      className: "erp-navgroup"
    }, sec.group), sec.items.map(it => /*#__PURE__*/React.createElement("button", {
      key: it.id,
      className: 'erp-navitem' + (sel === it.id ? ' active' : ''),
      onClick: () => go({
        screen: it.id
      })
    }, /*#__PURE__*/React.createElement("i", {
      "data-lucide": it.icon,
      style: {
        width: 17,
        height: 17
      }
    }), /*#__PURE__*/React.createElement("span", null, it.label), it.count && /*#__PURE__*/React.createElement("span", {
      className: "count"
    }, it.count)))))), /*#__PURE__*/React.createElement("div", {
      className: "erp-nav",
      style: {
        paddingTop: 0,
        flex: 'none',
        borderTop: '1px solid rgba(255,255,255,0.06)',
        paddingBottom: 0
      }
    }, /*#__PURE__*/React.createElement("button", {
      className: 'erp-navitem' + (sel === 'settings' ? ' active' : ''),
      onClick: () => go({
        screen: 'settings'
      })
    }, /*#__PURE__*/React.createElement("i", {
      "data-lucide": "settings-2",
      style: {
        width: 17,
        height: 17
      }
    }), /*#__PURE__*/React.createElement("span", null, "Configuraci\xF3n"))), /*#__PURE__*/React.createElement("div", {
      className: "erp-side__foot"
    }, /*#__PURE__*/React.createElement(Avatar, {
      name: "Jos\xE9 Ram\xEDrez",
      shape: "circle",
      size: "sm"
    }), /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("div", {
      className: "nm"
    }, "Jos\xE9 Ram\xEDrez"), /*#__PURE__*/React.createElement("div", {
      className: "rl"
    }, "Acme Distribuci\xF3n \xB7 Admin"))));
  }

  /* ---- Topbar ---- */
  function Topbar({
    crumbs,
    onOpenPalette
  }) {
    const [dark, setDark] = useState(isDarkStored);
    const toggleTheme = () => {
      const d = !dark;
      setDark(d);
      applyTheme(d);
    };
    return /*#__PURE__*/React.createElement("header", {
      className: "erp-top"
    }, /*#__PURE__*/React.createElement(Breadcrumb, {
      items: crumbs
    }), /*#__PURE__*/React.createElement("div", {
      className: "erp-top__right"
    }, /*#__PURE__*/React.createElement("button", {
      className: "erp-search",
      onClick: onOpenPalette,
      style: {
        cursor: 'pointer'
      }
    }, /*#__PURE__*/React.createElement("i", {
      "data-lucide": "search",
      style: {
        width: 15,
        height: 15
      }
    }), /*#__PURE__*/React.createElement("span", {
      style: {
        fontSize: 13,
        color: 'var(--text-faint)'
      }
    }, "Buscar\u2026"), /*#__PURE__*/React.createElement("kbd", {
      style: {
        fontFamily: 'var(--font-mono)',
        fontSize: 10,
        background: 'var(--surface)',
        border: '1px solid var(--border-default)',
        borderRadius: 3,
        padding: '1px 5px',
        color: 'var(--text-faint)'
      }
    }, "\u2318K")), /*#__PURE__*/React.createElement(IconButton, {
      label: dark ? 'Tema claro' : 'Tema oscuro',
      onClick: toggleTheme
    }, /*#__PURE__*/React.createElement("i", {
      "data-lucide": dark ? 'sun' : 'moon'
    })), /*#__PURE__*/React.createElement(IconButton, {
      label: "Notificaciones"
    }, /*#__PURE__*/React.createElement("i", {
      "data-lucide": "bell"
    })), /*#__PURE__*/React.createElement(IconButton, {
      label: "Ayuda"
    }, /*#__PURE__*/React.createElement("i", {
      "data-lucide": "circle-help"
    }))));
  }

  /* ---- CommandBar ---- */
  function CommandBar({
    title,
    actions
  }) {
    return /*#__PURE__*/React.createElement("div", {
      className: "erp-cmd"
    }, /*#__PURE__*/React.createElement("span", {
      className: "erp-cmd__title"
    }, title), /*#__PURE__*/React.createElement("div", {
      className: "erp-cmd__actions"
    }, actions));
  }

  /* ============================================================
   * APP SHELL — wires palette + pins state
   * ============================================================ */
  function AppShell({
    route,
    go,
    crumbs,
    title,
    actions,
    children
  }) {
    const [paletteOpen, setPaletteOpen] = useState(false);
    const [pins, setPins] = useState(loadPins);
    useLucide();

    // ⌘K global shortcut
    useEffect(() => {
      const h = e => {
        if ((e.metaKey || e.ctrlKey) && e.key === 'k') {
          e.preventDefault();
          setPaletteOpen(p => !p);
        }
      };
      window.addEventListener('keydown', h);
      return () => window.removeEventListener('keydown', h);
    }, []);
    const handlePinToggle = id => {
      setPins(prev => {
        const next = prev.includes(id) ? prev.filter(p => p !== id) : [...prev, id];
        savePins(next);
        return next;
      });
    };
    return /*#__PURE__*/React.createElement("div", {
      className: "erp"
    }, /*#__PURE__*/React.createElement(Sidebar, {
      route: route,
      go: go,
      pins: pins,
      onPinToggle: handlePinToggle,
      onOpenPalette: () => setPaletteOpen(true)
    }), /*#__PURE__*/React.createElement("div", {
      className: "erp-main"
    }, /*#__PURE__*/React.createElement(Topbar, {
      crumbs: crumbs,
      onOpenPalette: () => setPaletteOpen(true)
    }), /*#__PURE__*/React.createElement(CommandBar, {
      title: title,
      actions: actions
    }), /*#__PURE__*/React.createElement("div", {
      className: "erp-body"
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-body__inner"
    }, children))), paletteOpen && /*#__PURE__*/React.createElement(CommandPalette, {
      onClose: () => setPaletteOpen(false),
      go: go,
      pins: pins,
      onPinToggle: handlePinToggle
    }));
  }

  /* ---- Login (unchanged) ---- */
  function Login({
    onSignIn
  }) {
    useLucide();
    return /*#__PURE__*/React.createElement("div", {
      className: "erp-login"
    }, /*#__PURE__*/React.createElement("aside", {
      className: "erp-login__aside"
    }, /*#__PURE__*/React.createElement("div", {
      className: "erp-brand",
      style: {
        padding: 0,
        height: 'auto',
        border: 'none'
      }
    }, /*#__PURE__*/React.createElement("img", {
      src: "../../assets/logo-mark.svg",
      width: "34",
      height: "34",
      alt: ""
    }), /*#__PURE__*/React.createElement("b", {
      style: {
        fontSize: 22
      }
    }, "Nexus", /*#__PURE__*/React.createElement("span", {
      className: "l"
    }, " Billing"))), /*#__PURE__*/React.createElement("div", {
      style: {
        maxWidth: 420
      }
    }, /*#__PURE__*/React.createElement("h1", {
      style: {
        color: '#fff',
        fontSize: 34,
        lineHeight: 1.15,
        letterSpacing: '-0.02em'
      }
    }, "Tu negocio,", /*#__PURE__*/React.createElement("br", null), "como vos quer\xE9s llevarlo."), /*#__PURE__*/React.createElement("p", {
      style: {
        color: 'var(--slate-400)',
        fontSize: 15,
        marginTop: 14,
        lineHeight: 1.65
      }
    }, "Activ\xE1 los m\xF3dulos que us\xE1s, ocult\xE1 los que no. Dale a cada persona acceso a lo que necesita \u2014 nada m\xE1s, nada menos. El sistema se adapta a tu operaci\xF3n, no al rev\xE9s.")), /*#__PURE__*/React.createElement("div", {
      style: {
        display: 'flex',
        gap: 24,
        color: 'var(--slate-400)',
        fontSize: 13,
        flexWrap: 'wrap'
      }
    }, /*#__PURE__*/React.createElement("span", null, /*#__PURE__*/React.createElement("b", {
      style: {
        color: '#fff',
        fontFamily: 'var(--font-mono)'
      }
    }, "Facturaci\xF3n"), " con NCF"), /*#__PURE__*/React.createElement("span", null, /*#__PURE__*/React.createElement("b", {
      style: {
        color: '#fff',
        fontFamily: 'var(--font-mono)'
      }
    }, "Contabilidad"), " integrada"), /*#__PURE__*/React.createElement("span", null, /*#__PURE__*/React.createElement("b", {
      style: {
        color: '#fff',
        fontFamily: 'var(--font-mono)'
      }
    }, "Sin l\xEDmite"), " de usuarios"))), /*#__PURE__*/React.createElement("div", {
      className: "erp-login__form"
    }, /*#__PURE__*/React.createElement("form", {
      className: "erp-login__form-inner",
      onSubmit: e => {
        e.preventDefault();
        onSignIn();
      }
    }, /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("h2", {
      style: {
        fontSize: 26,
        fontWeight: 700,
        letterSpacing: '-0.02em'
      }
    }, "Bienvenido"), /*#__PURE__*/React.createElement("p", {
      style: {
        fontSize: 14,
        color: 'var(--text-muted)',
        marginTop: 4
      }
    }, "Ingres\xE1 con tu correo para continuar.")), /*#__PURE__*/React.createElement("label", {
      className: "nx-field"
    }, /*#__PURE__*/React.createElement("span", {
      className: "nx-label"
    }, "Correo"), /*#__PURE__*/React.createElement("input", {
      className: "nx-input",
      type: "email",
      defaultValue: "jramirez@acmedist.do"
    })), /*#__PURE__*/React.createElement("label", {
      className: "nx-field"
    }, /*#__PURE__*/React.createElement("span", {
      className: "nx-label"
    }, "Contrase\xF1a"), /*#__PURE__*/React.createElement("input", {
      className: "nx-input",
      type: "password",
      defaultValue: "password"
    })), /*#__PURE__*/React.createElement("div", {
      style: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'space-between'
      }
    }, /*#__PURE__*/React.createElement("label", {
      className: "nx-check"
    }, /*#__PURE__*/React.createElement("input", {
      type: "checkbox",
      defaultChecked: true
    }), /*#__PURE__*/React.createElement("span", {
      className: "nx-check__box"
    }, /*#__PURE__*/React.createElement("svg", {
      viewBox: "0 0 24 24",
      fill: "none",
      stroke: "currentColor",
      strokeWidth: "3.2",
      strokeLinecap: "round",
      strokeLinejoin: "round"
    }, /*#__PURE__*/React.createElement("path", {
      d: "M20 6 9 17l-5-5"
    }))), /*#__PURE__*/React.createElement("span", null, "Recordarme")), /*#__PURE__*/React.createElement("a", {
      href: "#",
      style: {
        fontSize: 13
      }
    }, "\xBFNo record\xE1s la contrase\xF1a?")), /*#__PURE__*/React.createElement(Button, {
      variant: "primary",
      size: "lg",
      block: true,
      type: "submit"
    }, "Entrar"))));
  }
  Object.assign(window, {
    NXShell: AppShell,
    NXLogin: Login
  });
})();
})(); } catch (e) { __ds_ns.__errors.push({ path: "ui_kits/erp/shell.jsx", error: String((e && e.message) || e) }); }

__ds_ns.Avatar = __ds_scope.Avatar;

__ds_ns.Badge = __ds_scope.Badge;

__ds_ns.Button = __ds_scope.Button;

__ds_ns.Checkbox = __ds_scope.Checkbox;

__ds_ns.IconButton = __ds_scope.IconButton;

__ds_ns.Input = __ds_scope.Input;

__ds_ns.Select = __ds_scope.Select;

__ds_ns.Switch = __ds_scope.Switch;

__ds_ns.Tag = __ds_scope.Tag;

__ds_ns.DataTable = __ds_scope.DataTable;

__ds_ns.FactBox = __ds_scope.FactBox;

__ds_ns.KeyValue = __ds_scope.KeyValue;

__ds_ns.StatTile = __ds_scope.StatTile;

__ds_ns.Banner = __ds_scope.Banner;

__ds_ns.EmptyState = __ds_scope.EmptyState;

__ds_ns.Card = __ds_scope.Card;

__ds_ns.Collapsible = __ds_scope.Collapsible;

__ds_ns.Breadcrumb = __ds_scope.Breadcrumb;

__ds_ns.Tabs = __ds_scope.Tabs;

})();
