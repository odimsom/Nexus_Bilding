Animated collapsible section that persists open/closed to localStorage. Essential for dense ERP record cards where users want to hide sections they don't need.

```jsx
<Collapsible label="Shipping address" collapseKey="customer-shipping" defaultOpen={false}>
  <KeyValue items={[
    { key: 'Address', value: '27 Calle El Conde' },
    { key: 'City', value: 'Santo Domingo' },
  ]} />
</Collapsible>

<Collapsible label="Credit & terms" collapseKey="customer-credit">
  <KeyValue items={[
    { key: 'Credit limit', value: 'RD$ 250,000.00', mono: true },
    { key: 'Terms', value: 'NET-30' },
  ]} />
</Collapsible>
```

Pass a unique `collapseKey` per section per page so states don't collide. Sections start closed when `defaultOpen={false}` — good for detail/secondary data.
