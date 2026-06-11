# Plan de Implementación: NexusBilling - Núcleo de Dominio y Persistencia

Este documento detalla la estrategia para implementar el modelo de dominio basado en el esquema ERP 2026, respetando la arquitectura Onion, DDD y las reglas de configuración de persistencia solicitadas.

## 1. Fase de Dominio (NexusBilling.Core.Domain)

Para cada módulo (**Administration**, **Security**, **Inventory**), se crearán los siguientes componentes:

### A. Value Objects y Tipos Base
- Implementación de `TenantId` como un Value Object central (struct o class) para asegurar el tipado fuerte en toda la solución.
- Creación de una clase base `Entity` y `AggregateRoot` (si aplica) para manejar la identidad y eventos de dominio.

### B. Entidades y Agregados (Respetando el esquema SQL 2026)
- **Inventory:**
  - `Item`: Mapeará `erp.item`. Incluirá métodos como `UpdateDirectCost`, `Block`, `AssignVendor`.
  - `Location`: Mapeará `erp.location`.
- **Security:**
  - `User`: Mapeará `erp.user`. Métodos: `ChangePassword`, `AssignPermissionSet`.
  - `PermissionSet`: Mapeará `erp.permission_set`.
- **Administration:**
  - `Tenant`: Mapeará `erp.tenant`.

### C. Interfaces de Repositorio
- Definición de contratos `IItemRepository`, `IUserRepository`, etc., dentro de la capa de dominio.

### D. Errores de Dominio
- Implementación de un patrón de resultados o excepciones de dominio personalizadas por módulo para evitar el uso de excepciones genéricas.

---

## 2. Fase de Infraestructura (NexusBilling.Infrastructure.Persistence)

### A. DbContext Modular
- Creación de `NexusBillingDbContext`.
- Uso de `OnModelCreating` para aplicar todas las configuraciones de forma automática mediante `modelBuilder.ApplyConfigurationsFromAssembly`.

### B. Configuraciones de Entidad (EntityTypeConfiguration)
- Se creará un archivo por entidad en la carpeta del módulo correspondiente (ej. `Infrastructure/Persistence/Inventory/Configurations/ItemConfiguration.cs`).
- **Mapeo de Value Objects:** Uso de `builder.OwnsOne` para `TenantId` y otros objetos de valor complejos.
- **Mapeo de Tablas:** Los nombres de tabla se forzarán al esquema `erp` (ej. `builder.ToTable("item", "erp")`).

### C. Implementación de Repositorios
- Implementaciones concretas de las interfaces de dominio utilizando EF Core.

---

## 3. Estándares de Codificación
- **Sin Comentarios Excesivos:** Solo comentarios simples sobre el bloque si es estrictamente necesario.
- **Sin Lógica de Negocio en Persistencia:** Las configuraciones solo definen el mapeo; las reglas residen en las entidades.
- **Nomenclatura:** `NexusBilling.[Layer].[Module]`.

---

## Próximos Pasos (Tras aprobación)
1. Implementar la base de Dominio (`Entity`, `ValueObject`, `TenantId`).
2. Implementar las Entidades del módulo **Administration** (por ser el más básico).
3. Implementar las Configuraciones de Persistencia para **Administration**.
4. Continuar con **Security** e **Inventory**.
