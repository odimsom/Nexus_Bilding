# Matte & Precise Design System
> Version 2.0.0 - Complete Component Library

Este documento define las pautas de diseño universales "Matte & Precise" utilizadas en nuestras aplicaciones. Estas guías están pensadas para ser portables y aplicables a cualquier proyecto que requiera una estética moderna, limpia y suave (estilo Kindle/Papel).

---

## Tabla de Contenidos

1. [Filosofía del Sistema](#filosofía-del-sistema)
2. [Paleta de Colores](#paleta-de-colores)
3. [Tipografía](#tipografía)
4. [Espaciado y Grid](#espaciado-y-grid)
5. [Componentes](#componentes)
6. [Animaciones y Transiciones](#animaciones-y-transiciones)
7. [Tokens de Diseño](#tokens-de-diseño)
8. [Mejores Prácticas](#mejores-prácticas)

---

## Filosofía del Sistema

**Matte & Precise** busca recrear la experiencia de interactuar con materiales premium de alta calidad: papel mate, tinta precisa, y superficies táctiles. El sistema elimina elementos digitales gritones a favor de una estética reflexiva, calmada y profesional.

### Principios Fundamentales

1. **Suavidad Visual**: Evitar el blanco puro y los contrastes agresivos
2. **Claridad Funcional**: La información debe ser legible sin esfuerzo
3. **Materialidad Digital**: Simular texturas y profundidades sutiles
4. **Consistencia Armónica**: Todos los elementos deben sentirse parte del mismo universo

---

## Paleta de Colores

### Light Theme (Default)

La paleta se centra en tonos mate suaves para reducir la fatiga visual, manteniendo un alto contraste para la precisión de los datos.

#### Base Colors
```css
:root {
  /* Marca / Acción Principal */
  --primary: #8c92ab;           /* Azul acero mate suavizado */
  --primary-hover: #7a8099;     /* Variación para hovers */
  --primary-active: #686d87;    /* Estado presionado */
  --primary-light: #a8aec4;     /* Tinte suave para fondos */
  --primary-lighter: #d4d7e2;   /* Tinte muy suave */
  
  /* Superficies (Papel Mate) */
  --bg-base: #E6E4DF;          /* Fondo general (Canvas) */
  --bg-surface: #DEDCD6;       /* Tarjetas y contenedores */
  --bg-elevated: #E9E7E2;      /* Modales y elementos flotantes */
  --bg-input: #EDECEA;         /* Campos de formulario */
  --bg-hover: #D6D4CE;         /* Estado hover sobre superficies */
  
  /* Bordes y Líneas */
  --border-color: #C7C5C0;     /* Líneas de separación sutiles */
  --border-dark: #B5B3AE;      /* Bordes más pronunciados */
  --border-light: #D9D7D2;     /* Bordes muy sutiles */
  
  /* Tipografía */
  --text-primary: #4A4A4A;     /* Negro suavizado (no #000) */
  --text-secondary: #727379;   /* Secundario para etiquetas/meta */
  --text-tertiary: #9A9AA0;    /* Texto deshabilitado o terciario */
  --text-inverse: #FFFFFF;     /* Texto sobre fondos oscuros */
  
  /* Sombras */
  --shadow-sm: 0 1px 2px rgba(74, 74, 74, 0.06);
  --shadow-md: 0 4px 6px rgba(74, 74, 74, 0.08);
  --shadow-lg: 0 10px 15px rgba(74, 74, 74, 0.10);
  --shadow-xl: 0 20px 25px rgba(74, 74, 74, 0.12);
}
```

#### Semantic Status Colors (Ink Series)
Colores semánticos diseñados para parecer "tinta sobre papel", evitando neones digitales.

```css
/* Success / Paid / Active / Confirmed */
--ink-success-bg: #D4D9D4;
--ink-success-text: #4A5A4A;
--ink-success-border: #B8C4B8;
--ink-success-hover: #C8CDC8;

/* Warning / Pending / Processing / Review */
--ink-warning-bg: #E0DDD4;
--ink-warning-text: #5A5444;
--ink-warning-border: #C9C4B8;
--ink-warning-hover: #D5D2C9;

/* Error / Overdue / Rejected / Critical */
--ink-error-bg: #DDD4D4;
--ink-error-text: #5A4444;
--ink-error-border: #C4B8B8;
--ink-error-hover: #D2C9C9;

/* Info / Notice / Help / Informational */
--ink-info-bg: #D4D9DD;
--ink-info-text: #44495A;
--ink-info-border: #B8BFC4;
--ink-info-hover: #C9CED2;

/* Neutral / Draft / Inactive */
--ink-neutral-bg: #DCDCDC;
--ink-neutral-text: #5A5A5A;
--ink-neutral-border: #C4C4C4;
--ink-neutral-hover: #D1D1D1;
```

### Dark Theme

Inspirado en papel oscuro premium y tinta clara. Mantiene la filosofía mate pero invierte la jerarquía lumínica.

```css
:root[data-theme="dark"] {
  /* Marca / Acción Principal */
  --primary: #A8AEC4;           /* Azul acero más claro */
  --primary-hover: #BEC3D6;     /* Más brillante en hover */
  --primary-active: #D4D7E2;    /* Estado presionado */
  --primary-light: #8c92ab;     /* Tinte para fondos */
  --primary-lighter: #686d87;   /* Tinte oscurecido */
  
  /* Superficies (Papel Oscuro Mate) */
  --bg-base: #1C1C1E;          /* Fondo general oscuro */
  --bg-surface: #2C2C2E;       /* Tarjetas y contenedores */
  --bg-elevated: #3A3A3C;      /* Modales y elementos flotantes */
  --bg-input: #252527;         /* Campos de formulario */
  --bg-hover: #363638;         /* Estado hover sobre superficies */
  
  /* Bordes y Líneas */
  --border-color: #48484A;     /* Líneas de separación sutiles */
  --border-dark: #5C5C5E;      /* Bordes más pronunciados */
  --border-light: #3A3A3C;     /* Bordes muy sutiles */
  
  /* Tipografía */
  --text-primary: #E8E8E8;     /* Blanco suavizado */
  --text-secondary: #A8A8AA;   /* Secundario para etiquetas */
  --text-tertiary: #72727A;    /* Texto deshabilitado */
  --text-inverse: #1C1C1E;     /* Texto sobre fondos claros */
  
  /* Sombras (más pronunciadas en dark) */
  --shadow-sm: 0 1px 3px rgba(0, 0, 0, 0.20);
  --shadow-md: 0 4px 8px rgba(0, 0, 0, 0.25);
  --shadow-lg: 0 10px 20px rgba(0, 0, 0, 0.30);
  --shadow-xl: 0 20px 30px rgba(0, 0, 0, 0.35);
  
  /* Semantic Colors (Dark Theme Adaptations) */
  --ink-success-bg: #2A3A2A;
  --ink-success-text: #A8D4A8;
  --ink-success-border: #4A5A4A;
  
  --ink-warning-bg: #3A362A;
  --ink-warning-text: #D4C8A8;
  --ink-warning-border: #5A5444;
  
  --ink-error-bg: #3A2A2A;
  --ink-error-text: #D4A8A8;
  --ink-error-border: #5A4444;
  
  --ink-info-bg: #2A2E3A;
  --ink-info-text: #A8BCD4;
  --ink-info-border: #44495A;
  
  --ink-neutral-bg: #2E2E2E;
  --ink-neutral-text: #B8B8B8;
  --ink-neutral-border: #4A4A4A;
}
```

---

## Tipografía

### Font Family Stack

```css
:root {
  --font-sans: 'Inter', -apple-system, BlinkMacSystemFont, 
               'Segoe UI', 'Roboto', 'Helvetica Neue', Arial, 
               sans-serif;
  
  --font-mono: 'JetBrains Mono', 'Fira Code', 'Consolas', 
               'Monaco', monospace;
  
  --font-display: 'Inter', system-ui, sans-serif;
}
```

### Type Scale

```css
/* Escala tipográfica basada en proporción áurea (1.25) */
:root {
  --text-xs: 0.75rem;      /* 12px - Labels, badges */
  --text-sm: 0.875rem;     /* 14px - Body text, inputs */
  --text-base: 1rem;       /* 16px - Default body */
  --text-lg: 1.125rem;     /* 18px - Card titles */
  --text-xl: 1.25rem;      /* 20px - Section headers */
  --text-2xl: 1.5rem;      /* 24px - Page titles */
  --text-3xl: 1.875rem;    /* 30px - Hero titles */
  --text-4xl: 2.25rem;     /* 36px - Display titles */
  
  /* Pesos de fuente */
  --font-normal: 400;
  --font-medium: 500;
  --font-semibold: 600;
  --font-bold: 700;
  
  /* Altura de línea */
  --leading-tight: 1.25;
  --leading-snug: 1.375;
  --leading-normal: 1.5;
  --leading-relaxed: 1.625;
  --leading-loose: 2;
  
  /* Espaciado de letras */
  --tracking-tighter: -0.05em;
  --tracking-tight: -0.025em;
  --tracking-normal: 0;
  --tracking-wide: 0.025em;
  --tracking-wider: 0.05em;
  --tracking-widest: 0.1em;
}
```

### Jerarquía de Texto

```css
/* Clases de utilidad para consistencia */
.heading-1 {
  font-size: var(--text-4xl);
  font-weight: var(--font-bold);
  line-height: var(--leading-tight);
  color: var(--text-primary);
  letter-spacing: var(--tracking-tight);
}

.heading-2 {
  font-size: var(--text-3xl);
  font-weight: var(--font-bold);
  line-height: var(--leading-tight);
  color: var(--text-primary);
}

.heading-3 {
  font-size: var(--text-2xl);
  font-weight: var(--font-semibold);
  line-height: var(--leading-snug);
  color: var(--text-primary);
}

.heading-4 {
  font-size: var(--text-xl);
  font-weight: var(--font-semibold);
  line-height: var(--leading-snug);
  color: var(--text-primary);
}

.heading-5 {
  font-size: var(--text-lg);
  font-weight: var(--font-semibold);
  line-height: var(--leading-normal);
  color: var(--text-primary);
}

.body-large {
  font-size: var(--text-base);
  font-weight: var(--font-normal);
  line-height: var(--leading-relaxed);
  color: var(--text-primary);
}

.body {
  font-size: var(--text-sm);
  font-weight: var(--font-normal);
  line-height: var(--leading-normal);
  color: var(--text-primary);
}

.body-small {
  font-size: var(--text-xs);
  font-weight: var(--font-normal);
  line-height: var(--leading-normal);
  color: var(--text-secondary);
}

.label {
  font-size: var(--text-xs);
  font-weight: var(--font-semibold);
  line-height: var(--leading-normal);
  color: var(--text-secondary);
  text-transform: uppercase;
  letter-spacing: var(--tracking-wider);
}

.caption {
  font-size: var(--text-xs);
  font-weight: var(--font-normal);
  line-height: var(--leading-normal);
  color: var(--text-tertiary);
}
```

---

## Espaciado y Grid

### Sistema de Espaciado (Base 4px)

```css
:root {
  --space-0: 0;
  --space-1: 0.25rem;   /* 4px */
  --space-2: 0.5rem;    /* 8px */
  --space-3: 0.75rem;   /* 12px */
  --space-4: 1rem;      /* 16px */
  --space-5: 1.25rem;   /* 20px */
  --space-6: 1.5rem;    /* 24px */
  --space-8: 2rem;      /* 32px */
  --space-10: 2.5rem;   /* 40px */
  --space-12: 3rem;     /* 48px */
  --space-16: 4rem;     /* 64px */
  --space-20: 5rem;     /* 80px */
  --space-24: 6rem;     /* 96px */
}
```

### Border Radius

```css
:root {
  --radius-none: 0;
  --radius-sm: 0.25rem;    /* 4px - Badges, tags */
  --radius-base: 0.5rem;   /* 8px - Buttons, inputs */
  --radius-lg: 0.75rem;    /* 12px - Cards */
  --radius-xl: 1rem;       /* 16px - Modals */
  --radius-2xl: 1.5rem;    /* 24px - Hero sections */
  --radius-full: 9999px;   /* Circular */
}
```

### Grid System

```css
/* Container máximos */
:root {
  --container-sm: 640px;
  --container-md: 768px;
  --container-lg: 1024px;
  --container-xl: 1280px;
  --container-2xl: 1536px;
}

.container {
  width: 100%;
  max-width: var(--container-xl);
  margin-left: auto;
  margin-right: auto;
  padding-left: var(--space-4);
  padding-right: var(--space-4);
}

/* Grid de 12 columnas */
.grid {
  display: grid;
  gap: var(--space-6);
}

.grid-cols-1 { grid-template-columns: repeat(1, minmax(0, 1fr)); }
.grid-cols-2 { grid-template-columns: repeat(2, minmax(0, 1fr)); }
.grid-cols-3 { grid-template-columns: repeat(3, minmax(0, 1fr)); }
.grid-cols-4 { grid-template-columns: repeat(4, minmax(0, 1fr)); }
.grid-cols-12 { grid-template-columns: repeat(12, minmax(0, 1fr)); }
```

---

## Componentes

### 1. Card

Contenedor fundamental para agrupar información relacionada.

#### Variantes

**Card Base**
```css
.card {
  background: var(--bg-surface);
  border: 1px solid var(--border-color);
  border-radius: var(--radius-lg);
  box-shadow: var(--shadow-sm);
  padding: var(--space-6);
  transition: all 0.2s ease;
}

.card:hover {
  box-shadow: var(--shadow-md);
  border-color: var(--border-dark);
}
```

**Card Elevated** (para modales y elementos destacados)
```css
.card-elevated {
  background: var(--bg-elevated);
  border: 1px solid var(--border-color);
  border-radius: var(--radius-xl);
  box-shadow: var(--shadow-lg);
  padding: var(--space-8);
}
```

**Card Interactive** (clickeable)
```css
.card-interactive {
  background: var(--bg-surface);
  border: 1px solid var(--border-color);
  border-radius: var(--radius-lg);
  box-shadow: var(--shadow-sm);
  padding: var(--space-6);
  cursor: pointer;
  transition: all 0.2s ease;
}

.card-interactive:hover {
  background: var(--bg-hover);
  box-shadow: var(--shadow-md);
  transform: translateY(-2px);
}

.card-interactive:active {
  transform: translateY(0);
  box-shadow: var(--shadow-sm);
}
```

---

### 2. Buttons

Sistema completo de botones para todas las acciones.

#### Primary Button
```css
.btn-primary {
  background: var(--primary);
  color: var(--text-inverse);
  border: none;
  border-radius: var(--radius-base);
  padding: var(--space-3) var(--space-6);
  font-size: var(--text-sm);
  font-weight: var(--font-semibold);
  cursor: pointer;
  transition: all 0.2s ease;
  box-shadow: var(--shadow-sm);
}

.btn-primary:hover {
  background: var(--primary-hover);
  box-shadow: var(--shadow-md);
  transform: translateY(-1px);
}

.btn-primary:active {
  background: var(--primary-active);
  transform: translateY(0);
  box-shadow: var(--shadow-sm);
}

.btn-primary:disabled {
  opacity: 0.5;
  cursor: not-allowed;
  transform: none;
}
```

#### Secondary Button
```css
.btn-secondary {
  background: transparent;
  color: var(--text-primary);
  border: 1px solid var(--border-color);
  border-radius: var(--radius-base);
  padding: var(--space-3) var(--space-6);
  font-size: var(--text-sm);
  font-weight: var(--font-semibold);
  cursor: pointer;
  transition: all 0.2s ease;
}

.btn-secondary:hover {
  background: var(--bg-hover);
  border-color: var(--border-dark);
  box-shadow: var(--shadow-sm);
}

.btn-secondary:active {
  background: var(--bg-surface);
}
```

#### Ghost Button
```css
.btn-ghost {
  background: transparent;
  color: var(--text-secondary);
  border: none;
  border-radius: var(--radius-base);
  padding: var(--space-3) var(--space-6);
  font-size: var(--text-sm);
  font-weight: var(--font-medium);
  cursor: pointer;
  transition: all 0.2s ease;
}

.btn-ghost:hover {
  background: var(--bg-hover);
  color: var(--text-primary);
}
```

#### Button Sizes
```css
.btn-sm {
  padding: var(--space-2) var(--space-4);
  font-size: var(--text-xs);
}

.btn-lg {
  padding: var(--space-4) var(--space-8);
  font-size: var(--text-base);
}

.btn-icon {
  padding: var(--space-2);
  aspect-ratio: 1;
  display: inline-flex;
  align-items: center;
  justify-content: center;
}
```

---

### 3. Inputs & Forms

Campos de entrada coherentes y accesibles.

#### Input Base
```css
.input {
  background: var(--bg-input);
  border: 1px solid var(--border-color);
  border-radius: var(--radius-base);
  color: var(--text-primary);
  font-size: var(--text-sm);
  padding: var(--space-3) var(--space-4);
  transition: all 0.2s ease;
  width: 100%;
}

.input::placeholder {
  color: var(--text-tertiary);
}

.input:hover {
  border-color: var(--border-dark);
}

.input:focus {
  outline: none;
  border-color: var(--primary);
  box-shadow: 0 0 0 3px var(--primary-lighter);
}

.input:disabled {
  background: var(--bg-surface);
  color: var(--text-tertiary);
  cursor: not-allowed;
}

.input.error {
  border-color: var(--ink-error-border);
}

.input.error:focus {
  box-shadow: 0 0 0 3px var(--ink-error-bg);
}
```

#### Textarea
```css
.textarea {
  background: var(--bg-input);
  border: 1px solid var(--border-color);
  border-radius: var(--radius-base);
  color: var(--text-primary);
  font-size: var(--text-sm);
  font-family: var(--font-sans);
  padding: var(--space-3) var(--space-4);
  transition: all 0.2s ease;
  width: 100%;
  min-height: 120px;
  resize: vertical;
}

.textarea:focus {
  outline: none;
  border-color: var(--primary);
  box-shadow: 0 0 0 3px var(--primary-lighter);
}
```

#### Select
```css
.select {
  background: var(--bg-input);
  border: 1px solid var(--border-color);
  border-radius: var(--radius-base);
  color: var(--text-primary);
  font-size: var(--text-sm);
  padding: var(--space-3) var(--space-4);
  padding-right: var(--space-10);
  transition: all 0.2s ease;
  width: 100%;
  cursor: pointer;
  appearance: none;
  background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='12' viewBox='0 0 12 12'%3E%3Cpath fill='%234A4A4A' d='M6 9L1 4h10z'/%3E%3C/svg%3E");
  background-repeat: no-repeat;
  background-position: right var(--space-4) center;
}

.select:hover {
  border-color: var(--border-dark);
}

.select:focus {
  outline: none;
  border-color: var(--primary);
  box-shadow: 0 0 0 3px var(--primary-lighter);
}
```

#### Checkbox & Radio
```css
.checkbox,
.radio {
  appearance: none;
  width: 20px;
  height: 20px;
  border: 2px solid var(--border-color);
  background: var(--bg-input);
  cursor: pointer;
  transition: all 0.2s ease;
  position: relative;
}

.checkbox {
  border-radius: var(--radius-sm);
}

.radio {
  border-radius: var(--radius-full);
}

.checkbox:hover,
.radio:hover {
  border-color: var(--primary);
}

.checkbox:checked,
.radio:checked {
  background: var(--primary);
  border-color: var(--primary);
}

.checkbox:checked::after {
  content: '';
  position: absolute;
  left: 5px;
  top: 2px;
  width: 6px;
  height: 10px;
  border: solid white;
  border-width: 0 2px 2px 0;
  transform: rotate(45deg);
}

.radio:checked::after {
  content: '';
  position: absolute;
  left: 50%;
  top: 50%;
  width: 8px;
  height: 8px;
  background: white;
  border-radius: var(--radius-full);
  transform: translate(-50%, -50%);
}
```

#### Form Label
```css
.label {
  display: block;
  font-size: var(--text-xs);
  font-weight: var(--font-semibold);
  color: var(--text-secondary);
  text-transform: uppercase;
  letter-spacing: var(--tracking-wider);
  margin-bottom: var(--space-2);
}
```

#### Form Group
```css
.form-group {
  margin-bottom: var(--space-6);
}

.form-hint {
  display: block;
  font-size: var(--text-xs);
  color: var(--text-tertiary);
  margin-top: var(--space-2);
}

.form-error {
  display: block;
  font-size: var(--text-xs);
  color: var(--ink-error-text);
  margin-top: var(--space-2);
}
```

---

### 4. Badges & Tags

Indicadores visuales para estados y categorías.

```css
.badge {
  display: inline-flex;
  align-items: center;
  padding: var(--space-1) var(--space-3);
  border-radius: var(--radius-full);
  font-size: var(--text-xs);
  font-weight: var(--font-semibold);
  text-transform: uppercase;
  letter-spacing: var(--tracking-wide);
}

.badge-success {
  background: var(--ink-success-bg);
  color: var(--ink-success-text);
  border: 1px solid var(--ink-success-border);
}

.badge-warning {
  background: var(--ink-warning-bg);
  color: var(--ink-warning-text);
  border: 1px solid var(--ink-warning-border);
}

.badge-error {
  background: var(--ink-error-bg);
  color: var(--ink-error-text);
  border: 1px solid var(--ink-error-border);
}

.badge-info {
  background: var(--ink-info-bg);
  color: var(--ink-info-text);
  border: 1px solid var(--ink-info-border);
}

.badge-neutral {
  background: var(--ink-neutral-bg);
  color: var(--ink-neutral-text);
  border: 1px solid var(--ink-neutral-border);
}
```

---

### 5. Alerts & Notifications

Mensajes contextuales para comunicar información importante.

```css
.alert {
  border-radius: var(--radius-base);
  padding: var(--space-4);
  display: flex;
  gap: var(--space-3);
  border: 1px solid;
}

.alert-success {
  background: var(--ink-success-bg);
  color: var(--ink-success-text);
  border-color: var(--ink-success-border);
}

.alert-warning {
  background: var(--ink-warning-bg);
  color: var(--ink-warning-text);
  border-color: var(--ink-warning-border);
}

.alert-error {
  background: var(--ink-error-bg);
  color: var(--ink-error-text);
  border-color: var(--ink-error-border);
}

.alert-info {
  background: var(--ink-info-bg);
  color: var(--ink-info-text);
  border-color: var(--ink-info-border);
}

.alert-title {
  font-weight: var(--font-semibold);
  margin-bottom: var(--space-1);
}

.alert-description {
  font-size: var(--text-sm);
  opacity: 0.9;
}
```

---

### 6. Modals & Dialogs

Ventanas emergentes para interacciones críticas.

```css
.modal-overlay {
  position: fixed;
  inset: 0;
  background: rgba(28, 28, 30, 0.5);
  backdrop-filter: blur(4px);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
  animation: fadeIn 0.2s ease;
}

.modal {
  background: var(--bg-elevated);
  border: 1px solid var(--border-color);
  border-radius: var(--radius-xl);
  box-shadow: var(--shadow-xl);
  max-width: 500px;
  width: 90%;
  max-height: 90vh;
  overflow: auto;
  animation: slideUp 0.3s ease;
}

.modal-header {
  padding: var(--space-6);
  border-bottom: 1px solid var(--border-color);
}

.modal-title {
  font-size: var(--text-xl);
  font-weight: var(--font-semibold);
  color: var(--text-primary);
}

.modal-body {
  padding: var(--space-6);
}

.modal-footer {
  padding: var(--space-6);
  border-top: 1px solid var(--border-color);
  display: flex;
  gap: var(--space-3);
  justify-content: flex-end;
}

.modal-footer button {
  padding: var(--space-2) var(--space-4);
  border-radius: var(--radius-base);
  border: 1px solid var(--border-color);
  font-size: var(--text-sm);
  font-weight: var(--font-semibold);
  color: var(--text-primary);
  background: var(--bg-input);
}

.modal-footer button:hover {
  background: var(--primary);
  color: var(--bg-input);
}

.modal-footer button:active {
  transform: translateY(2px);
}

@keyframes fadeIn {
  from { opacity: 0; }
  to { opacity: 1; }
}

@keyframes slideUp {
  from {
    opacity: 0;
    transform: translateY(20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

---

### 7. Tables

Tablas estructuradas para presentación de datos.

```css
.table-container {
  background: var(--bg-surface);
  border: 1px solid var(--border-color);
  border-radius: var(--radius-lg);
  overflow: hidden;
}

.table {
  width: 100%;
  border-collapse: collapse;
  font-size: var(--text-sm);
}

.table thead {
  background: var(--bg-hover);
  border-bottom: 1px solid var(--border-color);
}

.table th {
  padding: var(--space-4) var(--space-6);
  text-align: left;
  font-weight: var(--font-semibold);
  color: var(--text-secondary);
  text-transform: uppercase;
  font-size: var(--text-xs);
  letter-spacing: var(--tracking-wider);
}

.table td {
  padding: var(--space-4) var(--space-6);
  border-bottom: 1px solid var(--border-light);
  color: var(--text-primary);
}

.table tbody tr:hover {
  background: var(--bg-hover);
  transition: background 0.15s ease;
}

.table tbody tr:last-child td {
  border-bottom: none;
}

/* Table Row Interactive */
.table-row-interactive {
  cursor: pointer;
}

.table-row-interactive:active {
  background: var(--bg-surface);
}
```

---

### 8. Tabs

Sistema de navegación por pestañas.

```css
.tabs {
  display: flex;
  gap: var(--space-2);
  border-bottom: 1px solid var(--border-color);
  margin-bottom: var(--space-6);
}

.tab {
  background: transparent;
  border: none;
  padding: var(--space-3) var(--space-6);
  font-size: var(--text-sm);
  font-weight: var(--font-medium);
  color: var(--text-secondary);
  cursor: pointer;
  transition: all 0.2s ease;
  position: relative;
  border-radius: var(--radius-base) var(--radius-base) 0 0;
}

.tab:hover {
  color: var(--text-primary);
  background: var(--bg-hover);
}

.tab.active {
  color: var(--primary);
  font-weight: var(--font-semibold);
}

.tab.active::after {
  content: '';
  position: absolute;
  bottom: -1px;
  left: 0;
  right: 0;
  height: 2px;
  background: var(--primary);
}

.tab-content {
  animation: fadeIn 0.3s ease;
}
```

---

### 9. Tooltips

Ayudas contextuales al pasar el cursor.

```css
.tooltip-wrapper {
  position: relative;
  display: inline-block;
}

.tooltip {
  position: absolute;
  bottom: calc(100% + var(--space-2));
  left: 50%;
  transform: translateX(-50%);
  background: var(--text-primary);
  color: var(--text-inverse);
  padding: var(--space-2) var(--space-3);
  border-radius: var(--radius-base);
  font-size: var(--text-xs);
  white-space: nowrap;
  pointer-events: none;
  opacity: 0;
  transition: opacity 0.2s ease;
  z-index: 1000;
}

.tooltip::after {
  content: '';
  position: absolute;
  top: 100%;
  left: 50%;
  transform: translateX(-50%);
  border: 4px solid transparent;
  border-top-color: var(--text-primary);
}

.tooltip-wrapper:hover .tooltip {
  opacity: 1;
}
```

---

### 10. Dropdowns

Menús desplegables para acciones y navegación.

```css
.dropdown {
  position: relative;
  display: inline-block;
}

.dropdown-toggle {
  background: var(--bg-input);
  border: 1px solid var(--border-color);
  border-radius: var(--radius-base);
  padding: var(--space-3) var(--space-4);
  font-size: var(--text-sm);
  color: var(--text-primary);
  cursor: pointer;
  display: flex;
  align-items: center;
  gap: var(--space-2);
  transition: all 0.2s ease;
}

.dropdown-toggle:hover {
  border-color: var(--border-dark);
}

.dropdown-menu {
  position: absolute;
  top: calc(100% + var(--space-2));
  left: 0;
  background: var(--bg-elevated);
  border: 1px solid var(--border-color);
  border-radius: var(--radius-base);
  box-shadow: var(--shadow-lg);
  min-width: 200px;
  padding: var(--space-2);
  opacity: 0;
  pointer-events: none;
  transform: translateY(-8px);
  transition: all 0.2s ease;
  z-index: 1000;
}

.dropdown.open .dropdown-menu {
  opacity: 1;
  pointer-events: all;
  transform: translateY(0);
}

.dropdown-item {
  display: block;
  width: 100%;
  padding: var(--space-3) var(--space-4);
  border: none;
  background: transparent;
  border-radius: var(--radius-sm);
  font-size: var(--text-sm);
  color: var(--text-primary);
  text-align: left;
  cursor: pointer;
  transition: all 0.15s ease;
}

.dropdown-item:hover {
  background: var(--bg-hover);
}

.dropdown-divider {
  height: 1px;
  background: var(--border-color);
  margin: var(--space-2) 0;
}
```

---

### 11. Progress Bars

Indicadores de progreso y carga.

```css
.progress {
  width: 100%;
  height: 8px;
  background: var(--bg-hover);
  border-radius: var(--radius-full);
  overflow: hidden;
  position: relative;
}

.progress-bar {
  height: 100%;
  background: var(--primary);
  border-radius: var(--radius-full);
  transition: width 0.3s ease;
  position: relative;
  overflow: hidden;
}

.progress-bar::after {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: linear-gradient(
    90deg,
    transparent,
    rgba(255, 255, 255, 0.3),
    transparent
  );
  animation: shimmer 2s infinite;
}

@keyframes shimmer {
  0% { transform: translateX(-100%); }
  100% { transform: translateX(100%); }
}

/* Progress Sizes */
.progress-sm {
  height: 4px;
}

.progress-lg {
  height: 12px;
}

/* Progress Variants */
.progress-bar-success {
  background: var(--ink-success-text);
}

.progress-bar-warning {
  background: var(--ink-warning-text);
}

.progress-bar-error {
  background: var(--ink-error-text);
}
```

---

### 12. Loaders & Spinners

Indicadores de carga y estado de procesamiento.

```css
.spinner {
  width: 40px;
  height: 40px;
  border: 3px solid var(--border-color);
  border-top-color: var(--primary);
  border-radius: var(--radius-full);
  animation: spin 0.8s linear infinite;
}

@keyframes spin {
  to { transform: rotate(360deg); }
}

.spinner-sm {
  width: 20px;
  height: 20px;
  border-width: 2px;
}

.spinner-lg {
  width: 60px;
  height: 60px;
  border-width: 4px;
}

/* Dots Loader */
.dots-loader {
  display: flex;
  gap: var(--space-2);
}

.dot {
  width: 8px;
  height: 8px;
  background: var(--primary);
  border-radius: var(--radius-full);
  animation: bounce 1.4s infinite ease-in-out;
}

.dot:nth-child(1) {
  animation-delay: -0.32s;
}

.dot:nth-child(2) {
  animation-delay: -0.16s;
}

@keyframes bounce {
  0%, 80%, 100% {
    transform: scale(0);
    opacity: 0.5;
  }
  40% {
    transform: scale(1);
    opacity: 1;
  }
}

/* Skeleton Loader */
.skeleton {
  background: var(--bg-hover);
  border-radius: var(--radius-base);
  position: relative;
  overflow: hidden;
}

.skeleton::after {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: linear-gradient(
    90deg,
    transparent,
    var(--bg-surface),
    transparent
  );
  animation: shimmer 1.5s infinite;
}

.skeleton-text {
  height: 1em;
  margin-bottom: var(--space-2);
}

.skeleton-title {
  height: 1.5em;
  width: 60%;
  margin-bottom: var(--space-4);
}

.skeleton-avatar {
  width: 48px;
  height: 48px;
  border-radius: var(--radius-full);
}
```

---

### 13. Breadcrumbs

Navegación de ruta jerárquica.

```css
.breadcrumbs {
  display: flex;
  align-items: center;
  gap: var(--space-2);
  font-size: var(--text-sm);
  color: var(--text-secondary);
  padding: var(--space-4) 0;
}

.breadcrumb-item {
  display: flex;
  align-items: center;
  gap: var(--space-2);
}

.breadcrumb-link {
  color: var(--text-secondary);
  text-decoration: none;
  transition: color 0.2s ease;
}

.breadcrumb-link:hover {
  color: var(--primary);
}

.breadcrumb-item.active {
  color: var(--text-primary);
  font-weight: var(--font-medium);
}

.breadcrumb-separator {
  color: var(--text-tertiary);
  user-select: none;
}
```

---

### 14. Pagination

Navegación por páginas de contenido.

```css
.pagination {
  display: flex;
  align-items: center;
  gap: var(--space-2);
  justify-content: center;
  margin: var(--space-6) 0;
}

.page-item {
  background: transparent;
  border: 1px solid var(--border-color);
  border-radius: var(--radius-base);
  padding: var(--space-2) var(--space-4);
  font-size: var(--text-sm);
  color: var(--text-primary);
  cursor: pointer;
  transition: all 0.2s ease;
  min-width: 40px;
  text-align: center;
}

.page-item:hover {
  background: var(--bg-hover);
  border-color: var(--border-dark);
}

.page-item.active {
  background: var(--primary);
  border-color: var(--primary);
  color: var(--text-inverse);
  font-weight: var(--font-semibold);
}

.page-item:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.page-ellipsis {
  color: var(--text-tertiary);
  padding: var(--space-2) var(--space-4);
}
```

---

### 15. Avatar

Representación visual de usuarios o entidades.

```css
.avatar {
  width: 40px;
  height: 40px;
  border-radius: var(--radius-full);
  background: var(--bg-hover);
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: var(--font-semibold);
  color: var(--text-primary);
  overflow: hidden;
  position: relative;
}

.avatar img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.avatar-sm {
  width: 32px;
  height: 32px;
  font-size: var(--text-xs);
}

.avatar-lg {
  width: 56px;
  height: 56px;
  font-size: var(--text-lg);
}

.avatar-xl {
  width: 80px;
  height: 80px;
  font-size: var(--text-2xl);
}

/* Avatar Group */
.avatar-group {
  display: flex;
  align-items: center;
}

.avatar-group .avatar {
  margin-left: -12px;
  border: 2px solid var(--bg-base);
}

.avatar-group .avatar:first-child {
  margin-left: 0;
}

.avatar-group .avatar:hover {
  z-index: 10;
}
```

---

## Animaciones y Transiciones (NUEVO)

### Duraciones Estándar
- **Micro-interactions** (hover, focus): 150ms
- **Navegación de elementos** (botones, links): 200ms  
- **Cambios de estado** (cards, badges): 250ms
- **Transiciones de página**: 300ms
- **Modales y overlays**: 250-300ms

### Easing Functions
- **ease-out**: Para la mayoría de interacciones (default)
- **ease-in-out**: Para navegación entre páginas
- **cubic-bezier(0.16, 1, 0.3, 1)**: Para modales (efecto "spring" suave)

### Clases de Utilidad
```jsx
// Hover básico
className="transition-colors duration-200 ease-out"

// Hover con elevación
className="transition-all duration-200 ease-out hover:shadow-md hover:-translate-y-0.5"

// Botón con feedback
className="transition-all duration-200 ease-out hover:shadow-md active:scale-[0.98]"

// Card interactivo
className="transition-all duration-300 ease-out hover:shadow-md hover:-translate-y-0.5"
```

### Micro-animations
```jsx
// Page Transition
.animate-page-in {
  animation: pageIn 200ms ease-out forwards;
}

@keyframes pageIn {
  from { opacity: 0; transform: translateY(8px); }
  to { opacity: 1; transform: translateY(0); }
}
```


---

### 16. Elementos Globales (Inputs & Scrollbars)

Estilos base para elementos nativos del navegador.

```css
/* Custom Scrollbar */
::-webkit-scrollbar {
  width: 10px;
  height: 10px;
}

::-webkit-scrollbar-track {
  background: transparent;
}

::-webkit-scrollbar-thumb {
  background: var(--border-color);
  border: 3px solid transparent;
  background-clip: content-box;
  border-radius: var(--radius-full);
}

::-webkit-scrollbar-thumb:hover {
  background: var(--text-tertiary);
}

/* Custom Selection */
::selection {
  background: rgba(var(--primary-rgb), 0.1);
  color: var(--primary);
}

/* Native Inputs Override */
input[type="checkbox"],
input[type="radio"] {
  accent-color: var(--primary);
  cursor: pointer;
  width: 1rem;
  height: 1rem;
}

/* Select Styling */
select {
  appearance: none;
  background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 20 20'%3e%3cpath stroke='%236b7280' stroke-linecap='round' stroke-linejoin='round' stroke-width='1.5' d='M6 8l4 4 4-4'/%3e%3c/svg%3e");
  background-position: right 0.5rem center;
  background-repeat: no-repeat;
  background-size: 1.5em 1.5em;
  padding-right: 2.5rem;
}
```

---

## Tokens de Diseño

### Transiciones y Animaciones

```css
:root {
  /* Durations */
  --duration-instant: 0.1s;
  --duration-fast: 0.2s;
  --duration-base: 0.3s;
  --duration-slow: 0.5s;
  
  /* Easing */
  --ease-in: cubic-bezier(0.4, 0, 1, 1);
  --ease-out: cubic-bezier(0, 0, 0.2, 1);
  --ease-in-out: cubic-bezier(0.4, 0, 0.2, 1);
  --ease-bounce: cubic-bezier(0.68, -0.55, 0.265, 1.55);
}

/* Clases de utilidad para animaciones */
.transition-all {
  transition: all var(--duration-base) var(--ease-in-out);
}

.transition-colors {
  transition: background-color var(--duration-fast) var(--ease-in-out),
              border-color var(--duration-fast) var(--ease-in-out),
              color var(--duration-fast) var(--ease-in-out);
}

.transition-transform {
  transition: transform var(--duration-base) var(--ease-out);
}

.transition-opacity {
  transition: opacity var(--duration-base) var(--ease-in-out);
}
```

### Z-Index Scale

```css
:root {
  --z-base: 0;
  --z-dropdown: 1000;
  --z-sticky: 1020;
  --z-fixed: 1030;
  --z-modal-backdrop: 1040;
  --z-modal: 1050;
  --z-popover: 1060;
  --z-tooltip: 1070;
}
```

### Breakpoints

```css
:root {
  --breakpoint-sm: 640px;
  --breakpoint-md: 768px;
  --breakpoint-lg: 1024px;
  --breakpoint-xl: 1280px;
  --breakpoint-2xl: 1536px;
}

/* Media Queries */
@media (min-width: 640px) { /* sm */ }
@media (min-width: 768px) { /* md */ }
@media (min-width: 1024px) { /* lg */ }
@media (min-width: 1280px) { /* xl */ }
@media (min-width: 1536px) { /* 2xl */ }
```

---

## Mejores Prácticas

### 1. Accesibilidad

**Contraste de Color**
- Mantener un ratio mínimo de 4.5:1 para texto normal
- Ratio mínimo de 3:1 para texto grande (18px+)
- Usar las combinaciones predefinidas del sistema para garantizar accesibilidad

**Navegación por Teclado**
- Todos los elementos interactivos deben ser accesibles por teclado
- Implementar estados `:focus-visible` claros
- Mantener un orden lógico de tabulación

**Atributos ARIA**
```html
<!-- Ejemplos de uso correcto -->
<button aria-label="Cerrar modal">×</button>
<input aria-describedby="email-hint" />
<div role="alert" aria-live="polite">Mensaje guardado</div>
```

---

### 2. Performance

**Optimización de Animaciones**
- Preferir `transform` y `opacity` sobre propiedades que causan reflow
- Usar `will-change` con moderación
- Implementar `prefers-reduced-motion` para usuarios sensibles

```css
@media (prefers-reduced-motion: reduce) {
  *,
  *::before,
  *::after {
    animation-duration: 0.01ms !important;
    animation-iteration-count: 1 !important;
    transition-duration: 0.01ms !important;
  }
}
```

**Carga de Fuentes**
```css
@font-face {
  font-family: 'Inter';
  font-display: swap;
  src: url('/fonts/inter.woff2') format('woff2');
}
```

---

### 3. Responsive Design

**Mobile First**
- Diseñar primero para móvil, luego expandir
- Usar unidades flexibles (rem, em, %, vw/vh)
- Implementar breakpoints de manera progresiva

**Touch Targets**
- Mínimo 44×44px para elementos táctiles
- Espaciado adecuado entre elementos interactivos
- Estados hover/active claros en móvil

```css
@media (hover: hover) {
  .button:hover {
    /* Estilos hover solo en dispositivos con cursor */
  }
}

@media (hover: none) {
  .button:active {
    /* Estilos touch para dispositivos táctiles */
  }
}
```

---

### 4. Consistencia

**Nomenclatura**
- Usar BEM o convención consistente para clases
- Prefijos claros para variantes (btn-, card-, badge-)
- Evitar nombres genéricos que puedan causar conflictos

**Espaciado**
- Adherirse a la escala de espaciado base-4
- Usar variables CSS en lugar de valores hardcoded
- Mantener ritmo vertical consistente

**Componentes Reutilizables**
- Crear componentes atómicos y composables
- Documentar props y variantes
- Mantener una librería de componentes actualizada

---

### 5. Dark Mode

**Implementación**
```javascript
// Detectar preferencia del sistema
const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

// Toggle manual
function toggleTheme() {
  const root = document.documentElement;
  const currentTheme = root.getAttribute('data-theme');
  const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
  root.setAttribute('data-theme', newTheme);
  localStorage.setItem('theme', newTheme);
}

// Aplicar tema guardado
const savedTheme = localStorage.getItem('theme') || (prefersDark ? 'dark' : 'light');
document.documentElement.setAttribute('data-theme', savedTheme);
```

**Consideraciones**
- Probar todos los componentes en ambos temas
- Ajustar opacidades y sombras para dark mode
- Mantener legibilidad y contraste adecuados

---

### 6. Mantenimiento

**Versionado**
- Seguir Semantic Versioning (MAJOR.MINOR.PATCH)
- Documentar breaking changes claramente
- Mantener changelog actualizado

**Testing Visual**
- Implementar snapshot testing para componentes críticos
- Probar en múltiples navegadores y dispositivos
- Validar accesibilidad con herramientas automatizadas

**Documentación**
- Mantener ejemplos de uso actualizados
- Incluir casos edge y estados de error
- Proporcionar guías de migración entre versiones

---

## Recursos Adicionales

### Herramientas Recomendadas

- **Figma**: Para diseño y prototipado
- **Storybook**: Para documentación de componentes
- **Tailwind CSS**: Para desarrollo rápido (si es compatible)
- **PostCSS**: Para procesamiento avanzado de CSS

### Librerías Complementarias

```json
{
  "dependencies": {
    "clsx": "^2.0.0",
    "framer-motion": "^10.0.0",
    "react-aria": "^3.0.0"
  }
}
```

### Checklist de Implementación

- [ ] Variables CSS configuradas
- [ ] Tipografía cargada correctamente
- [ ] Paleta de colores implementada
- [ ] Componentes base creados
- [ ] Dark mode funcional
- [ ] Responsive design verificado
- [ ] Accesibilidad validada
- [ ] Performance optimizada
- [ ] Documentación completada

---

## Changelog

### Version 2.0.0 (2024)
- Sistema de diseño completo rediseñado
- Nuevos tokens de color "Ink Series"
- Dark mode nativo
- Componentes expandidos (15 componentes base)
- Mejoras de accesibilidad
- Documentación completa

### Version 1.0.0 (2023)
- Lanzamiento inicial
- Componentes básicos
- Sistema de colores Light theme

---

## Licencia

Este sistema de diseño es de uso interno. Para uso externo, contactar al equipo de diseño.

---

## Contacto y Soporte

Para preguntas, sugerencias o reportar issues:
- Email: design-system@tuempresa.com
- Slack: #design-system
- GitHub: github.com/tuempresa/design-system

---

**Última actualización**: Enero 2025  
**Mantenedores**: Equipo de Design System  
**Status**: Activo




