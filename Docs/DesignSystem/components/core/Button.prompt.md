Buttons trigger actions; use exactly one `primary` (ledger-emerald) per region and `secondary` for the rest.

```jsx
<Button variant="primary" leftIcon={<i data-lucide="plus" />}>New invoice</Button>
<Button variant="secondary">Cancel</Button>
<Button variant="ghost" size="sm">Filter</Button>
<Button variant="danger">Delete</Button>
```

Variants: `primary`, `secondary`, `ghost`, `subtle`, `danger`. Sizes: `sm`, `md`, `lg`. Use `block` for full-width (mobile, dialogs). Pass `disabled` natively. Icons are nodes via `leftIcon` / `rightIcon`.
