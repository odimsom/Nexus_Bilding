# Nexus Billing API

Backend de Nexus Billing desarrollado con .NET, implementando Clean Architecture para garantizar un código mantenible, escalable y testeable.

## 📋 Descripción

La API de Nexus Billing proporciona todos los servicios backend necesarios para el sistema de facturación, incluyendo gestión de clientes, productos, facturación, inventario y reportes. Está construida siguiendo los principios de Clean Architecture (Onion Architecture).

## 🏗️ Arquitectura

El proyecto sigue **Onion Architecture** con las siguientes capas:

```
nexus_bilding_API/
├── Core/
│   ├── Domain/                      # Entidades y reglas de negocio
│   │   └── nexus_bilding_api.core.domain.csproj
│   └── Application/                 # Lógica de aplicación y casos de uso
│       └── nexus_bilding_api.core.application.csproj
├── Infrastructure/
│   ├── Persistence/                 # Acceso a datos y repositorios
│   │   └── nexus_bilding_api.infrastructure.persistence.csproj
│   ├── Identity/                    # Autenticación y autorización
│   │   └── nexus_bilding_api.infrastructure.identity.csproj
│   └── Shared/                      # Servicios compartidos
│       └── nexus_bilding_api.infrastructure.shared.csproj
└── Presentation/
    └── API/                         # Controllers y endpoints REST
        └── nexus_bilding_api.presentation.api.csproj
```

### Capas y Responsabilidades

#### 🎯 Core - Domain
- Entidades del dominio
- Interfaces de repositorios
- Enumeraciones y constantes
- Excepciones del dominio
- **Sin dependencias externas**

#### 🎯 Core - Application
- DTOs (Data Transfer Objects)
- Interfaces de servicios
- Casos de uso / CQRS
- Validaciones de negocio
- **Depende solo de Domain**

#### 🔧 Infrastructure - Persistence
- DbContext de Entity Framework Core
- Implementación de repositorios
- Migraciones de base de datos
- Configuración de PostgreSQL

#### 🔧 Infrastructure - Identity
- Gestión de usuarios
- Autenticación JWT
- Autorización basada en roles
- Identity de ASP.NET Core

#### 🔧 Infrastructure - Shared
- Servicios de email
- Servicios de logging
- Servicios de archivos
- Otros servicios transversales

#### 🌐 Presentation - API
- Controllers REST
- Middleware
- Filtros y validaciones
- Configuración de Swagger
- Punto de entrada de la aplicación

## 🛠️ Tecnologías

- **.NET 6+** - Framework principal
- **ASP.NET Core** - Web API
- **Entity Framework Core** - ORM
- **PostgreSQL** - Base de datos
- **AutoMapper** - Mapeo de objetos
- **FluentValidation** - Validaciones
- **MediatR** - Patrón mediator (opcional)
- **JWT** - Autenticación
- **Swagger/OpenAPI** - Documentación de API

## 🚀 Configuración y Ejecución

### Prerrequisitos
- .NET SDK 6.0 o superior
- PostgreSQL 12+
- Visual Studio 2022 / VS Code / Rider

### Instalación

1. **Configurar la cadena de conexión**

Crear un archivo `appsettings.Development.json` en `nexus_bilding_api.presentation.api/`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=nexus_billing;Username=tu_usuario;Password=tu_password"
  }
}
```

2. **Restaurar paquetes**

```bash
cd nexus_bilding_API
dotnet restore
```

3. **Aplicar migraciones**

```bash
cd nexus_bilding_api.presentation.api
dotnet ef database update
```

4. **Ejecutar la API**

```bash
dotnet run
```

La API estará disponible en:
- HTTPS: `https://localhost:7001`
- HTTP: `http://localhost:5001`
- Swagger UI: `https://localhost:7001/swagger`

### Comandos Útiles

```bash
# Compilar el proyecto
dotnet build

# Ejecutar tests
dotnet test

# Crear nueva migración
dotnet ef migrations add NombreMigracion

# Revertir última migración
dotnet ef migrations remove

# Ver migraciones aplicadas
dotnet ef database update --verbose
```

## 📡 Endpoints Principales

### Autenticación
- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrar usuario
- `POST /api/auth/refresh` - Refrescar token

### Clientes
- `GET /api/clientes` - Listar clientes
- `GET /api/clientes/{id}` - Obtener cliente
- `POST /api/clientes` - Crear cliente
- `PUT /api/clientes/{id}` - Actualizar cliente
- `DELETE /api/clientes/{id}` - Eliminar cliente

### Productos
- `GET /api/productos` - Listar productos
- `GET /api/productos/{id}` - Obtener producto
- `POST /api/productos` - Crear producto
- `PUT /api/productos/{id}` - Actualizar producto
- `DELETE /api/productos/{id}` - Eliminar producto

### Facturas
- `GET /api/facturas` - Listar facturas
- `GET /api/facturas/{id}` - Obtener factura
- `POST /api/facturas` - Crear factura
- `PUT /api/facturas/{id}` - Actualizar factura
- `DELETE /api/facturas/{id}` - Anular factura
- `GET /api/facturas/{id}/pdf` - Descargar PDF

### Reportes
- `GET /api/reportes/ventas` - Reporte de ventas
- `GET /api/reportes/inventario` - Reporte de inventario
- `GET /api/reportes/clientes` - Reporte de clientes

## 🔒 Seguridad

- Autenticación JWT Bearer
- Autorización basada en roles
- Validación de datos de entrada
- Protección contra CSRF
- CORS configurado
- HTTPS enforced en producción

## 📝 Documentación de API

La documentación completa de la API está disponible mediante Swagger UI cuando la aplicación está en ejecución:

👉 [https://localhost:7001/swagger](https://localhost:7001/swagger)

## 🧪 Testing

```bash
# Ejecutar todos los tests
dotnet test

# Con cobertura
dotnet test /p:CollectCoverage=true
```

## 📦 Despliegue

### Publicar para producción

```bash
dotnet publish -c Release -o ./publish
```

### Docker (opcional)

```bash
docker build -t nexus-billing-api .
docker run -p 5000:80 nexus-billing-api
```

## 🤝 Contribución

Este proyecto sigue los principios SOLID y Clean Architecture. Al contribuir:

1. Mantén las dependencias unidireccionales (hacia adentro)
2. Domain no debe tener dependencias externas
3. Usa inyección de dependencias
4. Escribe tests unitarios
5. Sigue las convenciones de código existentes

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo [LICENSE](../LICENSE) en la raíz del proyecto.

## 👥 Autores

- **Francisco Daniel Castro Borrome** - [Odimsom](https://github.com/Odimsom)
- **Eva Nazareth Gonzalez Velasco** - [GGeva07](https://github.com/GGeva07)

---

[⬅️ Volver al proyecto principal](../README.md)
