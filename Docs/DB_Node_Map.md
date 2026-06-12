# Nexus Billing — Mapa de Nodos de la Base de Datos

> Referencia rápida de entidades clave y sus relaciones. Solo los nodos que importan para auditar, depurar o extender el sistema.
> Leyenda: `—<` uno a muchos · `>—<` muchos a muchos · `— —` FK directa

---

## 1. Raíz Multi-tenant

```
Tenant (TenantId)
  ├—< User
  │     ├── UserLogin
  │     ├── UserPersonalization
  │     ├── UserPreference
  │     └─< AccessControl —< PermissionSet —< Permission
  └—< CompanyInformation
```

---

## 2. Módulo Sales (Ventas)

```
Customer (No.)
  ├── CustomerPostingGroup
  ├── CustomerPriceGroup
  ├── PaymentTerms
  ├── PaymentMethod
  ├── ShipToAddress
  │
  ├—< SalesHeader (DocumentType, No.)          ← Documento vivo
  │     ├── SalesLine (LineNo.)
  │     │     └── Item (No.)
  │     ├── ShippingAgent
  │     └── SalespersonPurchaser
  │
  ├—< SalesInvoiceHeader (No.)                 ← Factura publicada/contabilizada
  │     └—< SalesInvoiceLine
  │           └── Item (No.)
  │
  ├—< SalesCrMemoHeader (No.)                  ← Nota de crédito
  │     └—< SalesCrMemoLine
  │
  ├—< CustLedgerEntry (EntryNo.)               ← Mayor de cliente
  │     └—< DetailedCustLedgEntry
  │
  └—< SalesShipmentHeader                      ← Despacho
        └—< SalesShipmentLine
```

**Campos críticos en SalesHeader / SalesInvoiceHeader para DGII:**
| Campo | Descripción |
|---|---|
| `No` | Número de documento interno |
| `ExternalDocumentNo` | NCF (ej. B0100000001) |
| `SellToCustomerNo` | Cliente vendedor |
| `BillToCustomerNo` | Cliente facturado |
| `PostingDate` | Fecha de contabilización |
| `Amount` | Monto sin ITBIS |
| `AmountIncludingVat` | Monto total con ITBIS |
| `VatRegistrationNo` | RNC del cliente |
| `DocumentType` | Invoice / Credit Memo / Quote |

---

## 3. Módulo Inventory (Inventario)

```
ItemCategory (Code)
  └—< Item (No.)
        ├── ItemUnitOfMeasure (ItemNo, Code)
        ├── ItemVariant (ItemNo, Code)
        ├── ItemVendor —> Vendor
        ├—< ItemLedgerEntry (EntryNo.)          ← Movimientos físicos
        │     └—< ValueEntry (EntryNo.)         ← Movimientos de valor / costo
        ├—< StockkeepingUnit (ItemNo, LocationCode, VariantCode)
        └── Location (Code)
              └—< BinContent —> Bin —> Zone
```

---

## 4. Módulo Finance (Finanzas / Mayor General)

```
GLAccount (No.)  ← Plan de cuentas
  ├── GLAccountCategory
  ├── GenBusinessPostingGroup
  ├── GenProductPostingGroup
  └—< GLEntry (EntryNo.)
        ├── GenJournalLine (origin)
        └── VatEntry (linked via GLEntryVatEntryLink)

GeneralLedgerSetup (singleton)
  ├── GeneralPostingSetup (GenBusPostingGroup x GenProdPostingGroup)
  ├── VatPostingSetup (VatBusPostingGroup x VatProdPostingGroup)
  └── AccountingPeriod (StartingDate)

CashFlowAccount
  └—< CashFlowWorksheetLine
```

---

## 5. Módulo Purchasing (Compras)

```
Vendor (No.)
  ├── VendorPostingGroup
  ├── PaymentTerms
  ├── VendorBankAccount
  │
  ├—< PurchaseHeader (DocumentType, No.)        ← Orden / factura de compra viva
  │     └—< PurchaseLine
  │           └── Item (No.)
  │
  ├—< PurchInvHeader (No.)                      ← Factura compra contabilizada
  │     └—< PurchInvLine
  │
  └—< VendorLedgerEntry (EntryNo.)
        └—< DetailedVendorLedgEntry
```

---

## 6. Módulo Security (Seguridad)

```
User (UserSecurityId)
  ├── UserLogin (UserName, AuthenticationEmail)
  ├── UserPersonalization (PageId)
  ├── ActiveSession (SessionId)
  │
  ├—< UserGroupMember —> UserGroup
  │     └—< UserGroupPermissionSet —> PermissionSet
  │           └—< Permission (ObjectType, ObjectId, RangeFrom, RangeTo)
  │
  └—< AccessControl (PermissionSetId, CompanyName)
```

---

## 7. ECF / DGII (Factura Electrónica)

> Entidades nuevas a agregar cuando se implemente el módulo ECF.

```
EcfCompanyConfig (TenantId)              ← Config por empresa
  ├── RNC
  ├── CertificatePath / Thumbprint
  ├── DgiiEnv (Test | Production)
  └── P12Password (encrypted)

EcfDocument (Id)
  ├── SalesInvoiceHeaderNo                ← FK a SalesInvoiceHeader
  ├── Ncf (B01…B14)
  ├── NcfType (e31 | e32 | e33 | e34)
  ├── XmlPayload (text)
  ├── SignedXml (text)
  ├── Status (Draft | Signed | Sent | Approved | Rejected)
  ├── DgiiTrackId
  ├── SentAt
  └── ResponseXml

EcfNcfSequence (TenantId, NcfType)
  ├── CurrentNumber
  └── MaxNumber
```

---

## 8. Flujo de datos: de factura a ECF

```
[SalesHeader — Open]
    → POST /api/sales/invoices/{no}/post
[SalesInvoiceHeader — Posted]
    → Trigger ECF pipeline
[EcfDocument — Draft]
    → Generar XML (dgii-ecf-generator)
[EcfDocument — Signed]
    → Firmar con certificado P12 (ViafirmaRD)
[EcfDocument — Sent]
    → Enviar a DGII API
[EcfDocument — Approved | Rejected]
    → Almacenar respuesta DGII
```

---

## 9. Índice de tablas por volumen esperado

| Tabla | Volumen esperado | Índices clave |
|---|---|---|
| `GLEntry` | Alto (millones) | `PostingDate`, `GLAccountNo`, `SourceNo` |
| `ItemLedgerEntry` | Alto | `ItemNo`, `PostingDate`, `EntryType` |
| `ValueEntry` | Alto | `ItemLedgerEntryNo`, `PostingDate` |
| `CustLedgerEntry` | Medio | `CustomerNo`, `Open`, `DueDate` |
| `SalesInvoiceHeader` | Medio | `SellToCustomerNo`, `PostingDate` |
| `EcfDocument` | Medio | `Status`, `SentAt`, `NcfType` |
| `DetailedCustLedgEntry` | Alto | `CustLedgerEntryNo`, `EntryType` |
| `User` | Bajo | `UserName` |
| `Permission` | Bajo | `RoleId`, `ObjectType` |
