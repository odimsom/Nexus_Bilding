// templates/landing/ds-base.js
// Loads the Nexus Billing design system tokens + bundle from this template's
// relative position (../../ = project root).
// In a consuming project: change `base` to point at your _ds/<folder>.
(() => {
  const base = '../..';
  for (const p of ['styles.css']) {
    const l = document.createElement('link');
    l.rel = 'stylesheet'; l.href = base + '/' + p;
    document.head.appendChild(l);
  }
  // External deps (Lucide + ECharts) — used by the landing template
  for (const src of [
    'https://unpkg.com/lucide@0.460.0/dist/umd/lucide.min.js',
    'https://cdn.jsdelivr.net/npm/echarts@5.5.0/dist/echarts.min.js',
  ]) {
    const s = document.createElement('script'); s.src = src;
    document.head.appendChild(s);
  }
  const s = document.createElement('script');
  s.src = base + '/_ds_bundle.js';
  s.onerror = () => console.error('ds-base.js: failed to load ' + s.src);
  document.head.appendChild(s);
})();
