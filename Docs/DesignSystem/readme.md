# Nexus Billing — Design System

The brand and interface system for **Nexus Billing**, a modern multi-tenant
**ERP & billing platform**. This repository defines the tokens, typography,
logo, components and product UI that make every Nexus Billing surface feel
clear, connected and trustworthy.

> **Design principle — "one click from the next."** An ERP is a web of related
> records: a customer has invoices, an invoice has items, an item has vendors.
> Nexus Billing is built so that from *any* record you can reach everything
> connected to it — primarily through the **FactBox** rail and inline **drill
> links**. Nothing is a dead end; nothing in the view breaks or overflows.

---

## 1. Product context

Nexus Billing is an **Enterprise Resource Planning (ERP)** system built on
Domain-Driven Design + Onion architecture (.NET core, Angular presentation).
It is organized into strict modules, each of which this design system serves:

| Module | Core records | Key terms |
|---|---|---|
| **Sales** | Customers, Sales Headers (Invoices, Credit Memos, Quotes) | Sell-to, Bill-to, Posting date |
| **Purchasing** | Vendors, Purchase Headers | Buy-from, Pay-to |
| **Inventory** | Items, Locations, Units of Measure | No., Base UoM, On hand, Blocked |
| **Finance** | GL Accounts (Chart of Accounts) | Account type, Income/Balance |
| **Security** | Users, Permission Sets | — |
| **Administration** | Tenants | TenantId (multi-tenant) |

The vocabulary is deliberately ERP-classic (the back-end mirrors Microsoft
Dynamics-style naming: `No.`, `Posting Date`, `Bill-to Name`, `Base Unit of
Measure`). The design system keeps that precise, professional language while
making it approachable.

### Sources used to build this system
- **GitHub (back-end, source of the domain):**
  [`odimsom/Nexus_Bilding`](https://github.com/odimsom/Nexus_Bilding) — the .NET
  Core / DDD ERP. The domain entities (`Customer`, `Item`, `SalesHeader`,
  `Vendor`, `GLAccount`, `Tenant`) and the
  [`Docs/Design_Plan_Domain_Persistence.md`](https://github.com/odimsom/Nexus_Bilding/blob/main/Docs/Design_Plan_Domain_Persistence.md)
  drove the module map, record fields and terminology above.
- **No front-end existed** in the repository (the README mentions an Angular
  presentation layer that is not yet committed). The entire visual language —
  logo, color, type, components and screens — was **designed fresh** for this
  system. Explore the back-end repo to extend the domain coverage (e.g. add
  Purchase Order or GL screens) with real field names.

---

## 2. Content & voice fundamentals

How Nexus Billing writes. The tone is **precise, calm and operational** — a
tool for professionals doing exacting financial work.

- **Voice:** clear, direct, second-person for actions ("Open the customer",
  "Post invoice"). Never chatty, never cute. Confidence without hype.
- **Casing:** **Sentence case** for everything — page titles, buttons, menus
  ("New invoice", not "New Invoice"). The only Title Case is ERP **field
  labels** that are domain terms ("Sell-to customer", "Posting date", "Base
  UoM") and document type names ("Credit Memo").
- **Terminology is exact and consistent.** Use the domain word every time:
  *Customer* (not client), *Item* (not product), *No.* for record numbers,
  *Posting date*, *Bill-to*, *Balance (LCY)*. LCY = Local Currency (RD$).
- **Numbers are first-class.** Always formatted, always tabular: `RD$ 84,120.00`.
  Negative/credit amounts in red with a real minus (−), positives in emerald.
- **Status is a word + a color**, never color alone: *Posted, Open, Pending,
  Overdue, Draft, Blocked*.
- **Buttons are verb-first and short:** "New invoice", "Post", "Edit", "Export".
  Primary action describes the outcome, not "Submit".
- **Empty states** state the fact then offer the next step: "No open invoices —
  Documents you create will appear here."
- **No emoji.** None, anywhere in the product. Iconography is line-based (see §5).
- **Microcopy examples:**
  - Banner (success): "Invoice posted — SI-2026-001847 was posted to the ledger."
  - Banner (danger): "Posting failed — Period 2026-05 is closed."
  - Reminder: "3 documents pending approval."

---

## 3. Visual foundations

The complete visual language. Render the **Design System tab** to see every
token and component as a live specimen card.

### Color
- **Neutral, paper-like canvas.** The app background is a soft cool slate
  (`--canvas` = `--slate-50`); cards/rows are white (`--surface`). Long work
  sessions stay calm; dense data reads clearly. Chrome (sidebar, command bar,
  tooltips) is deep ink-slate `--slate-900`.
- **One confident action color — "Ledger Emerald"** (`--action` = `--emerald-600`).
  It carries every primary button, link, selected row, focus ring and positive
  amount. Green signals money & "in the black," and is deliberately *not* the
  default-blue/purple of generic SaaS.
- **A single signature spark — "Nexus Gold"** (`--gold-*`) appears only in the
  logo node and as a data-viz accent. Used sparingly, it reads as craftsmanship.
- **Reserved semantics:** success=emerald, info=blue, warning=amber, danger=red,
  neutral=slate. Each is a **fg-on-tint pair** (`--status-*-fg` / `--status-*-bg`).
- **Money semantics:** `--money-positive` (emerald), `--money-negative` (red),
  `--money-zero` (faint slate).
- **Data-viz** uses a 6-hue categorical scale (`--viz-1…6`).

### Typography
Three families, each with a job (see `tokens/typography.css`):
- **Space Grotesk** — brand wordmark, page titles, big KPI display. Geometric,
  slightly technical — it gives "Nexus" its character. Tight tracking (-0.02em).
- **Hanken Grotesk** — all UI text, labels, body, table cells. Highly legible at
  the **14px UI base** (chosen over 16px so tables/forms stay dense yet readable).
- **JetBrains Mono** — every number that matters: amounts, document numbers,
  codes, dates. Always **tabular** so figures align in columns. This is a core
  ERP requirement, not decoration.

### Spacing, radii, layout
- **4px grid** (`--space-1…16`); most density lives between 4–16px.
- **Restrained radii** — `--radius-sm` (5px) on controls, `--radius-md` (8px) on
  cards. Software, not playful. Nothing is a pill except toggles and dots.
- **Fixed layout rails:** sidebar `248px`, topbar `52px`, command bar `44px`,
  FactBox `300px`, content max `1440px`. These never shift — the frame is stable.

### Surfaces, borders & elevation
- **Borders do the separating, not shadows.** Resting surfaces (cards, rows,
  inputs) use 1px slate borders (`--border-subtle` / `--border-default`). The UI
  feels flat and certain.
- **Shadows are soft, cool-tinted (slate, not black) and low-spread**, reserved
  only for *floating* UI: menus, popovers, modals, toasts (`--shadow-md/lg/xl`).
- **Cards:** white surface, `--border-subtle`, `--radius-md`, optional header
  (bottom border) and footer (slate-25 band). No drop shadow at rest.

### Motion
- **Functional and quick. Nothing bounces.** `--dur-fast` (140ms) for hover /
  press / toggles, `--dur-base` (200ms) for menus/tabs. Easing is a calm
  `cubic-bezier(0.2,0,0.2,1)`. Transitions communicate state, never decorate.
- **States:** hover = subtle surface tint or one step darker; **press = 1px
  nudge** + darker; focus = 2px emerald ring with offset. Disabled = 45–50%
  opacity, `not-allowed`.
- Respect `prefers-reduced-motion` for any non-essential animation.

### Imagery
- This is a data product; **photography is minimal**. Where used (e.g. a login
  aside), imagery is dark, cool and quiet so it never competes with data.
- No gradients in the working UI except the logo tile. No textures, no glassmorphism.

---

## 4. The signature pattern — FactBox & drill-through

This is what makes Nexus Billing *Nexus*. Every record screen pairs the main
content with a right-rail **FactBox** that surfaces:
1. **Statistics** of the focused record (balances, limits, counts).
2. **Related** — drill links to connected records (a customer's invoices, its
   ledger entries, items sold; an invoice's customer and item availability).
3. Contextual people/metadata (salesperson, etc.).

Combined with **inline drill links** (`.nx-link`) inside tables and document
headers, the rule holds: *from any record you can reach anything related to it.*
When you build new screens, **always include a FactBox with a "Related"
section** — that is the product, not an add-on.

---

## 5. Iconography

- **System:** [**Lucide**](https://lucide.dev) — clean, consistent 1.75–2px
  stroke line icons. The back-end repo contains **no icon assets**, so Lucide was
  selected as the system's icon language (a deliberate substitution; flagged for
  the user). It matches the calm, professional, line-based aesthetic.
- **Usage:** load from CDN (`https://unpkg.com/lucide`) and render
  `<i data-lucide="name"></i>`, then call `lucide.createIcons()`. In React kits
  this is wrapped as `<Icon n="users" />`. Sizes: 16px in buttons, 17px in nav
  and icon-buttons, 22px in empty-state tiles.
- **Common glyphs:** `layout-dashboard, users, file-text, truck, package,
  book-open, map-pin, search, bell, plus, pencil, printer, check, download,
  sliders-horizontal, arrow-up-right` (the drill affordance), `circle-help,
  ban, alert-triangle, check-circle-2`.
- **No emoji, ever.** Status uses a colored dot + word, not an emoji.
- **Brand mark** is the only custom vector art — see `assets/` (§7). All
  functional UI glyphs come from Lucide; do not hand-draw icons.

---

## 6. Logo & brand mark

The mark is an original geometric **"N"**: two **ledger pillars** (two
entities / two columns of a ledger) joined by a diagonal **beam** — the *nexus*,
the flow between them — with a **gold node** at the center where everything
connects. It encodes the whole product idea: records, linked.

- `assets/logo-mark.svg` — primary mark (emerald + gold; works on light & dark).
- `assets/logo-tile.svg` — app-icon / favicon (white mark on emerald gradient tile).
- `assets/logo-mono.svg` — single-color (`currentColor`) for print / one-color use.
- **Wordmark lockup:** mark + "Nexus" (Space Grotesk 700) + " Billing" (500,
  muted), optional eyebrow "ERP & Billing Platform". See `guidelines/brand-logo.card.html`.
- **Clear space** = the height of one pillar. **Don't** recolor the mark outside
  the emerald/gold/mono set, stretch it, or add effects.

---

## 7. Repository index / manifest

**Root**
- `styles.css` — the single entry point consumers link (only `@import`s).
- `readme.md` — this guide. · `SKILL.md` — Agent-Skill manifest.

**`tokens/`** — `fonts.css`, `colors.css`, `typography.css`, `spacing.css`,
`elevation.css`, `motion.css`, `base.css` (global resets).

**`system/`** — the classed visual layer (shipped via `styles.css`):
`controls.css` (buttons, inputs, selects, checkbox, switch), `display.css`
(badge, tag, avatar, card, stat, key/value, table, factbox, links),
`feedback.css` (banner, tooltip, tabs, breadcrumb, empty state).

**`tokens/theme-dark.css`** — Dark theme under `[data-theme="dark"]`. Toggle with `document.documentElement.setAttribute('data-theme','dark')`, persist via `nx-theme` in localStorage. ERP app has a sun/moon toggle.

**`components/`** — React primitives (`.jsx` + `.d.ts` + `.prompt.md`, one card per dir):
- `core/` — Button, IconButton, Input, Select, Checkbox, Switch, Badge, Tag, Avatar
- `layout/` — Card (collapsible), **Collapsible**
- `data/` — DataTable, StatTile, **KeyValue** (secondary-field toggle), **FactBox** (collapsible sections)
- `navigation/` — Tabs, Breadcrumb
- `feedback/` — Banner, EmptyState

**`ui_kits/erp/`** — interactive ERP app: Login → Dashboard → Customers → **Customer Card (FactBox, collapsible sections, secondary fields, dark theme toggle)** → Sales Invoice (NCF, ITBIS) → Items. Files: `app.css`, `data.js` (RNC + NCF), `shell.jsx`, `screens.jsx`.

**`ui_kits/landing/`** — marketing landing page with ECharts AR aging chart, hero, module grid, CTA. Light/dark theme toggle included.

**`templates/landing/`** — self-contained landing template (copy-paste ready, `ds-base.js` loads the DS automatically).

**`guidelines/`** — foundation specimen cards (Brand, Colors incl. light/dark comparison, Type, Spacing).

**`assets/`** — logo-mark, logo-tile, logo-mono SVGs.

---

## 8. Field visibility & collapsible sections

Users can reduce visual load by collapsing sections they don't currently need. Three levels:

1. **Card-level** — `<Card collapsible collapseKey="unique">` collapses the entire body on header click.
2. **Section-level** — `<Collapsible label="..." collapseKey="...">` wraps any content. FactBox sections collapse individually.
3. **Field-level** — `KeyValue` items with `secondary: true` are hidden by default. A "Show all fields (N)" toggle reveals them. Use for posting groups, tax codes, dimensions — detail rarely needed daily.

All states persist to `localStorage` under `nx-collapse-<key>` and `nx-kv-more-<key>`, surviving navigation and reload.

---

## 9. Configuración y permisos

### Módulos activos
El sistema se adapta al alcance del negocio. Desde **Configuración → Módulos activos** el administrador activa solo los módulos que usa — los que están desactivados no aparecen en el menú ni en la búsqueda. Sin tiers, sin licencias por módulo.

### Filosofía de permisos — lo que importa es qué puede VER y HACER
Nexus Billing no usa "tipos de usuario" ni roles rígidos. Cada persona tiene su propio conjunto de permisos que define:
- **Qué puede ver** — qué módulos y pantallas están visibles para esta persona
- **Qué puede hacer** — crear facturas, contabilizar documentos, registrar asientos, cambiar configuración, etc.

Dos personas que trabajan en ventas pueden tener permisos distintos: una puede contabilizar y la otra no. Esa diferencia no la define un rol — la define un permiso específico activado o desactivado por usuario.

**Implementación Angular:** la directiva `*appHasPermission="'nombre_del_permiso'"` controla qué elementos del DOM se renderizan. Si el usuario no tiene el permiso, el elemento no existe — no solo está deshabilitado. Los permisos se cargan al autenticar y viajan en el JWT.

### Navegación rápida — command palette
`⌘K` (o `Ctrl+K`) abre el buscador de vistas desde cualquier pantalla. Desde ahí:
- Navegación a cualquier módulo
- Acciones rápidas: nueva factura, nuevo cliente, nuevo artículo
- **Atajos anclados al sidebar** — cada usuario elige qué vistas quiere a mano. Se guardan por usuario en el navegador.

---

## 10. Using the system

Consumers link one file and use either the classes or the React components:

```html
<link rel="stylesheet" href="styles.css">
<!-- React: load the compiled bundle, then read components off the namespace -->
<script src="_ds_bundle.js"></script>
<script>
  const { Button, DataTable, FactBox } = window.NexusBillingDesignSystem_f29fd0;
</script>
```

Plain HTML can use the same look with the `nx-*` classes directly
(`<button class="nx-btn nx-btn--primary">Post</button>`).

> **Note on fonts:** webfonts currently load from **Google Fonts CDN** (see
> `tokens/fonts.css`). To fully self-host/offline, drop the `.woff2` files into
> `assets/fonts/` and replace the `@import` with local `@font-face` rules.
