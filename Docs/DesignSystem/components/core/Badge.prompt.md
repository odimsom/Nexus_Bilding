Compact status pill for record/document states. Map document status to a semantic color, not a literal one.

```jsx
<Badge status="success" dot>Posted</Badge>
<Badge status="info" dot>Open</Badge>
<Badge status="warn" dot>Pending approval</Badge>
<Badge status="danger" dot>Overdue</Badge>
<Badge>Draft</Badge>
```

Convention: Posted→success, Open→info, Pending→warn, Overdue/Blocked→danger, Draft→neutral.
