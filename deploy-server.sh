#!/bin/bash
# Ejecutar LOCALMENTE — build aquí, sube imagen al servidor
set -e

SERVER="synset_server@server.synsetsolutions.com"
SERVER_PASS="1011"
CONTAINER="nexus-billing-api"
IMAGE="nexus-billing:latest"
DB_CONN="Host=synset-postgres;Port=5432;Database=nexus_db;Username=nexus_user;Password=NexusBilling2026!;"
TENANT_ID="aaaaaaaa-0000-0000-0000-000000000001"

SCRIPT_DIR="$(cd "$(dirname "$0")" && pwd)"
echo "=== Nexus Billing Deploy $(date) ==="

# 1. Build frontend Angular
echo "--- [1/4] Build frontend..."
cd "$SCRIPT_DIR/src/Presentation/NexusBilling.Web"
npm run build -- --configuration=production --output-path=/tmp/nexus-web-dist 2>&1 | tail -5
cd "$SCRIPT_DIR"

# 2. Publish .NET API
echo "--- [2/4] Publish API..."
dotnet publish "$SCRIPT_DIR/src/Presentation/NexusBilling.Api/NexusBilling.Api.csproj" \
  -c Release -o /tmp/nexus-publish --nologo -v q

# Copiar frontend al wwwroot del API
mkdir -p /tmp/nexus-publish/wwwroot
cp -r /tmp/nexus-web-dist/browser/. /tmp/nexus-publish/wwwroot/

# 3. Build imagen Docker
echo "--- [3/4] Build Docker image..."
cat > /tmp/nexus-publish/Dockerfile <<'DOCKERFILE'
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "NexusBilling.Api.dll"]
DOCKERFILE

docker build -t $IMAGE /tmp/nexus-publish/ -q

# 4. Transferir imagen al servidor y actualizar container
echo "--- [4/4] Transfer & deploy..."
docker save $IMAGE | gzip | sshpass -p "$SERVER_PASS" \
  ssh -o StrictHostKeyChecking=no "$SERVER" \
  "docker load && \
   docker stop $CONTAINER 2>/dev/null || true && \
   docker rm   $CONTAINER 2>/dev/null || true && \
   docker run -d \
     --name $CONTAINER \
     --network synset-db \
     --network synset-public \
     -e ASPNETCORE_ENVIRONMENT=Production \
     -e ASPNETCORE_URLS='http://+:8080' \
     -e 'ConnectionStrings__DefaultConnection=$DB_CONN' \
     -e 'Jwt__Key=NexusBilling_ProductionKey_2026_x7kQz9mP3nRvWsLd!' \
     -e 'Jwt__Issuer=NexusBilling.Api' \
     -e 'Jwt__Audience=NexusBilling.Web' \
     -e 'Jwt__ExpiryMinutes=60' \
     -e 'Cors__AllowedOrigins__0=https://nexus.server.synsetsolutions.com' \
     --label 'traefik.enable=true' \
     --label 'traefik.http.routers.nexus.entrypoints=websecure' \
     --label 'traefik.http.routers.nexus.rule=Host(\`nexus.server.synsetsolutions.com\`)' \
     --label 'traefik.http.routers.nexus.tls.certresolver=le' \
     --label 'traefik.http.services.nexus.loadbalancer.server.port=8080' \
     --restart unless-stopped \
     $IMAGE && \
   echo '=== Container started ===' && \
   docker ps | grep $CONTAINER"

# Columnas nuevas en purchase_header (idempotente)
echo "--- Schema patch purchase_header..."
sshpass -p "$SERVER_PASS" ssh -o StrictHostKeyChecking=no "$SERVER" \
  "docker exec -i synset-postgres psql -U nexus_user -d nexus_db" <<'SCHEMA_SQL' 2>/dev/null || true
ALTER TABLE purchasing.purchase_header ADD COLUMN IF NOT EXISTS status            varchar(50)    NOT NULL DEFAULT 'Open';
ALTER TABLE purchasing.purchase_header ADD COLUMN IF NOT EXISTS amount            numeric(18,2)  NOT NULL DEFAULT 0;
ALTER TABLE purchasing.purchase_header ADD COLUMN IF NOT EXISTS amount_including_vat numeric(18,2) NOT NULL DEFAULT 0;
ALTER TABLE purchasing.purchase_header ADD COLUMN IF NOT EXISTS currency_code     varchar(10)    NOT NULL DEFAULT '';
ALTER TABLE purchasing.purchase_header ADD COLUMN IF NOT EXISTS payment_terms_code varchar(20)   NOT NULL DEFAULT '';
ALTER TABLE purchasing.purchase_header ADD COLUMN IF NOT EXISTS external_document_no varchar(50)  NOT NULL DEFAULT '';
ALTER TABLE purchasing.purchase_header ADD COLUMN IF NOT EXISTS due_date          timestamp;
SCHEMA_SQL

# Seed no-series (idempotente)
echo "--- Seed no-series..."
sshpass -p "$SERVER_PASS" ssh -o StrictHostKeyChecking=no "$SERVER" \
  "docker exec -i synset-postgres psql -U nexus_user -d nexus_db" <<SQL 2>/dev/null || true
INSERT INTO administration.no_series ("Id", tenant_id, code, description, default_nos, manual_nos, date_order, "CreatedAt", "UpdatedAt")
SELECT gen_random_uuid(), '$TENANT_ID', v.code, v.description, true, false, false, NOW(), NOW()
FROM (VALUES
  ('CUST', 'Clientes'),
  ('ITEM', 'Artículos'),
  ('VEND', 'Proveedores'),
  ('ORD',  'Órdenes de Venta'),
  ('PV',   'Pedidos de Venta'),
  ('COT',  'Cotizaciones'),
  ('PC',   'Pedidos de Compra'),
  ('FAC',  'Facturas Directas'),
  ('SI',   'Facturas Publicadas')
) AS v(code, description)
WHERE NOT EXISTS (
  SELECT 1 FROM administration.no_series ns
  WHERE ns.tenant_id = '$TENANT_ID' AND ns.code = v.code
);

INSERT INTO administration.no_series_line ("Id", tenant_id, series_code, line_no, starting_no, ending_no, warning_no, increment_by_no, last_no_used, open, "CreatedAt", "UpdatedAt")
SELECT gen_random_uuid(), '$TENANT_ID', code, 10,
  CASE code
    WHEN 'CUST' THEN 'C-00001'   WHEN 'ITEM' THEN 'ART-00001'
    WHEN 'VEND' THEN 'V-00001'   WHEN 'PV'   THEN 'PV-000001'
    WHEN 'ORD'  THEN 'ORD-000001' WHEN 'PC'  THEN 'PC-000001'
    WHEN 'COT'  THEN 'COT-000001'
    WHEN 'FAC'  THEN 'FAC-000001' WHEN 'SI'  THEN 'SI-000001'
  END,
  CASE code
    WHEN 'CUST' THEN 'C-99999'   WHEN 'ITEM' THEN 'ART-99999'
    WHEN 'VEND' THEN 'V-99999'   WHEN 'PV'   THEN 'PV-999999'
    WHEN 'ORD'  THEN 'ORD-999999' WHEN 'PC'  THEN 'PC-999999'
    WHEN 'COT'  THEN 'COT-999999'
    WHEN 'FAC'  THEN 'FAC-999999' WHEN 'SI'  THEN 'SI-999999'
  END,
  '', 1, '', true, NOW(), NOW()
FROM administration.no_series
WHERE tenant_id = '$TENANT_ID' AND code IN ('CUST','ITEM','VEND','ORD','PV','COT','PC','FAC','SI')
  AND NOT EXISTS (
    SELECT 1 FROM administration.no_series_line l
    WHERE l.tenant_id = '$TENANT_ID' AND l.series_code = no_series.code
  );

-- Insertar Proveedores de prueba
INSERT INTO purchasing.vendor ("Id", tenant_id, no, name, address, city, contact, blocked, "CreatedAt", "UpdatedAt")
SELECT gen_random_uuid(), '$TENANT_ID', 'V-00001', 'Proveedora Mundial S.R.L.', 'Calle Principal 45', 'Santo Domingo', 'Juan Pérez', false, NOW(), NOW()
WHERE NOT EXISTS (SELECT 1 FROM purchasing.vendor WHERE tenant_id = '$TENANT_ID' AND no = 'V-00001');

INSERT INTO purchasing.vendor ("Id", tenant_id, no, name, address, city, contact, blocked, "CreatedAt", "UpdatedAt")
SELECT gen_random_uuid(), '$TENANT_ID', 'V-00002', 'Almacenes Central', 'Av. John F. Kennedy', 'Santiago', 'María López', false, NOW(), NOW()
WHERE NOT EXISTS (SELECT 1 FROM purchasing.vendor WHERE tenant_id = '$TENANT_ID' AND no = 'V-00002');

UPDATE administration.no_series_line SET last_no_used = 'V-00002' WHERE tenant_id = '$TENANT_ID' AND series_code = 'VEND';

-- Insertar Pedidos de Venta (PVs) de prueba si no existen
INSERT INTO sales.sales_header ("Id", tenant_id, "DocumentType", "No", "SellToCustomerNo", "BillToName", "PostingDate", "CreatedAt", "UpdatedAt", amount, amount_including_vat, currency_code, payment_terms_code, payment_method_code, salesperson_code, external_document_no, status, due_date, sell_to_customer_name)
SELECT gen_random_uuid(), '$TENANT_ID', 'Order', 'PV-000001', 'C-00001', 'Cliente Local', NOW(), NOW(), NOW(), 1500, 1770, 'DOP', 'CONTADO', 'EFECTIVO', 'VEND01', '', 'Open', NOW(), 'Cliente Local'
WHERE NOT EXISTS (SELECT 1 FROM sales.sales_header WHERE tenant_id = '$TENANT_ID' AND "No" = 'PV-000001');

INSERT INTO sales.sales_header ("Id", tenant_id, "DocumentType", "No", "SellToCustomerNo", "BillToName", "PostingDate", "CreatedAt", "UpdatedAt", amount, amount_including_vat, currency_code, payment_terms_code, payment_method_code, salesperson_code, external_document_no, status, due_date, sell_to_customer_name)
SELECT gen_random_uuid(), '$TENANT_ID', 'Order', 'PV-000002', 'C-00002', 'Empresa XYZ', NOW(), NOW(), NOW(), 4500, 5310, 'DOP', 'CREDITO', 'TRANSFERENCIA', 'VEND02', 'REF-99', 'Released', NOW(), 'Empresa XYZ'
WHERE NOT EXISTS (SELECT 1 FROM sales.sales_header WHERE tenant_id = '$TENANT_ID' AND "No" = 'PV-000002');

INSERT INTO sales.sales_line ("Id", tenant_id, document_type, sell_to_customer_no, document_no, line_no, type, no, location_code, posting_group, description, description2, unit_of_measure, quantity, outstanding_quantity, qty_to_invoice, qty_to_ship, unit_price, unit_cost_lcy, vat, line_discount, line_discount_amount, amount, amount_including_vat, "CreatedAt", "UpdatedAt")
SELECT gen_random_uuid(), '$TENANT_ID', 1, 'C-00001', 'PV-000001', 10000, 1, 'ART-001', '', '', 'Servicio de Consultoría', '', 'SRV', 1, 1, 1, 1, 1500, 0, 18, 0, 0, 1500, 1770, NOW(), NOW()
WHERE NOT EXISTS (SELECT 1 FROM sales.sales_line WHERE tenant_id = '$TENANT_ID' AND document_no = 'PV-000001');

INSERT INTO sales.sales_line ("Id", tenant_id, document_type, sell_to_customer_no, document_no, line_no, type, no, location_code, posting_group, description, description2, unit_of_measure, quantity, outstanding_quantity, qty_to_invoice, qty_to_ship, unit_price, unit_cost_lcy, vat, line_discount, line_discount_amount, amount, amount_including_vat, "CreatedAt", "UpdatedAt")
SELECT gen_random_uuid(), '$TENANT_ID', 1, 'C-00002', 'PV-000002', 10000, 1, 'ART-002', '', '', 'Licencia de Software', '', 'UND', 3, 3, 3, 3, 1500, 500, 18, 0, 0, 4500, 5310, NOW(), NOW()
WHERE NOT EXISTS (SELECT 1 FROM sales.sales_line WHERE tenant_id = '$TENANT_ID' AND document_no = 'PV-000002');

-- Actualizar correlativo
UPDATE administration.no_series_line SET last_no_used = 'PV-000002' WHERE tenant_id = '$TENANT_ID' AND series_code = 'PV';
SQL

echo "=== Deploy completado ==="
