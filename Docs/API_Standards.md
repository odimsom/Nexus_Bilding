# Nexus Billing — Estándar de API REST

> El objetivo es que el frontend Angular siempre pueda leer las respuestas del backend de manera predecible, sin inspeccionar el código del servidor.

---

## 1. Envelope de respuesta

Toda respuesta de la API usa el mismo wrapper, sin excepciones:

```json
{
  "success": true,
  "data": { },
  "error": null,
  "meta": {
    "requestId": "3f7a1b2c",
    "timestamp": "2026-06-12T01:00:00Z"
  }
}
```

En error:
```json
{
  "success": false,
  "data": null,
  "error": {
    "code": "POSTING_PERIOD_CLOSED",
    "message": "El período 2026-05 está cerrado.",
    "details": []
  },
  "meta": {
    "requestId": "3f7a1b2c",
    "timestamp": "2026-06-12T01:00:00Z"
  }
}
```

### Campos del envelope

| Campo | Tipo | Descripción |
|---|---|---|
| `success` | bool | `true` si HTTP 2xx, `false` si error |
| `data` | T \| null | Payload de la respuesta |
| `error` | ErrorDto \| null | Presente solo cuando `success = false` |
| `error.code` | string | Código de error en SCREAMING_SNAKE_CASE |
| `error.message` | string | Mensaje legible para el usuario |
| `error.details` | FieldError[] | Errores de validación por campo |
| `meta.requestId` | string | ID único de la petición (para logs) |
| `meta.timestamp` | ISO 8601 | Hora UTC del servidor |

---

## 2. Respuestas paginadas

Cuando el endpoint devuelve una lista paginada, `data` es un objeto con esta forma:

```json
{
  "success": true,
  "data": {
    "items": [ ],
    "pagination": {
      "page": 1,
      "pageSize": 50,
      "totalItems": 320,
      "totalPages": 7
    }
  }
}
```

**Query params estándar:**
- `page` (default 1)
- `pageSize` (default 50, max 200)
- `sortBy` — nombre del campo en camelCase
- `sortDir` — `asc` | `desc`
- `search` — texto libre

---

## 3. HTTP Status Codes

| Código | Cuándo |
|---|---|
| `200 OK` | GET, PUT, PATCH exitosos |
| `201 Created` | POST que crea un recurso |
| `204 No Content` | DELETE exitoso |
| `400 Bad Request` | Validación fallida (datos inválidos) |
| `401 Unauthorized` | Token ausente o expirado |
| `403 Forbidden` | Token válido pero sin permiso |
| `404 Not Found` | Recurso no encontrado |
| `409 Conflict` | Conflicto de estado (ej. período cerrado) |
| `422 Unprocessable Entity` | Regla de negocio rechazada |
| `500 Internal Server Error` | Error inesperado del servidor |

> No usar `200` con `success: false`. El código HTTP debe reflejar el resultado.

---

## 4. Nomenclatura de rutas

```
/api/{version}/{module}/{resource}
/api/v1/sales/customers
/api/v1/sales/customers/{no}
/api/v1/sales/invoices
/api/v1/sales/invoices/{no}
/api/v1/sales/invoices/{no}/post        ← acciones: verbo al final
/api/v1/inventory/items
/api/v1/inventory/items/{no}
/api/v1/finance/gl-accounts
/api/v1/purchasing/vendors
/api/v1/security/users
/api/v1/ecf/documents
/api/v1/ecf/documents/{id}/send
```

Reglas:
- Siempre plural para colecciones (`customers`, no `customer`)
- Kebab-case para palabras compuestas (`gl-accounts`, `sales-orders`)
- Acciones de negocio como sub-ruta: `POST /invoices/{no}/post`, `POST /invoices/{no}/cancel`
- Sin verbos en la ruta base (no `/getCustomers`)

---

## 5. Campos de fecha y número

- Fechas: ISO 8601 en UTC — `"2026-06-12T00:00:00Z"`
- Montos: `decimal` / `number` con 2 decimales — `84120.00` (nunca string)
- IDs numéricos: `number`; IDs de texto (Nos ERP): `string`
- Booleanos: `true` / `false` (nunca `0` / `1` / `"Y"`)

---

## 6. Errores de validación (FieldError)

```json
{
  "success": false,
  "data": null,
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "La solicitud contiene errores de validación.",
    "details": [
      { "field": "postingDate", "message": "La fecha de contabilización es requerida." },
      { "field": "sellToCustomerNo", "message": "El cliente no existe." }
    ]
  }
}
```

---

## 7. Códigos de error de dominio (SCREAMING_SNAKE_CASE)

| Código | Significado |
|---|---|
| `VALIDATION_ERROR` | Datos inválidos en la petición |
| `NOT_FOUND` | Recurso no encontrado |
| `POSTING_PERIOD_CLOSED` | El período contable está cerrado |
| `CUSTOMER_BLOCKED` | El cliente está bloqueado |
| `ITEM_BLOCKED` | El artículo está bloqueado |
| `INSUFFICIENT_INVENTORY` | Stock insuficiente |
| `DOCUMENT_ALREADY_POSTED` | El documento ya fue contabilizado |
| `UNAUTHORIZED` | Sin autenticación |
| `FORBIDDEN` | Sin permiso para esta operación |
| `ECF_SIGN_FAILED` | Error al firmar el XML con el certificado |
| `ECF_DGII_REJECTED` | DGII rechazó el comprobante |
| `DUPLICATE_NCF` | NCF ya utilizado |

---

## 8. Implementación en .NET

### ApiResponse<T> (clase base)

```csharp
public record ApiResponse<T>(
    bool Success,
    T? Data,
    ApiError? Error,
    ApiMeta Meta
);

public record ApiError(string Code, string Message, IReadOnlyList<FieldError> Details);
public record FieldError(string Field, string Message);
public record ApiMeta(string RequestId, DateTimeOffset Timestamp);
```

### Uso en controladores

```csharp
// Éxito
return Ok(ApiResponse.Ok(data));

// Error de negocio
return UnprocessableEntity(ApiResponse.Fail("DOCUMENT_ALREADY_POSTED", "El documento ya fue contabilizado."));

// Error de validación con campos
return BadRequest(ApiResponse.ValidationError(modelState));
```

### Middleware de errores globales
Un `IMiddleware` captura cualquier excepción no manejada y devuelve el envelope estándar con `500` y `code: "INTERNAL_ERROR"` sin exponer el stack trace en producción.

---

## 9. Seguridad del endpoint

- JWT Bearer en header: `Authorization: Bearer <token>`
- Refresh token en cookie HttpOnly (ya implementado)
- Todos los endpoints requieren `[Authorize]` por defecto
- Los endpoints públicos se anotan explícitamente con `[AllowAnonymous]`
- Permisos granulares: `[RequirePermission("sales.invoices.post")]`
