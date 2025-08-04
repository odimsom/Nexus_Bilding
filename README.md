# Nexus Billing - Sistema de Facturación

Una aplicación web moderna de facturación desarrollada con Node.js, Express, Handlebars, SQLite y Bootstrap.

## 🚀 Características

- **Gestión de Clientes**: Crear, editar y eliminar información de clientes
- **Productos y Servicios**: Catálogo completo con precios y categorías
- **Facturación**: Generación de facturas profesionales con cálculos automáticos
- **Reportes**: Análisis de ventas y estadísticas financieras
- **Inventario**: Control de stock y alertas de productos
- **Interfaz Responsive**: Diseño adaptable con Bootstrap
- **Base de Datos Local**: SQLite para máximo rendimiento

## 🛠️ Tecnologías Utilizadas

- **Backend**: Node.js + Express.js
- **Frontend**: Handlebars (HBS) + Bootstrap 5
- **Base de Datos**: SQLite
- **Arquitectura**: Clean Architecture con capas separadas
- **Estilos**: CSS3 + Bootstrap

## 📁 Estructura del Proyecto

```
Nexus_Bilding/
├── README.md
├── package.json
├── src/
│   ├── Core/                    # Lógica de negocio principal
│   │   ├── Application/         # Casos de uso y servicios
│   │   └── Domain/             # Entidades y reglas de negocio
│   ├── Infrastructure/         # Capa de infraestructura
│   │   ├── Persistencia/       # Repositorios y acceso a datos
│   │   └── Share/              # Utilidades compartidas
│   └── Presentation/           # Capa de presentación
│       └── Web/
│           ├── Api/            # API REST endpoints
│           │   └── Controller/ # Controladores API
│           └── WEBApp/         # Aplicación web
│               ├── Controller/ # Controladores web
│               └── Views/      # Plantillas Handlebars
```

## 🔧 Instalación y Configuración

### Prerrequisitos

- Node.js (versión 16 o superior)
- npm o yarn
- Git

### Pasos de Instalación

1. **Clonar el repositorio**

   ```bash
   git clone <url-del-repositorio>
   cd Nexus_Bilding
   ```

2. **Instalar dependencias**

   ```bash
   npm install
   ```

3. **Configurar variables de entorno**

   ```bash
   cp .env.example .env
   # Editar .env con tus configuraciones
   ```

4. **Inicializar base de datos**

   ```bash
   npm run db:migrate
   npm run db:seed
   ```

5. **Ejecutar en modo desarrollo**

   ```bash
   npm run dev
   ```

6. **Abrir en navegador**
   ```
   http://localhost:3000
   ```

## 📦 Scripts Disponibles

```bash
npm run dev          # Ejecutar en modo desarrollo
npm run start        # Ejecutar en producción
npm run test         # Ejecutar pruebas
npm run build        # Construir para producción
npm run db:migrate   # Ejecutar migraciones
npm run db:seed      # Insertar datos de prueba
npm run lint         # Verificar código
npm run format       # Formatear código
```

## 🗄️ Base de Datos

El proyecto utiliza SQLite como base de datos local. Las tablas principales incluyen:

- **customers** - Información de clientes
- **products** - Catálogo de productos/servicios
- **invoices** - Facturas generadas
- **invoice_items** - Detalles de factura
- **payments** - Registro de pagos
- **categories** - Categorías de productos

## 🎨 Interfaz de Usuario

La aplicación cuenta con las siguientes secciones:

### Dashboard Principal

- Resumen de ventas del día/mes
- Gráficos de estadísticas
- Accesos rápidos

### Gestión de Clientes

- Lista de clientes con búsqueda
- Formularios de registro/edición
- Historial de facturación

### Catálogo de Productos

- Gestión de inventario
- Categorización
- Control de precios

### Facturación

- Creación de facturas
- Cálculo automático de impuestos
- Generación de PDF
- Envío por email

### Reportes

- Ventas por período
- Productos más vendidos
- Análisis de clientes

## 🔐 Características de Seguridad

- Validación de entrada de datos
- Sanitización de consultas SQL
- Manejo seguro de sesiones
- Protección contra ataques comunes

## 🤝 Contribución

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.

## 👥 Autores

- **Tu Nombre** - _Desarrollo inicial_ - [TuUsuario](https://github.com/tu-usuario)

## 🆘 Soporte

Si encuentras algún problema o tienes preguntas:

1. Revisa los [Issues existentes](https://github.com/tu-usuario/nexus-billing/issues)
2. Crea un nuevo Issue si es necesario
3. Consulta la documentación en `/docs`

## 🔄 Roadmap

- [ ] Integración con APIs de facturación electrónica
- [ ] Módulo de contabilidad
- [ ] App móvil
- [ ] Integración con sistemas de pago
- [ ] Backup automático en la nube
- [ ] Multi-empresa
- [ ] Módulo de CRM

---

**¿Te gusta el proyecto? ¡Dale una ⭐ en GitHub!**
