/* Nexus Billing ERP — screens. Composes design-system primitives (window.NS). */
(function () {
  const NS = window.NexusBillingDesignSystem_f29fd0 || {};
  const { Button, IconButton, Badge, Tag, Avatar, Input, Select, DataTable, StatTile, KeyValue, FactBox, Card, Tabs } = NS;
  const Icon = window.NXIcon;
  const { useState } = React;
  const D = window.NXDATA;

  const StatusBadge = ({ s }) => <Badge status={D.statusTone[s]} dot>{D.statusLabel[s]}</Badge>;
  const Money = ({ n, sign }) => <span className="erp-num" style={sign ? { color: n < 0 ? 'var(--money-negative)' : 'var(--money-positive)' } : undefined}>{D.money(n)}</span>;

  /* ============ DASHBOARD ============ */
  function Dashboard({ go }) {
    const max = Math.max(...D.aging.map(a => a.val));
    return (
      <div className="erp-stack">
        <div className="erp-pagehead">
          <div style={{ flex: 1 }}>
            <h1>Good morning, José</h1>
            <div className="erp-pagehead__meta">Thursday, 11 June 2026 · Fiscal period 2026-06 is open</div>
          </div>
        </div>

        <div className="erp-grid-3">
          <StatTile label="Outstanding AR" value={D.moneyShort(1834532)} delta="+12.4%" trend="up" />
          <StatTile label="Overdue" value={D.moneyShort(84105)} delta="-3.1%" trend="down" />
          <StatTile label="Posted today" value="37" delta="+6" trend="up" />
        </div>

        <div className="erp-grid-2">
          <Card title="AR aging" actions={<Button variant="ghost" size="sm" rightIcon={<Icon n="arrow-up-right" />} onClick={() => go({ screen: 'gl' })}>Ledger</Button>}>
            <div className="erp-bars">
              {D.aging.map(a => (
                <div className="col" key={a.cap}>
                  <span className="val">{D.moneyShort(a.val)}</span>
                  <div className="bar" style={{ height: (a.val / max * 100) + '%', background: a.color }}></div>
                  <span className="cap">{a.cap}</span>
                </div>
              ))}
            </div>
          </Card>

          <Card title="Activity">
            <ul className="nx-linklist" style={{ gap: 0 }}>
              {[
                { ic: 'check-circle-2', c: 'var(--emerald-500)', t: 'Invoice SI-2026-001848 posted', m: 'Distribuidora Caribe · RD$ 208,750.00', time: '08:24' },
                { ic: 'plus-circle', c: 'var(--blue-500)', t: 'Customer C-002101 created', m: 'Constructora Bávaro', time: '07:55' },
                { ic: 'alert-triangle', c: 'var(--amber-500)', t: 'Credit memo CM-2026-000219 awaiting approval', m: 'Ferretería del Sur · RD$ 1,905.40', time: 'Yesterday' },
                { ic: 'ban', c: 'var(--red-500)', t: 'Customer C-002130 blocked', m: 'Colmadón La Esquina · credit hold', time: 'Yesterday' },
              ].map((a, i) => (
                <li key={i} style={{ display: 'flex', gap: 12, alignItems: 'flex-start', padding: '11px 0', borderBottom: i < 3 ? '1px solid var(--border-subtle)' : 'none' }}>
                  <span style={{ color: a.c, marginTop: 1 }}><Icon n={a.ic} /></span>
                  <div style={{ flex: 1, minWidth: 0 }}>
                    <div style={{ fontSize: 13.5, fontWeight: 600, color: 'var(--text-strong)' }}>{a.t}</div>
                    <div style={{ fontSize: 12.5, color: 'var(--text-muted)' }}>{a.m}</div>
                  </div>
                  <span style={{ fontSize: 12, color: 'var(--text-faint)', fontFamily: 'var(--font-mono)' }}>{a.time}</span>
                </li>
              ))}
            </ul>
          </Card>
        </div>

        <Card title="Open sales invoices" padded={false}
              actions={<Button variant="primary" size="sm" leftIcon={<Icon n="plus" />} onClick={() => go({ screen: 'invoice', no: 'SI-2026-001847' })}>New invoice</Button>}>
          <InvoiceTable go={go} rows={D.invoices.filter(i => ['open', 'overdue', 'pending'].includes(i.status))} />
        </Card>
      </div>
    );
  }

  /* ============ SHARED TABLES ============ */
  function InvoiceTable({ rows, go, selectedId }) {
    return (
      <DataTable
        selectedId={selectedId}
        getRowId={(r) => r.no}
        onRowClick={(r) => go({ screen: 'invoice', no: r.no })}
        columns={[
          { key: 'no', header: 'Document No.', variant: 'doc', width: '170px' },
          { key: 'billTo', header: 'Bill-to' },
          { key: 'type', header: 'Type', width: '120px', render: (v) => <span style={{ color: 'var(--text-muted)' }}>{v}</span> },
          { key: 'postingDate', header: 'Posting date', width: '130px', render: (v) => <span className="erp-num" style={{ fontSize: 13 }}>{v}</span> },
          { key: 'status', header: 'Status', width: '120px', render: (v) => <StatusBadge s={v} /> },
          { key: 'amount', header: 'Amount', variant: 'num', align: 'right', render: (v) => D.money(v) },
        ]}
        rows={rows}
      />
    );
  }

  /* ============ CUSTOMER LIST ============ */
  function CustomerList({ go }) {
    const [q, setQ] = useState('');
    const rows = D.customers.filter(c => c.name.toLowerCase().includes(q.toLowerCase()) || c.no.toLowerCase().includes(q.toLowerCase()));
    return (
      <div className="erp-stack">
        <div className="erp-pagehead">
          <div style={{ flex: 1 }}>
            <h1>Customers</h1>
            <div className="erp-pagehead__meta">{D.customers.length} records · RD$ 353,075.40 total balance</div>
          </div>
        </div>
        <Card padded={false}>
          <div className="erp-toolbar">
            <div style={{ width: 260 }}>
              <span className="nx-inputgroup"><span className="nx-adorn"><Icon n="search" /></span>
                <input className="nx-input" placeholder="Search customers…" value={q} onChange={e => setQ(e.target.value)} /></span>
            </div>
            <Tag onRemove={() => {}}>Balance &gt; 0</Tag>
            <Button variant="ghost" size="sm" leftIcon={<Icon n="sliders-horizontal" />}>Filters</Button>
            <div className="spacer"></div>
            <Button variant="secondary" size="sm" leftIcon={<Icon n="download" />}>Export</Button>
            <Button variant="primary" size="sm" leftIcon={<Icon n="plus" />}>New customer</Button>
          </div>
          <DataTable
            getRowId={(r) => r.no}
            onRowClick={(r) => go({ screen: 'customer', no: r.no })}
            columns={[
              { key: 'no', header: 'No.', variant: 'doc', width: '120px' },
              { key: 'name', header: 'Name', render: (v, r) => (
                <span style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
                  <Avatar name={r.name} size="sm" /><span style={{ fontWeight: 600, color: 'var(--text-strong)' }}>{v}</span>
                </span>
              ) },
              { key: 'city', header: 'City', width: '150px' },
              { key: 'salesperson', header: 'Salesperson', width: '150px' },
              { key: 'status', header: 'Status', width: '120px', render: (v) => <StatusBadge s={v} /> },
              { key: 'balance', header: 'Balance (LCY)', variant: 'num', align: 'right', render: (v) => D.money(v) },
            ]}
            rows={rows}
          />
        </Card>
      </div>
    );
  }

  /* ============ CUSTOMER CARD (signature drill-through) ============ */
  function CustomerCard({ no, go }) {
    const c = D.customers.find(x => x.no === no) || D.customers[0];
    const [tab, setTab] = useState('general');
    const custInvoices = D.invoices.filter(i => i.customerNo === c.no);
    return (
      <div className="erp-stack">
        <div className="erp-pagehead">
          <Avatar name={c.name} size="lg" />
          <div style={{ flex: 1 }}>
            <h1 style={{ display: 'flex', alignItems: 'center', gap: 12 }}>{c.name} <StatusBadge s={c.status} /></h1>
            <div className="erp-pagehead__meta erp-num">{c.no} · {c.city} · Terms {c.terms}</div>
          </div>
        </div>

        <div className="erp-record">
          <div className="erp-stack">
            <Card padded={false}>
              <div style={{ padding: '0 14px' }}>
                <Tabs value={tab} onChange={setTab} tabs={[
                  { id: 'general', label: 'General' },
                  { id: 'invoicing', label: 'Invoicing' },
                  { id: 'documents', label: 'Documents', count: custInvoices.length },
                ]} />
              </div>
              <div style={{ padding: 18 }}>
                {tab === 'general' && (
                  <div className="erp-grid-2">
                    <KeyValue ruled collapseKey={`cust-gen-left-${c.no}`} items={[
                      { key: 'No.', value: c.no, mono: true },
                      { key: 'Name', value: c.name },
                      { key: 'RNC', value: c.rnc || '—', mono: true },
                      { key: 'Contact', value: c.contact },
                      { key: 'City', value: c.city },
                      { key: 'Address', value: '27 Calle El Conde', secondary: true },
                      { key: 'Country', value: 'Dominican Republic', secondary: true },
                      { key: 'Phone', value: '+1 809 555 1234', secondary: true, mono: true },
                    ]} />
                    <KeyValue ruled collapseKey={`cust-gen-right-${c.no}`} items={[
                      { key: 'Salesperson', value: c.salesperson },
                      { key: 'Payment terms', value: c.terms },
                      { key: 'Credit limit (LCY)', value: D.money(c.creditLimit), mono: true },
                      { key: 'Blocked', value: c.blocked ? 'Yes — credit hold' : 'No' },
                      { key: 'Customer price group', value: 'WHOLESALE', secondary: true, mono: true },
                      { key: 'Gen. posting group', value: 'DOMESTIC', secondary: true, mono: true },
                      { key: 'Tax area code', value: 'DOM-ITBIS', secondary: true, mono: true },
                    ]} />
                  </div>
                )}
                {tab === 'invoicing' && (
                  <KeyValue ruled items={[
                    { key: 'Balance (LCY)', value: D.money(c.balance), mono: true },
                    { key: 'Overdue (LCY)', value: D.money(c.overdue), mono: true },
                    { key: 'Credit limit (LCY)', value: D.money(c.creditLimit), mono: true },
                    { key: 'Available credit', value: D.money(c.creditLimit - c.balance), mono: true },
                  ]} />
                )}
                {tab === 'documents' && (
                  custInvoices.length
                    ? <div style={{ margin: -18 }}><InvoiceTable rows={custInvoices} go={go} /></div>
                    : <div className="nx-empty"><div className="nx-empty__icon"><Icon n="file-text" /></div><div className="nx-empty__title">No documents</div></div>
                )}
              </div>
            </Card>
          </div>

          <FactBox
            eyebrow="Customer"
            title={c.name}
            sections={[
              { label: 'Statistics', content: <KeyValue items={[
                  { key: 'Balance', value: D.money(c.balance), mono: true },
                  { key: 'Overdue', value: D.money(c.overdue), mono: true },
                  { key: 'Credit limit', value: D.money(c.creditLimit), mono: true },
                ]} /> },
              { label: 'Related', content: (
                <ul className="nx-linklist">
                  <li><span className="nx-link" onClick={() => go({ screen: 'invoices' })}>Posted invoices ({custInvoices.length}) <Icon n="arrow-up-right" /></span></li>
                  <li><span className="nx-link" onClick={() => go({ screen: 'gl' })}>Customer ledger entries <Icon n="arrow-up-right" /></span></li>
                  <li><span className="nx-link" onClick={() => go({ screen: 'items' })}>Items sold <Icon n="arrow-up-right" /></span></li>
                </ul>
              ) },
              { label: 'Salesperson', content: (
                <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
                  <Avatar name={c.salesperson} shape="circle" size="sm" />
                  <div><div style={{ fontSize: 13, fontWeight: 600, color: 'var(--text-strong)' }}>{c.salesperson}</div><div style={{ fontSize: 12, color: 'var(--text-muted)' }}>Sales · RD market</div></div>
                </div>
              ) },
            ]}
          />
        </div>
      </div>
    );
  }

  /* ============ SALES INVOICE (document) ============ */
  function SalesInvoice({ no, go }) {
    const inv = D.invoices.find(x => x.no === no) || D.invoices[0];
    const cust = D.customers.find(c => c.no === inv.customerNo);
    const lines = D.invoiceLines;
    const subtotal = lines.reduce((s, l) => s + l.line, 0);
    const itbis = subtotal * 0.18;
    const total = subtotal + itbis;
    return (
      <div className="erp-record">
        <div className="erp-doc">
          <div className="erp-doc__head">
            <div>
              <div className="nx-factbox__eyebrow">{inv.type}</div>
              <h1 className="erp-num" style={{ fontSize: 26, marginTop: 2 }}>{inv.no}</h1>
              <div style={{ display: 'flex', gap: 8, alignItems: 'center', marginTop: 8 }}>
                <StatusBadge s={inv.status} />
                {inv.ncf && <span style={{ fontFamily: 'var(--font-mono)', fontSize: 12, color: 'var(--text-muted)', background: 'var(--surface-sunken)', padding: '2px 8px', borderRadius: 'var(--radius-xs)', border: '1px solid var(--border-subtle)' }}>NCF: {inv.ncf}</span>}
              </div>
            </div>
            <div style={{ minWidth: 260 }}>
              <KeyValue items={[
                { key: 'Sell-to customer', value: <span className="nx-link" onClick={() => go({ screen: 'customer', no: inv.customerNo })}>{inv.billTo}</span> },
                { key: 'Customer No.', value: inv.customerNo, mono: true },
                { key: 'Posting date', value: inv.postingDate, mono: true },
                { key: 'Due date', value: inv.dueDate, mono: true },
              ]} />
            </div>
          </div>

          <DataTable
            columns={[
              { key: 'itemNo', header: 'Item No.', variant: 'doc', width: '120px',
                render: (v) => <span className="nx-link" onClick={() => go({ screen: 'items' })}>{v}</span> },
              { key: 'description', header: 'Description' },
              { key: 'qty', header: 'Qty', variant: 'num', align: 'right', width: '80px' },
              { key: 'uom', header: 'Unit', width: '80px' },
              { key: 'price', header: 'Unit price', variant: 'num', align: 'right', width: '120px', render: (v) => D.money(v) },
              { key: 'line', header: 'Line amount', variant: 'num', align: 'right', width: '140px', render: (v) => D.money(v) },
            ]}
            rows={lines}
            getRowId={(r) => r.itemNo}
          />

          <div className="erp-doc__totals">
            <div className="box">
              <div className="ln"><span style={{ color: 'var(--text-muted)' }}>Subtotal</span><span className="erp-num">{D.money(subtotal)}</span></div>
              <div className="ln"><span style={{ color: 'var(--text-muted)' }}>ITBIS ({inv.vatPct || 18}%)</span><span className="erp-num">{D.money(itbis)}</span></div>
              <div className="ln grand"><span>Total</span><span className="erp-num">{D.money(total)}</span></div>
            </div>
          </div>
        </div>

        <FactBox
          eyebrow="Bill-to customer"
          title={inv.billTo}
          sections={[
            { label: 'Customer', content: <KeyValue items={[
                { key: 'No.', value: inv.customerNo, mono: true },
                { key: 'Balance', value: D.money(cust ? cust.balance : 0), mono: true },
                { key: 'Credit limit', value: D.money(cust ? cust.creditLimit : 0), mono: true },
              ]} /> },
            { label: 'Related', content: (
              <ul className="nx-linklist">
                <li><span className="nx-link" onClick={() => go({ screen: 'customer', no: inv.customerNo })}>Open customer card <Icon n="arrow-up-right" /></span></li>
                <li><span className="nx-link" onClick={() => go({ screen: 'items' })}>Item availability <Icon n="arrow-up-right" /></span></li>
              </ul>
            ) },
          ]}
        />
      </div>
    );
  }

  /* ============ ITEM LIST ============ */
  function ItemList({ go }) {
    return (
      <div className="erp-stack">
        <div className="erp-pagehead"><div style={{ flex: 1 }}><h1>Items</h1><div className="erp-pagehead__meta">{D.items.length} records · Inventory module</div></div></div>
        <Card padded={false}>
          <div className="erp-toolbar">
            <div style={{ width: 260 }}><span className="nx-inputgroup"><span className="nx-adorn"><Icon n="search" /></span><input className="nx-input" placeholder="Search items…" /></span></div>
            <div className="spacer"></div>
            <Button variant="primary" size="sm" leftIcon={<Icon n="plus" />}>New item</Button>
          </div>
          <DataTable
            getRowId={(r) => r.no}
            columns={[
              { key: 'no', header: 'No.', variant: 'doc', width: '120px' },
              { key: 'description', header: 'Description' },
              { key: 'uom', header: 'Base UoM', width: '110px' },
              { key: 'onHand', header: 'On hand', variant: 'num', align: 'right', width: '120px', render: (v, r) => <span style={{ color: v === 0 ? 'var(--money-negative)' : 'var(--text-body)' }} className="erp-num">{v.toLocaleString()}</span> },
              { key: 'price', header: 'Unit price', variant: 'num', align: 'right', width: '130px', render: (v) => D.money(v) },
              { key: 'blocked', header: 'Status', width: '110px', render: (v) => v ? <Badge status="danger" dot>Blocked</Badge> : <Badge status="success" dot>Active</Badge> },
            ]}
            rows={D.items}
          />
        </Card>
      </div>
    );
  }

  /* ============ GENERIC PLACEHOLDER for un-built modules ============ */
  function Placeholder({ title, go }) {
    return (
      <div className="erp-stack">
        <div className="erp-pagehead"><div style={{ flex: 1 }}><h1>{title}</h1></div></div>
        <Card>
          <div className="nx-empty">
            <div className="nx-empty__icon"><Icon n="hammer" /></div>
            <div className="nx-empty__title">Module preview not included in this kit</div>
            <div className="nx-empty__text">This UI kit recreates the Sales &amp; Inventory surfaces. {title} follows the same shell, list and FactBox patterns.</div>
            <Button variant="secondary" onClick={() => go({ screen: 'dashboard' })}>Back to dashboard</Button>
          </div>
        </Card>
      </div>
    );
  }

  /* ============ SETTINGS ============ */
  function Settings({ go }) {
    const [tab, setTab] = useState('empresa');
    const { Switch, Checkbox } = NS;

    /* --- mock users with per-permission state --- */
    const [users, setUsers] = useState([
      {
        id: 1, name: 'José Ramírez', email: 'jramirez@acmedist.do', initials: 'JR', active: true,
        perms: { ver_ventas: true, editar_ventas: true, contabilizar: true, ver_compras: true, editar_compras: false, ver_contabilidad: true, editar_contabilidad: false, ver_usuarios: false, configuracion: false }
      },
      {
        id: 2, name: 'Laura Méndez', email: 'lmendez@acmedist.do', initials: 'LM', active: true,
        perms: { ver_ventas: true, editar_ventas: true, contabilizar: false, ver_compras: false, editar_compras: false, ver_contabilidad: false, editar_contabilidad: false, ver_usuarios: false, configuracion: false }
      },
      {
        id: 3, name: 'Carla Objío', email: 'cobjio@acmedist.do', initials: 'CO', active: true,
        perms: { ver_ventas: true, editar_ventas: false, contabilizar: false, ver_compras: true, editar_compras: true, ver_contabilidad: false, editar_contabilidad: false, ver_usuarios: false, configuracion: false }
      },
    ]);

    /* --- mock modules state --- */
    const [modules, setModules] = useState([
      { id: 'ventas',       label: 'Ventas',        desc: 'Clientes, facturas, notas de crédito y cobros.',          icon: 'file-text',     on: true },
      { id: 'compras',      label: 'Compras',        desc: 'Proveedores, órdenes de compra y cuentas por pagar.',    icon: 'shopping-cart', on: true },
      { id: 'inventario',   label: 'Inventario',     desc: 'Artículos, stock por almacén y movimientos.',            icon: 'package',       on: true },
      { id: 'contabilidad', label: 'Contabilidad',   desc: 'Plan de cuentas, asientos y estados financieros.',       icon: 'book-open',     on: true },
      { id: 'bancos',       label: 'Bancos',         desc: 'Cuentas bancarias, conciliación y pagos.',               icon: 'landmark',      on: false },
      { id: 'pos',          label: 'Punto de venta', desc: 'Caja registradora para ventas en mostrador.',            icon: 'receipt',       on: false },
    ]);

    const togglePerm = (userId, perm) => {
      setUsers(us => us.map(u => u.id !== userId ? u : { ...u, perms: { ...u.perms, [perm]: !u.perms[perm] } }));
    };
    const toggleModule = (id) => setModules(ms => ms.map(m => m.id === id ? { ...m, on: !m.on } : m));

    const PERMS = [
      { id: 'ver_ventas',         label: 'Ver ventas',              group: 'Ventas' },
      { id: 'editar_ventas',      label: 'Crear y editar facturas', group: 'Ventas' },
      { id: 'contabilizar',       label: 'Contabilizar documentos', group: 'Ventas' },
      { id: 'ver_compras',        label: 'Ver compras',             group: 'Compras' },
      { id: 'editar_compras',     label: 'Crear órdenes de compra', group: 'Compras' },
      { id: 'ver_contabilidad',   label: 'Ver contabilidad',        group: 'Finanzas' },
      { id: 'editar_contabilidad',label: 'Registrar asientos',      group: 'Finanzas' },
      { id: 'ver_usuarios',       label: 'Ver otros usuarios',      group: 'Configuración' },
      { id: 'configuracion',      label: 'Cambiar configuración',   group: 'Configuración' },
    ];
    const permGroups = [...new Set(PERMS.map(p => p.group))];

    const MENU = [
      { id: 'empresa',   label: 'Mi empresa',          icon: 'building-2' },
      { id: 'modulos',   label: 'Módulos activos',      icon: 'layout-grid' },
      { id: 'usuarios',  label: 'Usuarios y permisos',  icon: 'users' },
      { id: 'numeracion',label: 'Numeración',            icon: 'hash' },
      { id: 'impuestos', label: 'Impuestos',             icon: 'percent' },
    ];

    return (
      <div className="erp-stack">
        <div className="erp-pagehead">
          <div style={{ flex: 1 }}>
            <h1>Configuración</h1>
            <div className="erp-pagehead__meta">Acme Distribución · RNC 1-01-12345-0</div>
          </div>
        </div>

        <div style={{ display: 'grid', gridTemplateColumns: '210px 1fr', gap: 24, alignItems: 'start' }}>
          {/* Left nav */}
          <div style={{ display: 'flex', flexDirection: 'column', gap: 1 }}>
            {MENU.map(m => (
              <button key={m.id}
                onClick={() => setTab(m.id)}
                style={{ display: 'flex', alignItems: 'center', gap: 10, padding: '9px 12px', borderRadius: 'var(--radius-sm)', border: 'none', cursor: 'pointer', fontSize: 13.5, fontWeight: 500, textAlign: 'left', transition: 'all 120ms', background: tab === m.id ? 'var(--surface-selected)' : 'transparent', color: tab === m.id ? 'var(--action)' : 'var(--text-body)' }}>
                <Icon n={m.icon} style={{ width: 16, height: 16, opacity: tab === m.id ? 1 : 0.6 }} />
                {m.label}
              </button>
            ))}
          </div>

          {/* Content */}
          <div>
            {/* ─── MI EMPRESA ─── */}
            {tab === 'empresa' && (
              <Card title="Mi empresa">
                <div className="erp-grid-2" style={{ gap: 20 }}>
                  <Input label="Nombre de la empresa" defaultValue="Acme Distribución, S.R.L." />
                  <Input label="RNC" mono defaultValue="1-01-12345-0" />
                  <Input label="Correo principal" defaultValue="admin@acmedist.do" />
                  <Input label="Teléfono" defaultValue="(809) 555-0100" />
                  <Input label="Dirección" defaultValue="Av. Winston Churchill, Santo Domingo" />
                  <Input label="Ciudad" defaultValue="Santo Domingo" />
                </div>
                <div style={{ marginTop: 20, paddingTop: 16, borderTop: '1px solid var(--border-subtle)', display: 'flex', justifyContent: 'flex-end', gap: 10 }}>
                  <Button variant="secondary">Cancelar</Button>
                  <Button variant="primary">Guardar cambios</Button>
                </div>
              </Card>
            )}

            {/* ─── MÓDULOS ─── */}
            {tab === 'modulos' && (
              <Card title="Módulos activos" eyebrow="Activá solo lo que usás">
                <p style={{ fontSize: 13.5, color: 'var(--text-muted)', marginBottom: 20, lineHeight: 1.65 }}>
                  El sistema se adapta a tu operación. Desactivá los módulos que no usás — no aparecerán en el menú ni en la búsqueda. Podés reactivarlos en cualquier momento.
                </p>
                <div style={{ display: 'flex', flexDirection: 'column', gap: 1 }}>
                  {modules.map(m => (
                    <div key={m.id} style={{ display: 'flex', alignItems: 'center', gap: 14, padding: '14px 16px', background: 'var(--surface-sunken)', borderRadius: 'var(--radius-sm)', border: '1px solid var(--border-subtle)', marginBottom: 8 }}>
                      <div style={{ width: 36, height: 36, borderRadius: 8, background: m.on ? 'var(--action-tint)' : 'var(--slate-100)', display: 'flex', alignItems: 'center', justifyContent: 'center', flex: 'none' }}>
                        <Icon n={m.icon} style={{ width: 17, height: 17, color: m.on ? 'var(--action)' : 'var(--text-faint)' }} />
                      </div>
                      <div style={{ flex: 1, minWidth: 0 }}>
                        <div style={{ fontSize: 14, fontWeight: 600, color: 'var(--text-strong)' }}>{m.label}</div>
                        <div style={{ fontSize: 12.5, color: 'var(--text-muted)', marginTop: 2 }}>{m.desc}</div>
                      </div>
                      <Switch checked={m.on} onChange={() => toggleModule(m.id)} />
                    </div>
                  ))}
                </div>
              </Card>
            )}

            {/* ─── USUARIOS Y PERMISOS ─── */}
            {tab === 'usuarios' && (
              <Card title="Usuarios y permisos" eyebrow="Qué puede ver y hacer cada persona" padded={false}>
                <div style={{ padding: '12px 18px 14px', borderBottom: '1px solid var(--border-subtle)' }}>
                  <p style={{ fontSize: 13, color: 'var(--text-muted)', lineHeight: 1.65 }}>
                    Aquí no hay "tipos de usuario". Cada persona tiene sus propios permisos — controlás exactamente qué puede ver y qué puede hacer, sin depender de categorías fijas.
                  </p>
                </div>
                <div style={{ overflowX: 'auto' }}>
                  <table style={{ width: '100%', borderCollapse: 'collapse', fontSize: 13 }}>
                    <thead>
                      <tr>
                        <th style={{ textAlign: 'left', padding: '10px 18px', fontWeight: 600, fontSize: 12, color: 'var(--text-muted)', borderBottom: '1px solid var(--border-default)', background: 'var(--surface-sunken)', position: 'sticky', left: 0, minWidth: 200 }}>
                          Usuario
                        </th>
                        {permGroups.map(g => (
                          <th key={g} colSpan={PERMS.filter(p => p.group === g).length}
                              style={{ textAlign: 'center', padding: '10px 8px', fontWeight: 700, fontSize: 10, letterSpacing: '0.06em', textTransform: 'uppercase', color: 'var(--text-faint)', borderBottom: '1px solid var(--border-default)', background: 'var(--surface-sunken)', whiteSpace: 'nowrap', borderLeft: '1px solid var(--border-subtle)' }}>
                            {g}
                          </th>
                        ))}
                      </tr>
                      <tr>
                        <th style={{ background: 'var(--surface-sunken)', borderBottom: '1px solid var(--border-default)', position: 'sticky', left: 0 }}></th>
                        {PERMS.map((p, i) => (
                          <th key={p.id} style={{ padding: '6px 10px', fontSize: 11, fontWeight: 500, color: 'var(--text-muted)', borderBottom: '1px solid var(--border-default)', background: 'var(--surface-sunken)', whiteSpace: 'nowrap', textAlign: 'center', borderLeft: i === 0 || PERMS[i - 1]?.group !== p.group ? '1px solid var(--border-subtle)' : 'none', maxWidth: 110 }}>
                            {p.label}
                          </th>
                        ))}
                      </tr>
                    </thead>
                    <tbody>
                      {users.map((u, ui) => (
                        <tr key={u.id} style={{ borderBottom: '1px solid var(--border-subtle)' }}>
                          <td style={{ padding: '12px 18px', background: 'var(--surface)', position: 'sticky', left: 0 }}>
                            <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
                              <Avatar name={u.name} shape="circle" size="sm" />
                              <div>
                                <div style={{ fontWeight: 600, color: 'var(--text-strong)', fontSize: 13 }}>{u.name}</div>
                                <div style={{ fontSize: 11.5, color: 'var(--text-muted)' }}>{u.email}</div>
                              </div>
                            </div>
                          </td>
                          {PERMS.map((p, pi) => (
                            <td key={p.id} style={{ textAlign: 'center', padding: '12px 10px', borderLeft: pi === 0 || PERMS[pi - 1]?.group !== p.group ? '1px solid var(--border-subtle)' : 'none' }}>
                              <Checkbox checked={u.perms[p.id]} onChange={() => togglePerm(u.id, p.id)} style={{ justifyContent: 'center' }} />
                            </td>
                          ))}
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
                <div style={{ padding: '12px 18px', borderTop: '1px solid var(--border-subtle)', display: 'flex', justifyContent: 'flex-end' }}>
                  <Button variant="primary" leftIcon={<Icon n="user-plus" />}>Agregar usuario</Button>
                </div>
              </Card>
            )}

            {/* ─── NUMERACIÓN ─── */}
            {tab === 'numeracion' && (
              <Card title="Numeración de documentos">
                <p style={{ fontSize: 13.5, color: 'var(--text-muted)', marginBottom: 20, lineHeight: 1.65 }}>
                  Configurá las series de NCF y los secuenciales de tus documentos. El sistema asigna el número siguiente automáticamente.
                </p>
                <div style={{ display: 'flex', flexDirection: 'column', gap: 12 }}>
                  {[
                    { tipo: 'Facturas de venta', serie: 'B01', desde: '00000001', hasta: '00009999', actual: '00000847' },
                    { tipo: 'Notas de crédito', serie: 'B04', desde: '00000001', hasta: '00001999', actual: '00000219' },
                    { tipo: 'Compras y gastos', serie: 'B11', desde: '00000001', hasta: '00001999', actual: '00000042' },
                  ].map(row => (
                    <div key={row.serie} style={{ display: 'grid', gridTemplateColumns: '1fr 90px 130px 130px 130px', gap: 12, alignItems: 'end', paddingBottom: 16, borderBottom: '1px solid var(--border-subtle)' }}>
                      <Input label="Tipo de comprobante" defaultValue={row.tipo} />
                      <Input label="Serie" mono defaultValue={row.serie} />
                      <Input label="Desde" mono defaultValue={row.desde} />
                      <Input label="Hasta" mono defaultValue={row.hasta} />
                      <Input label="Número actual" mono defaultValue={row.actual} hint="Próximo: +1" />
                    </div>
                  ))}
                </div>
                <div style={{ marginTop: 20, display: 'flex', justifyContent: 'flex-end', gap: 10 }}>
                  <Button variant="secondary">Cancelar</Button>
                  <Button variant="primary">Guardar</Button>
                </div>
              </Card>
            )}

            {/* ─── IMPUESTOS ─── */}
            {tab === 'impuestos' && (
              <Card title="Impuestos">
                <p style={{ fontSize: 13.5, color: 'var(--text-muted)', marginBottom: 20, lineHeight: 1.65 }}>
                  Configurá los grupos de impuestos que se aplican a tus artículos y servicios. El ITBIS se calcula automáticamente en cada línea de documento.
                </p>
                <div style={{ display: 'flex', flexDirection: 'column', gap: 10 }}>
                  {[
                    { codigo: 'ITBIS-18', nombre: 'ITBIS 18%', tasa: '18.00', aplica: 'Mayoría de bienes y servicios' },
                    { codigo: 'ITBIS-16', nombre: 'ITBIS 16%', tasa: '16.00', aplica: 'Servicios financieros y de seguros' },
                    { codigo: 'EXENTO',   nombre: 'Exento',    tasa: '0.00',  aplica: 'Alimentos básicos, medicamentos, exportaciones' },
                  ].map(row => (
                    <div key={row.codigo} style={{ display: 'grid', gridTemplateColumns: '110px 1fr 100px 1fr', gap: 12, alignItems: 'end', paddingBottom: 16, borderBottom: '1px solid var(--border-subtle)' }}>
                      <Input label="Código" mono defaultValue={row.codigo} />
                      <Input label="Nombre" defaultValue={row.nombre} />
                      <Input label="Tasa %" mono defaultValue={row.tasa} />
                      <Input label="Aplicable a" defaultValue={row.aplica} />
                    </div>
                  ))}
                </div>
                <div style={{ marginTop: 20, display: 'flex', justifyContent: 'flex-end', gap: 10 }}>
                  <Button variant="secondary">Cancelar</Button>
                  <Button variant="primary">Guardar</Button>
                </div>
              </Card>
            )}
          </div>
        </div>
      </div>
    );
  }

  Object.assign(window, { NXDashboard: Dashboard, NXCustomerList: CustomerList, NXCustomerCard: CustomerCard, NXSalesInvoice: SalesInvoice, NXItemList: ItemList, NXPlaceholder: Placeholder, NXSettings: Settings });
})();
