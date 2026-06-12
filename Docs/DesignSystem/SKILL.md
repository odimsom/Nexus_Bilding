---
name: nexus-billing-design
description: Use this skill to generate well-branded interfaces and assets for Nexus Billing — a multi-tenant ERP & billing platform for the Dominican market. Contains essential design guidelines, colors, type, fonts, logo assets, and UI kit components for prototyping or production work. Covers Sales, Inventory, Finance, Purchasing, and Administration modules with authentic domain terminology (Customer No., Posting Date, ITBIS, Bill-to, GL Account, etc.)
user-invocable: true
---

Read the README.md file within this skill, and explore the other available files.

## Design system structure

- `styles.css` — single entry point (link this + optionally `_ds_bundle.js`)
- `tokens/` — colors, typography, spacing, elevation, motion, base resets, dark theme
- `system/` — CSS class layer: `.nx-btn`, `.nx-input`, `.nx-table`, `.nx-badge`, `.nx-factbox`, etc.
- `components/core/` — Button, IconButton, Input, Select, Checkbox, Switch, Badge, Tag, Avatar
- `components/layout/` — Card
- `components/data/` — DataTable, StatTile, KeyValue, FactBox
- `components/navigation/` — Tabs, Breadcrumb
- `components/feedback/` — Banner, EmptyState
- `assets/` — logo-mark.svg, logo-tile.svg, logo-mono.svg
- `ui_kits/erp/` — interactive ERP app (Login → Dashboard → Customers → FactBox → Invoice)
- `ui_kits/landing/` — marketing landing page with ECharts dashboard preview
- `guidelines/` — foundation specimen cards (colors, typography, spacing, brand, themes)

## Key design decisions

**Color:** Ledger Emerald (`--emerald-600`) is the single action color. Nexus Gold (`--gold-500`) only in the logo. Cool ink-slate neutrals (`--slate-*`). Status: success=emerald, info=blue, warn=amber, danger=red. Money: positive=emerald, negative=red. Full dark theme via `[data-theme="dark"]`.

**Typography:** Space Grotesk (display/headings, tracking -0.02em) + Hanken Grotesk (UI base 14px) + JetBrains Mono (all numbers, amounts, document codes — always tabular).

**The Nexus pattern:** Every record screen has a right-rail **FactBox** showing statistics + drill links to related records. This is non-negotiable — always include it. The whole product idea is "any record, one click from the next."

**Domain vocabulary:** Use exact ERP terms — Customer (not client), Item (not product), No. for record numbers, Posting Date, Bill-to, Balance (LCY), ITBIS (18% VAT). Document types: Invoice, Credit Memo, Quote. Statuses: Posted, Open, Pending, Overdue, Draft, Blocked.

**Charts:** Apache ECharts (`ngx-echarts` for Angular). Themed by CSS custom properties. Used for AR aging bars, monthly sales line charts, KPI dashboards.

**Angular integration:** `ngx-echarts` for charts, `ng2-charts` as alternative. Token theming via CSS custom properties on `:root` / `[data-theme="dark"]`. Multi-language with `@ngx-translate/core`. Two themes (light/dark) toggled by setting `data-theme` on `<html>`.

## If creating visual artifacts

Copy logo assets from `assets/`. Link `styles.css` for tokens + classes. Load `_ds_bundle.js` and use `window.NexusBillingDesignSystem_f29fd0` for React components. Use Lucide icons (CDN: `https://unpkg.com/lucide@0.460.0/dist/umd/lucide.min.js`). Use ECharts (CDN: `https://cdn.jsdelivr.net/npm/echarts@5.5.0/dist/echarts.min.js`) for any dashboard charts.

## If working on production Angular code

- Global styles: link `styles.css` from the design system
- Component styles: use `nx-*` CSS classes directly, or wrap in Angular components that apply those classes
- Token reference: `var(--action)`, `var(--text-strong)`, `var(--font-display)`, etc.
- Dark theme: set `document.documentElement.setAttribute('data-theme', 'dark')` and persist to localStorage
- Icons: Lucide via `ngx-lucide` or direct CDN load
- Charts: `ngx-echarts` wrapping Apache ECharts; theme tokens map directly

## If the user invokes this skill without guidance

Ask what they want to build or design. Good questions:
1. Is this for the Angular production app or a throwaway HTML prototype?
2. Which module/screen? (Sales, Inventory, Finance, Purchasing, Admin, or Landing)
3. Light or dark theme, or both?
4. New screen or modification of an existing one?
5. Should it include a FactBox? (Almost always yes for record screens)

Then act as an expert ERP designer who knows this domain deeply — and always include drill-through links and the FactBox.
