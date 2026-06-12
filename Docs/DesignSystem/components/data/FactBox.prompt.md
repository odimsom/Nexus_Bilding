The FactBox is Nexus Billing's defining navigation idea — a right-rail panel that surfaces everything connected to the record you're looking at, so you never have to leave to find related data.

```jsx
<FactBox
  eyebrow="Customer"
  title="Altagracia Comercial"
  sections={[
    { label: 'Statistics', content: <KeyValue items={[
        { key: 'Balance (LCY)', value: 'RD$ 84,120.00', mono: true },
        { key: 'Overdue', value: 'RD$ 12,400.00', mono: true },
      ]} /> },
    { label: 'Related', content: (
      <ul className="nx-linklist">
        <li><a className="nx-link" href="#">Ongoing sales orders (3)</a></li>
        <li><a className="nx-link" href="#">Posted invoices (28)</a></li>
        <li><a className="nx-link" href="#">Customer ledger entries</a></li>
      </ul>
    ) },
  ]}
/>
```

Always include a "Related" section with drill links — that is the whole point.
