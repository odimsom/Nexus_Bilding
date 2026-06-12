Bordered surface for grouping content. Use `padded={false}` when the body is a flush DataTable.

```jsx
<Card title="Open invoices" actions={<Button variant="ghost" size="sm">Export</Button>} padded={false}>
  <DataTable columns={cols} rows={rows} />
</Card>
```
