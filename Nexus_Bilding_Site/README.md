# Nexus Billing Site

Frontend moderno de Nexus Billing desarrollado con React, TypeScript y Vite, proporcionando una interfaz de usuario intuitiva y responsiva para el sistema de facturación.

## 📋 Descripción

La aplicación web frontend de Nexus Billing permite a los usuarios gestionar clientes, productos, facturas e inventario a través de una interfaz moderna y fácil de usar. Construida con las últimas tecnologías de React para garantizar una experiencia de usuario fluida y reactiva.

## 🛠️ Stack Tecnológico

- **React 19** - Biblioteca de UI
- **TypeScript** - Tipado estático
- **Vite** - Build tool y dev server ultrarrápido
- **ESLint** - Linting y calidad de código

### Dependencias Principales

```json
{
  "react": "^19.2.0",
  "react-dom": "^19.2.0",
  "typescript": "~5.9.3",
  "vite": "^7.2.4"
}
```

## 🚀 Configuración y Ejecución

### Prerrequisitos

- Node.js 18+ 
- npm 9+ (o yarn/pnpm)

### Instalación

1. **Instalar dependencias**

```bash
cd Nexus_Bilding_Site
npm install
```

2. **Configurar variables de entorno**

Crear un archivo `.env` en la raíz del proyecto:

```env
VITE_API_URL=http://localhost:5001/api
VITE_API_TIMEOUT=10000
```

3. **Ejecutar en modo desarrollo**

```bash
npm run dev
```

La aplicación estará disponible en: `http://localhost:5173`

### Scripts Disponibles

```bash
# Modo desarrollo con HMR
npm run dev

# Compilar para producción
npm run build

# Previsualizar build de producción
npm run preview

# Ejecutar linter
npm run lint
```

## 📁 Estructura del Proyecto

```
Nexus_Bilding_Site/
├── public/                  # Archivos estáticos
│   └── vite.svg
├── src/
│   ├── assets/             # Recursos (imágenes, iconos)
│   ├── components/         # Componentes reutilizables
│   ├── pages/              # Páginas/vistas
│   ├── services/           # Servicios API
│   ├── hooks/              # Custom hooks
│   ├── utils/              # Utilidades
│   ├── types/              # Tipos TypeScript
│   ├── App.tsx             # Componente principal
│   ├── App.css             # Estilos principales
│   ├── main.tsx            # Punto de entrada
│   └── index.css           # Estilos globales
├── index.html              # HTML template
├── package.json            # Dependencias y scripts
├── tsconfig.json           # Configuración TypeScript
├── vite.config.ts          # Configuración Vite
└── eslint.config.js        # Configuración ESLint
```

## 🎨 Características de UI/UX

- **Diseño Responsivo**: Adaptable a móviles, tablets y desktop
- **Hot Module Replacement (HMR)**: Desarrollo rápido con recarga en caliente
- **TypeScript**: Tipado estático para prevenir errores
- **ESLint**: Código limpio y consistente
- **Fast Refresh**: Preserva el estado durante el desarrollo

## 🔌 Integración con API

La aplicación se conecta al backend a través de servicios REST:

```typescript
// Ejemplo de servicio API
const API_URL = import.meta.env.VITE_API_URL;

export const clientesService = {
  getAll: () => fetch(`${API_URL}/clientes`),
  getById: (id: string) => fetch(`${API_URL}/clientes/${id}`),
  create: (data: Cliente) => fetch(`${API_URL}/clientes`, {
    method: 'POST',
    body: JSON.stringify(data)
  }),
  // ...
};
```

## 📦 Build para Producción

### Compilar

```bash
npm run build
```

Esto generará los archivos optimizados en la carpeta `dist/`.

### Configuración de Build

El proyecto usa Vite para optimizaciones automáticas:
- Tree-shaking
- Code-splitting
- Minificación
- Asset optimization

### Despliegue

Los archivos de `dist/` pueden ser desplegados en cualquier servidor web estático:

**Netlify:**
```bash
npm run build
netlify deploy --prod --dir=dist
```

**Vercel:**
```bash
npm run build
vercel --prod
```

**Servidor tradicional:**
```bash
npm run build
# Copiar contenido de dist/ al servidor
```

## 🧪 Testing

Para agregar tests, se recomienda:

```bash
# Instalar dependencias de testing
npm install -D vitest @testing-library/react @testing-library/jest-dom
```

```typescript
// Ejemplo de test
import { render, screen } from '@testing-library/react';
import { describe, it, expect } from 'vitest';
import App from './App';

describe('App', () => {
  it('renders correctly', () => {
    render(<App />);
    expect(screen.getByText(/Nexus Billing/i)).toBeInTheDocument();
  });
});
```

## 🎯 Próximas Funcionalidades

- [ ] Autenticación de usuarios
- [ ] Dashboard con estadísticas
- [ ] Gestión de clientes
- [ ] Catálogo de productos
- [ ] Generación de facturas
- [ ] Reportes y gráficos
- [ ] Gestión de inventario
- [ ] Tema oscuro/claro
- [ ] Internacionalización (i18n)

## 🔧 Configuración de TypeScript

El proyecto usa configuración estricta de TypeScript:

```json
{
  "compilerOptions": {
    "strict": true,
    "noImplicitAny": true,
    "strictNullChecks": true,
    "target": "ES2020",
    "module": "ESNext"
  }
}
```

## 📚 Recursos Útiles

- [React Documentation](https://react.dev)
- [TypeScript Documentation](https://www.typescriptlang.org/docs)
- [Vite Documentation](https://vite.dev)
- [React TypeScript Cheatsheet](https://react-typescript-cheatsheet.netlify.app)

## 🤝 Convenciones de Desarrollo

### Componentes
- Usar PascalCase para nombres de componentes
- Preferir functional components con hooks
- Componentes pequeños y reutilizables

### Estilos
- CSS Modules o styled-components
- Mobile-first approach
- Variables CSS para temas

### Código
- Seguir guías de ESLint
- Formatear con Prettier (opcional)
- Commits descriptivos

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo [LICENSE](../LICENSE) en la raíz del proyecto.

## 👥 Autores

- **Francisco Daniel Castro Borrome** - [Odimsom](https://github.com/Odimsom)
- **Eva Nazareth Gonzalez Velasco** - [GGeva07](https://github.com/GGeva07)

---

[⬅️ Volver al proyecto principal](../README.md)
