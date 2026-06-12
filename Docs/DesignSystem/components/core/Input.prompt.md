Labeled text input with optional hint/error and leading adornment. Use `mono` for amounts and document numbers.

```jsx
<Input label="Customer name" placeholder="Search…" required />
<Input label="Unit price" mono adornment={<span>RD$</span>} defaultValue="1,240.00" />
<Input label="Email" error="Enter a valid email" defaultValue="bad@" />
```
