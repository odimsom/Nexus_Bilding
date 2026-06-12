The workhorse list view. Define columns (use `variant: 'doc'` for numbers, `variant: 'num'` for amounts) and render cells with `render` for badges and drill links.

```jsx
<DataTable
  selectedId={sel}
  onRowClick={(r) => openCustomer(r.no)}
  columns={[
    { key: 'no', header: 'No.', variant: 'doc', width: '140px' },
    { key: 'name', header: 'Customer' },
    { key: 'status', header: 'Status', render: (v) => <Badge status={v.tone} dot>{v.label}</Badge> },
    { key: 'balance', header: 'Balance', variant: 'num', align: 'right' },
  ]}
  rows={customers}
/>
```

`onRowClick` is how you implement drill-through: every row opens its record.
