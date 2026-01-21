# Nexus Billing - Sistema de Facturación

Una aplicación web moderna de facturación desarrollada con .NET y React, diseñada para gestionar clientes, productos, facturación e inventario de manera eficiente y profesional.

## 📋 Descripción del Proyecto

Nexus Billing es una solución completa de facturación que combina una arquitectura backend robusta con una interfaz moderna y responsiva. El proyecto está organizado en dos componentes principales:

- **[Nexus_Bilding_API](./Nexus_Bilding_API)**: Backend desarrollado en .NET con Clean Architecture
- **[Nexus_Bilding_Site](./Nexus_Bilding_Site)**: Frontend desarrollado en React + TypeScript + Vite

## ✨ Características Principales

### Gestión de Clientes
- Crear, editar y eliminar información de clientes
- Base de datos completa con información de contacto y facturación

### Productos y Servicios
- Catálogo completo con precios y categorías
- Gestión de inventario integrada

### Sistema de Facturación
- Generación de facturas profesionales
- Cálculos automáticos de impuestos y totales
- Historial completo de transacciones

### Reportes y Análisis
- Análisis de ventas y estadísticas financieras
- Visualización de datos

### Control de Inventario
- Seguimiento de stock en tiempo real
- Alertas de productos con bajo inventario

## 🏗️ Arquitectura del Sistema

```
Nexus_Bilding/
├── Nexus_Bilding_API/       # Backend .NET (Clean Architecture)
│   ├── Core/                 # Domain y Application
│   ├── Infrastructure/       # Identity, Persistence, Shared
│   └── Presentation/         # API REST
└── Nexus_Bilding_Site/      # Frontend React + TypeScript
    ├── src/                  # Código fuente
    └── public/               # Recursos estáticos
```

## 🛠️ Stack Tecnológico

### Backend
- **.NET** - Framework principal
- **PostgreSQL** - Base de datos
- **Clean Architecture** - Patrón arquitectónico

### Frontend
- **React 19** - Biblioteca UI
- **TypeScript** - Lenguaje de programación
- **Vite** - Build tool y dev server

## 🚀 Inicio Rápido

### Prerrequisitos
- .NET SDK 6.0 o superior
- Node.js 18+ y npm
- PostgreSQL

### Instalación

1. **Clonar el repositorio**
```bash
git clone https://github.com/Odimsom/Nexus_Bilding.git
cd Nexus_Bilding
```

2. **Configurar el Backend**
```bash
cd Nexus_Bilding_API
# Ver README.md del API para instrucciones detalladas
```

3. **Configurar el Frontend**
```bash
cd Nexus_Bilding_Site
# Ver README.md del Site para instrucciones detalladas
```

Para instrucciones detalladas de cada componente, consulta los README individuales de cada módulo.

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.

## 👥 Autores

- **Francisco Daniel Castro Borrome** - _Desarrollo inicial_ - [Odimsom](https://github.com/Odimsom)
- **Eva Nazareth Gonzalez Velasco** - _Desarrollo inicial_ - [GGeva07](https://github.com/GGeva07)

---

**¿Te gusta el proyecto? ¡Dale una ⭐ en GitHub!**
