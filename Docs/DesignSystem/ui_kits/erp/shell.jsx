/* Nexus Billing ERP — shell: sidebar (with quick nav + pinned shortcuts),
 * command palette (⌘K), topbar, command bar, login. */
(function () {
  const NS = window.NexusBillingDesignSystem_f29fd0 || {};
  const { Button, IconButton, Avatar, Breadcrumb } = NS;
  const { useEffect, useState, useRef, useCallback } = React;

  /* ---- Theme ---- */
  const THEME_KEY = 'nx-theme';
  const applyTheme = (dark) => {
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

  const Icon = ({ n, ...p }) => <i data-lucide={n} {...p}></i>;
  window.NXIcon = Icon;

  function useLucide() {
    useEffect(() => { const t = setTimeout(() => window.lucide && window.lucide.createIcons(), 0); return () => clearTimeout(t); });
  }
  window.useLucide = useLucide;

  /* ---- Pinned shortcuts persistence ---- */
  const PINS_KEY = 'nx-pinned';
  const DEFAULT_PINS = ['dashboard', 'invoices'];
  const loadPins = () => { try { return JSON.parse(localStorage.getItem(PINS_KEY)) || DEFAULT_PINS; } catch { return DEFAULT_PINS; } };
  const savePins = (p) => localStorage.setItem(PINS_KEY, JSON.stringify(p));

  /* ---- All searchable views ---- */
  const ALL_VIEWS = [
    { id: 'dashboard',  label: 'Dashboard',          icon: 'layout-dashboard', group: 'Vistas', desc: 'KPIs, actividad reciente, AR aging' },
    { id: 'customers',  label: 'Clientes',            icon: 'users',            group: 'Ventas',  desc: 'Fichas de clientes, balance y cobros' },
    { id: 'invoices',   label: 'Facturas de venta',   icon: 'file-text',        group: 'Ventas',  desc: 'Facturas, notas de crédito, NCF' },
    { id: 'vendors',    label: 'Proveedores',          icon: 'truck',            group: 'Compras', desc: 'Fichas de proveedores, cuentas por pagar' },
    { id: 'porders',    label: 'Órdenes de compra',   icon: 'shopping-cart',    group: 'Compras', desc: 'Órdenes, recepciones de mercancía' },
    { id: 'items',      label: 'Artículos',            icon: 'package',          group: 'Inventario', desc: 'Artículos, precios, stock disponible' },
    { id: 'locations',  label: 'Almacenes',            icon: 'map-pin',          group: 'Inventario', desc: 'Ubicaciones y stock por almacén' },
    { id: 'gl',         label: 'Plan de cuentas',      icon: 'book-open',        group: 'Contabilidad', desc: 'Cuentas contables y asientos' },
  ];

  const ALL_ACTIONS = [
    { id: 'act-newinv',  label: 'Nueva factura',     icon: 'plus-circle',   group: 'Acciones', screen: 'invoice', no: 'SI-2026-001847', desc: 'Crear una nueva factura de venta' },
    { id: 'act-newcust', label: 'Nuevo cliente',     icon: 'user-plus',     group: 'Acciones', screen: 'customers', desc: 'Registrar un nuevo cliente' },
    { id: 'act-newitem', label: 'Nuevo artículo',    icon: 'package-plus',  group: 'Acciones', screen: 'items', desc: 'Crear un artículo en inventario' },
  ];

  /* ---- Sidebar nav tree ---- */
  const NAV = [
    { group: null,        items: [{ id: 'dashboard', label: 'Dashboard',       icon: 'layout-dashboard' }] },
    { group: 'Ventas',    items: [{ id: 'customers', label: 'Clientes',        icon: 'users', count: '6' },
                                   { id: 'invoices',  label: 'Facturas',        icon: 'file-text', count: '18' }] },
    { group: 'Compras',   items: [{ id: 'vendors',   label: 'Proveedores',     icon: 'truck' },
                                   { id: 'porders',   label: 'Ó. de compra',   icon: 'shopping-cart' }] },
    { group: 'Inventario',items: [{ id: 'items',     label: 'Artículos',       icon: 'package' },
                                   { id: 'locations', label: 'Almacenes',       icon: 'map-pin' }] },
    { group: 'Contabilidad', items: [{ id: 'gl',     label: 'Plan de cuentas', icon: 'book-open' }] },
    { group: null, items: [{ id: 'settings',  label: 'Configuración',    icon: 'settings-2' }] },
  ];
  const allNavItems = NAV.flatMap(s => s.items);
  const viewMeta = (id) => ALL_VIEWS.find(v => v.id === id) || allNavItems.find(n => n.id === id) || { label: id, icon: 'circle' };

  /* ============================================================
   * COMMAND PALETTE
   * ============================================================ */
  function CommandPalette({ onClose, go, pins, onPinToggle }) {
    const [q, setQ] = useState('');
    const [cursor, setCursor] = useState(0);
    const inputRef = useRef(null);
    useLucide();

    useEffect(() => { setTimeout(() => inputRef.current?.focus(), 30); }, []);

    // Build results
    const lq = q.toLowerCase();
    const matchViews = ALL_VIEWS.filter(v => !lq || v.label.toLowerCase().includes(lq) || v.desc.toLowerCase().includes(lq));
    const matchActions = ALL_ACTIONS.filter(a => !lq || a.label.toLowerCase().includes(lq) || a.desc.toLowerCase().includes(lq));
    const results = [...matchViews, ...matchActions];

    const select = useCallback((item) => {
      if (item.screen) go({ screen: item.screen, no: item.no });
      else go({ screen: item.id });
      onClose();
    }, [go, onClose]);

    useEffect(() => { setCursor(0); }, [q]);

    const onKey = (e) => {
      if (e.key === 'Escape') { onClose(); return; }
      if (e.key === 'ArrowDown') { e.preventDefault(); setCursor(c => Math.min(c + 1, results.length - 1)); }
      if (e.key === 'ArrowUp')   { e.preventDefault(); setCursor(c => Math.max(c - 1, 0)); }
      if (e.key === 'Enter' && results[cursor]) select(results[cursor]);
    };

    const grouped = {};
    results.forEach(r => { (grouped[r.group] = grouped[r.group] || []).push(r); });

    return (
      <div style={{ position: 'fixed', inset: 0, background: 'var(--scrim)', zIndex: 500, display: 'flex', alignItems: 'flex-start', justifyContent: 'center', paddingTop: 80 }}
           onClick={onClose}>
        <div style={{ width: '100%', maxWidth: 560, background: 'var(--surface)', border: '1px solid var(--border-default)', borderRadius: 'var(--radius-lg)', boxShadow: 'var(--shadow-xl)', overflow: 'hidden' }}
             onClick={e => e.stopPropagation()}>

          {/* Input */}
          <div style={{ display: 'flex', alignItems: 'center', gap: 10, padding: '0 16px', borderBottom: '1px solid var(--border-subtle)', height: 52 }}>
            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="var(--text-faint)" strokeWidth="2" strokeLinecap="round"><circle cx="11" cy="11" r="8"/><path d="m21 21-4.35-4.35"/></svg>
            <input ref={inputRef} value={q} onChange={e => setQ(e.target.value)} onKeyDown={onKey}
                   placeholder="Buscar vistas, clientes, facturas…"
                   style={{ flex: 1, border: 'none', background: 'none', outline: 'none', font: 'inherit', fontSize: 15, color: 'var(--text-strong)' }} />
            {q && <button onClick={() => setQ('')} style={{ background: 'none', border: 'none', cursor: 'pointer', color: 'var(--text-faint)', display: 'flex', padding: 2 }}>
              <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round"><path d="M18 6 6 18M6 6l12 12"/></svg>
            </button>}
            <kbd style={{ fontFamily: 'var(--font-mono)', fontSize: 11, background: 'var(--surface-sunken)', border: '1px solid var(--border-subtle)', borderRadius: 4, padding: '2px 6px', color: 'var(--text-faint)' }}>Esc</kbd>
          </div>

          {/* Results */}
          <div style={{ maxHeight: 400, overflowY: 'auto' }}>
            {results.length === 0 && (
              <div style={{ padding: '32px 20px', textAlign: 'center', color: 'var(--text-muted)', fontSize: 13 }}>Sin resultados para "{q}"</div>
            )}
            {Object.entries(grouped).map(([grp, items]) => (
              <div key={grp}>
                <div style={{ padding: '10px 16px 4px', fontSize: 10.5, fontWeight: 700, letterSpacing: '0.08em', textTransform: 'uppercase', color: 'var(--text-faint)' }}>{grp}</div>
                {items.map((item) => {
                  const idx = results.indexOf(item);
                  const isPinned = pins.includes(item.id);
                  const isView = item.group !== 'Acciones';
                  return (
                    <div key={item.id}
                         style={{ display: 'flex', alignItems: 'center', gap: 12, padding: '9px 16px', cursor: 'pointer', background: idx === cursor ? 'var(--surface-hover)' : 'transparent', transition: 'background 80ms' }}
                         onMouseEnter={() => setCursor(idx)}
                         onClick={() => select(item)}>
                      <div style={{ width: 30, height: 30, borderRadius: 'var(--radius-sm)', background: 'var(--surface-sunken)', display: 'flex', alignItems: 'center', justifyContent: 'center', color: 'var(--text-muted)', flex: 'none' }}>
                        <i data-lucide={item.icon} style={{ width: 15, height: 15 }}></i>
                      </div>
                      <div style={{ flex: 1, minWidth: 0 }}>
                        <div style={{ fontSize: 13.5, fontWeight: 600, color: 'var(--text-strong)' }}>{item.label}</div>
                        <div style={{ fontSize: 12, color: 'var(--text-muted)', marginTop: 1 }}>{item.desc}</div>
                      </div>
                      {isView && (
                        <button onClick={(e) => { e.stopPropagation(); onPinToggle(item.id); }}
                                title={isPinned ? 'Quitar atajo' : 'Agregar como atajo'}
                                style={{ background: isPinned ? 'var(--action-tint)' : 'none', border: '1px solid ' + (isPinned ? 'var(--action-ring)' : 'var(--border-subtle)'), borderRadius: 'var(--radius-xs)', cursor: 'pointer', color: isPinned ? 'var(--action)' : 'var(--text-faint)', padding: '3px 8px', fontSize: 11, fontWeight: 600, display: 'flex', alignItems: 'center', gap: 4, whiteSpace: 'nowrap', transition: 'all 120ms' }}>
                          <svg width="11" height="11" viewBox="0 0 24 24" fill={isPinned ? 'currentColor' : 'none'} stroke="currentColor" strokeWidth="2" strokeLinecap="round"><path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"/></svg>
                          {isPinned ? 'Anclado' : 'Anclar'}
                        </button>
                      )}
                    </div>
                  );
                })}
              </div>
            ))}
          </div>

          {/* Footer hint */}
          <div style={{ padding: '8px 16px', borderTop: '1px solid var(--border-subtle)', display: 'flex', gap: 16, alignItems: 'center' }}>
            {[['↑↓', 'navegar'], ['↵', 'abrir'], ['Esc', 'cerrar']].map(([k, v]) => (
              <span key={k} style={{ display: 'flex', gap: 5, alignItems: 'center', fontSize: 11, color: 'var(--text-faint)' }}>
                <kbd style={{ fontFamily: 'var(--font-mono)', fontSize: 10, background: 'var(--surface-sunken)', border: '1px solid var(--border-subtle)', borderRadius: 3, padding: '1px 5px' }}>{k}</kbd>{v}
              </span>
            ))}
          </div>
        </div>
      </div>
    );
  }

  /* ============================================================
   * SIDEBAR
   * ============================================================ */
  function Sidebar({ route, go, pins, onPinToggle, onOpenPalette }) {
    const active = route.screen;
    const map = { customer: 'customers', invoice: 'invoices' };
    const sel = map[active] || active;
    useLucide();

    return (
      <nav className="erp-side">
        <div className="erp-brand erp-clickable" onClick={() => go({ screen: 'dashboard' })}>
          <img src="../../assets/logo-mark.svg" width="26" height="26" alt="" />
          <b>Nexus<span className="l"> Billing</span></b>
        </div>

        {/* Quick search trigger */}
        <div style={{ padding: '10px 10px 0' }}>
          <button onClick={onOpenPalette}
                  style={{ width: '100%', display: 'flex', alignItems: 'center', gap: 8, padding: '7px 10px', background: 'rgba(255,255,255,0.05)', border: '1px solid rgba(255,255,255,0.08)', borderRadius: 'var(--radius-sm)', cursor: 'pointer', color: 'var(--slate-400)', fontSize: 13 }}>
            <i data-lucide="search" style={{ width: 14, height: 14 }}></i>
            <span style={{ flex: 1, textAlign: 'left' }}>Buscar vistas…</span>
            <kbd style={{ fontFamily: 'var(--font-mono)', fontSize: 10, background: 'rgba(255,255,255,0.07)', border: '1px solid rgba(255,255,255,0.08)', borderRadius: 3, padding: '1px 5px', color: 'var(--slate-500)' }}>⌘K</kbd>
          </button>
        </div>

        <div className="erp-nav">
          {/* Pinned shortcuts */}
          {pins.length > 0 && (
            <>
              <div className="erp-navgroup" style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between' }}>
                <span>Atajos</span>
              </div>
              {pins.map(id => {
                const v = viewMeta(id);
                return (
                  <div key={id} style={{ display: 'flex', alignItems: 'center' }}>
                    <button className={'erp-navitem' + (sel === id ? ' active' : '')}
                            style={{ flex: 1 }} onClick={() => go({ screen: id })}>
                      <i data-lucide={v.icon} style={{ width: 17, height: 17 }}></i>
                      <span>{v.label}</span>
                    </button>
                    <button onClick={() => onPinToggle(id)} title="Quitar atajo"
                            style={{ background: 'none', border: 'none', cursor: 'pointer', color: 'var(--slate-600)', padding: '0 8px 0 0', display: 'flex', alignItems: 'center', transition: 'color 120ms' }}
                            onMouseEnter={e => e.currentTarget.style.color = 'var(--slate-400)'}
                            onMouseLeave={e => e.currentTarget.style.color = 'var(--slate-600)'}>
                      <svg width="13" height="13" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2.2" strokeLinecap="round"><path d="M18 6 6 18M6 6l12 12"/></svg>
                    </button>
                  </div>
                );
              })}
              <div style={{ height: 1, background: 'rgba(255,255,255,0.06)', margin: '6px 10px' }}></div>
            </>
          )}

          {/* Standard nav */}
          {NAV.map((sec, i) => (
            <React.Fragment key={i}>
              {sec.group && <div className="erp-navgroup">{sec.group}</div>}
              {sec.items.map(it => (
                <button key={it.id} className={'erp-navitem' + (sel === it.id ? ' active' : '')}
                        onClick={() => go({ screen: it.id })}>
                  <i data-lucide={it.icon} style={{ width: 17, height: 17 }}></i>
                  <span>{it.label}</span>
                  {it.count && <span className="count">{it.count}</span>}
                </button>
              ))}
            </React.Fragment>
          ))}
        </div>

        {/* Settings at bottom of nav */}
        <div className="erp-nav" style={{ paddingTop: 0, flex: 'none', borderTop: '1px solid rgba(255,255,255,0.06)', paddingBottom: 0 }}>
          <button className={'erp-navitem' + (sel === 'settings' ? ' active' : '')} onClick={() => go({ screen: 'settings' })}>
            <i data-lucide="settings-2" style={{ width: 17, height: 17 }}></i>
            <span>Configuración</span>
          </button>
        </div>
      <div className="erp-side__foot">
          <Avatar name="José Ramírez" shape="circle" size="sm" />
          <div>
            <div className="nm">José Ramírez</div>
            <div className="rl">Acme Distribución · Admin</div>
          </div>
        </div>
      </nav>
    );
  }

  /* ---- Topbar ---- */
  function Topbar({ crumbs, onOpenPalette }) {
    const [dark, setDark] = useState(isDarkStored);
    const toggleTheme = () => { const d = !dark; setDark(d); applyTheme(d); };
    return (
      <header className="erp-top">
        <Breadcrumb items={crumbs} />
        <div className="erp-top__right">
          <button className="erp-search" onClick={onOpenPalette} style={{ cursor: 'pointer' }}>
            <i data-lucide="search" style={{ width: 15, height: 15 }}></i>
            <span style={{ fontSize: 13, color: 'var(--text-faint)' }}>Buscar…</span>
            <kbd style={{ fontFamily: 'var(--font-mono)', fontSize: 10, background: 'var(--surface)', border: '1px solid var(--border-default)', borderRadius: 3, padding: '1px 5px', color: 'var(--text-faint)' }}>⌘K</kbd>
          </button>
          <IconButton label={dark ? 'Tema claro' : 'Tema oscuro'} onClick={toggleTheme}>
            <i data-lucide={dark ? 'sun' : 'moon'}></i>
          </IconButton>
          <IconButton label="Notificaciones"><i data-lucide="bell"></i></IconButton>
          <IconButton label="Ayuda"><i data-lucide="circle-help"></i></IconButton>
        </div>
      </header>
    );
  }

  /* ---- CommandBar ---- */
  function CommandBar({ title, actions }) {
    return (
      <div className="erp-cmd">
        <span className="erp-cmd__title">{title}</span>
        <div className="erp-cmd__actions">{actions}</div>
      </div>
    );
  }

  /* ============================================================
   * APP SHELL — wires palette + pins state
   * ============================================================ */
  function AppShell({ route, go, crumbs, title, actions, children }) {
    const [paletteOpen, setPaletteOpen] = useState(false);
    const [pins, setPins] = useState(loadPins);
    useLucide();

    // ⌘K global shortcut
    useEffect(() => {
      const h = (e) => { if ((e.metaKey || e.ctrlKey) && e.key === 'k') { e.preventDefault(); setPaletteOpen(p => !p); } };
      window.addEventListener('keydown', h);
      return () => window.removeEventListener('keydown', h);
    }, []);

    const handlePinToggle = (id) => {
      setPins(prev => {
        const next = prev.includes(id) ? prev.filter(p => p !== id) : [...prev, id];
        savePins(next);
        return next;
      });
    };

    return (
      <div className="erp">
        <Sidebar route={route} go={go} pins={pins} onPinToggle={handlePinToggle} onOpenPalette={() => setPaletteOpen(true)} />
        <div className="erp-main">
          <Topbar crumbs={crumbs} onOpenPalette={() => setPaletteOpen(true)} />
          <CommandBar title={title} actions={actions} />
          <div className="erp-body"><div className="erp-body__inner">{children}</div></div>
        </div>
        {paletteOpen && (
          <CommandPalette
            onClose={() => setPaletteOpen(false)}
            go={go}
            pins={pins}
            onPinToggle={handlePinToggle}
          />
        )}
      </div>
    );
  }

  /* ---- Login (unchanged) ---- */
  function Login({ onSignIn }) {
    useLucide();
    return (
      <div className="erp-login">
        <aside className="erp-login__aside">
          <div className="erp-brand" style={{ padding: 0, height: 'auto', border: 'none' }}>
            <img src="../../assets/logo-mark.svg" width="34" height="34" alt="" />
            <b style={{ fontSize: 22 }}>Nexus<span className="l"> Billing</span></b>
          </div>
          <div style={{ maxWidth: 420 }}>
            <h1 style={{ color: '#fff', fontSize: 34, lineHeight: 1.15, letterSpacing: '-0.02em' }}>
              Tu negocio,<br />como vos querés llevarlo.
            </h1>
            <p style={{ color: 'var(--slate-400)', fontSize: 15, marginTop: 14, lineHeight: 1.65 }}>
              Activá los módulos que usás, ocultá los que no. Dale a cada persona acceso a lo que necesita — nada más, nada menos. El sistema se adapta a tu operación, no al revés.
            </p>
          </div>
          <div style={{ display: 'flex', gap: 24, color: 'var(--slate-400)', fontSize: 13, flexWrap: 'wrap' }}>
            <span><b style={{ color: '#fff', fontFamily: 'var(--font-mono)' }}>Facturación</b> con NCF</span>
            <span><b style={{ color: '#fff', fontFamily: 'var(--font-mono)' }}>Contabilidad</b> integrada</span>
            <span><b style={{ color: '#fff', fontFamily: 'var(--font-mono)' }}>Sin límite</b> de usuarios</span>
          </div>
        </aside>
        <div className="erp-login__form">
          <form className="erp-login__form-inner" onSubmit={(e) => { e.preventDefault(); onSignIn(); }}>
            <div>
              <h2 style={{ fontSize: 26, fontWeight: 700, letterSpacing: '-0.02em' }}>Bienvenido</h2>
              <p style={{ fontSize: 14, color: 'var(--text-muted)', marginTop: 4 }}>Ingresá con tu correo para continuar.</p>
            </div>
            <label className="nx-field">
              <span className="nx-label">Correo</span>
              <input className="nx-input" type="email" defaultValue="jramirez@acmedist.do" />
            </label>
            <label className="nx-field">
              <span className="nx-label">Contraseña</span>
              <input className="nx-input" type="password" defaultValue="password" />
            </label>
            <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between' }}>
              <label className="nx-check">
                <input type="checkbox" defaultChecked />
                <span className="nx-check__box"><svg viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="3.2" strokeLinecap="round" strokeLinejoin="round"><path d="M20 6 9 17l-5-5" /></svg></span>
                <span>Recordarme</span>
              </label>
              <a href="#" style={{ fontSize: 13 }}>¿No recordás la contraseña?</a>
            </div>
            <Button variant="primary" size="lg" block type="submit">Entrar</Button>
          </form>
        </div>
      </div>
    );
  }

  Object.assign(window, { NXShell: AppShell, NXLogin: Login });
})();
