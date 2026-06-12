# Plan ECF / DGII — Facturación Electrónica República Dominicana

> Objetivo: permitir que cada empresa registrada en Nexus Billing emita comprobantes fiscales electrónicos (e-CF) sin que el software en sí esté certificado — la certificación es por empresa.

---

## 1. Marco legal y proceso de certificación

### ¿Qué se certifica?
**La empresa emisora**, no el software. Nexus Billing es el medio técnico; cada cliente obtiene su propia habilitación ante la DGII.

### Pasos por empresa (según instrucción del jefe de Francisco)

```
Paso 1 — Certificado digital P12
  └─ URL: https://ra.viafirma.do/ra/viafirmard/requestNewCertificate/
           profile/VIAFIRMA-PI-DGII%3BEE_VIAFIRMA_PI_DGII%3B1624624563565?typeProfile=P12
  └─ A nombre del representante de facturación electrónica de la empresa
  └─ Costo: ~RD$ 2,200 anuales
  └─ IMPORTANTE: validar primero con DGII si la empresa puede ser emisora
     antes de comprar el certificado

Paso 2 — Iniciar proceso de emisor electrónico
  └─ Portal: Oficina Virtual de la DGII (www.dgii.gov.do)
  └─ Sección: Comprobantes Fiscales → Emisor Electrónico

Paso 3 — Pruebas en portal DGII
  └─ La DGII habilita un portal de certificación donde se envían documentos de prueba
  └─ Nexus Billing automatiza este paso: genera los XML, los firma y los envía

Paso 4 — Habilitación en producción
  └─ Una vez aprobadas las pruebas, la empresa queda habilitada para emitir e-CF reales
```

---

## 2. Tipos de comprobantes (NCF electrónicos)

| Tipo | Código DGII | Descripción | Uso |
|---|---|---|---|
| e31 | B01 | Factura de crédito fiscal | Empresas con RNC |
| e32 | B02 | Factura de consumo | Personas físicas |
| e33 | B03 | Nota de débito | Ajuste al alza |
| e34 | B04 | Nota de crédito | Devoluciones / ajuste a la baja |
| e41 | B11 | Comprobante de compras | Para compras (proveedor) |
| e43 | B14 | Regímenes especiales | — |
| e44 | B15 | Gubernamental | — |

---

## 3. Módulo ECF en Nexus Billing

### 3.1 Configuración por empresa (EcfCompanyConfig)

El administrador llena un formulario en Nexus Billing → Configuración → Facturación Electrónica:

```
Empresa: [selección]
RNC: _______________
Nombre del representante: _______________
Entorno DGII: [ ] Pruebas  [ ] Producción
Certificado P12: [upload archivo]
Contraseña P12: [campo enmascarado]
NCF activos:
  B01 desde: ___ hasta: ___
  B02 desde: ___ hasta: ___
  B04 desde: ___ hasta: ___
```

Este formulario guarda en `EcfCompanyConfig` y `EcfNcfSequence`. El P12 se guarda cifrado en disco (no en la DB); solo el thumbprint y la ruta van a la DB.

### 3.2 Flujo de emisión automática

```
1. Contabilizar factura (POST /api/v1/sales/invoices/{no}/post)
2. Pipeline ECF se dispara automáticamente (MediatR handler)
3. Generar XML según tipo de NCF
4. Firmar XML con P12 de la empresa
5. Enviar a DGII API (ambiente test o producción)
6. Guardar respuesta en EcfDocument
7. Actualizar SalesInvoiceHeader con TrackId y Estado ECF
```

### 3.3 Estados del documento ECF

```
Draft → Signed → Sent → Approved
                       └→ Rejected → (corregir y reenviar)
```

---

## 4. Generador de XML — mejoras al repo existente

**Repo base:** `https://github.com/odimsom/dgii-ecf-generator`

### Mejoras planificadas

1. **Batch de XMLs**: aceptar un array de documentos, no solo uno, y generar múltiples archivos en un ZIP.

2. **Envío automático a DGII**: integrar el cliente HTTP de la DGII directamente en el pipeline (hoy es manual).

3. **Formulario de datos de empresa**: en lugar de hardcodear datos, leer de `EcfCompanyConfig` de Nexus Billing via API interna.

4. **Integración directa como servicio .NET**: envolver el script Python como un microservicio HTTP (FastAPI) que Nexus Billing consume, o reescribir la lógica en .NET directamente.

### Estructura propuesta del servicio ECF (Python / FastAPI)

```
POST /ecf/generate
  Body: { documents: [...], companyConfig: { rnc, p12Path, p12Password } }
  Response: { xmlFiles: [{ ncf, xml, signed }] }

POST /ecf/send
  Body: { signedXml, environment: "test" | "production" }
  Response: { trackId, status, dgiiResponse }

POST /ecf/batch
  Body: { documents: [...], companyConfig: {...}, send: true }
  Response: { results: [{ ncf, status, trackId }] }
```

---

## 5. Estructura del XML ECF (e31 — Factura de Crédito Fiscal)

Campos mínimos requeridos por la DGII:

```xml
<RFCE>
  <Encabezado>
    <Version>1.0</Version>
    <IdDoc>
      <TipoeCF>31</TipoeCF>          <!-- e31 = B01 -->
      <eNCF>E310000000001</eNCF>     <!-- Número del NCF electrónico -->
      <FechaVencimientoSecuencia>31-12-2026</FechaVencimientoSecuencia>
      <IndicadorMontoGravado>1</IndicadorMontoGravado>
      <TipoIngresos>01</TipoIngresos>
      <TipoPago>1</TipoPago>         <!-- 1=Contado, 2=Crédito -->
    </IdDoc>
    <Emisor>
      <RNCEmisor>101234567</RNCEmisor>
      <RazonSocialEmisor>Mi Empresa SRL</RazonSocialEmisor>
      <DireccionEmisor>Calle Principal #1</DireccionEmisor>
      <FechaEmision>12-06-2026</FechaEmision>
    </Emisor>
    <Comprador>
      <RNCComprador>132456789</RNCComprador>
      <RazonSocialComprador>Cliente SA</RazonSocialComprador>
    </Comprador>
    <Totales>
      <MontoGravadoTotal>84745.76</MontoGravadoTotal>
      <MontoITBIS1>15254.24</MontoITBIS1>     <!-- 18% -->
      <MontoTotal>100000.00</MontoTotal>
    </Totales>
  </Encabezado>
  <DetallesItems>
    <Item NumLinea="1">
      <NombreItem>Bloque de Concreto 6"</NombreItem>
      <CantidadItem>1000</CantidadItem>
      <UnidadMedida>UND</UnidadMedida>
      <PrecioUnitarioItem>84.75</PrecioUnitarioItem>
      <TablaSubcotizacion>18</TablaSubcotizacion>  <!-- % ITBIS -->
      <MontoItem>84745.76</MontoItem>
    </Item>
  </DetallesItems>
</RFCE>
```

---

## 6. Tabla de secuencias NCF

La DGII asigna rangos de NCF por tipo. Nexus Billing gestiona la secuencia por empresa:

```
EcfNcfSequence:
  TenantId | NcfType | CurrentNumber | MaxNumber | ExpirationDate
  ─────────────────────────────────────────────────────────────────
  T001     | B01     | 47            | 1000      | 2026-12-31
  T001     | B02     | 12            | 500       | 2026-12-31
  T001     | B04     | 3             | 200       | 2026-12-31
```

Al contabilizar una factura, se asigna el siguiente NCF disponible de forma atómica (con lock de fila para evitar duplicados).

---

## 7. Plan de implementación por sprint

| Sprint | Entrega |
|---|---|
| 1 | Modelo `EcfCompanyConfig` + `EcfDocument` + `EcfNcfSequence` en DB |
| 2 | Formulario de configuración ECF en Angular |
| 3 | Generador XML integrado (mejorar repo Python) |
| 4 | Firma digital con P12 en .NET (BouncyCastle o X509) |
| 5 | Envío a DGII en ambiente de pruebas |
| 6 | Pruebas de certificación con DGII (completar el proceso por empresa) |
| 7 | Producción — B01, B02, B04 |
| 8 | Monitoreo, alertas de NCF próximos a agotarse, renovación de certificado |

---

## 8. Notas de seguridad

- El archivo P12 **nunca** se sube a un repositorio Git. Se guarda en disco del servidor con permisos 600.
- La contraseña del P12 se cifra con AES-256 antes de guardar en DB (clave maestra en variables de entorno).
- Logs de ECF son auditables pero **no** guardan el XML firmado completo en logs (puede contener datos fiscales sensibles).
- La renovación anual del certificado genera una alerta automática 30 días antes de expirar.
