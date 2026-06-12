#!/bin/bash
# Ejecutar LOCALMENTE — build aquí, sube imagen al servidor
set -e

SERVER="synset_server@server.synsetsolutions.com"
SERVER_PASS="1011"
CONTAINER="nexus-billing-api"
IMAGE="nexus-billing:latest"
DB_CONN="Host=synset-postgres;Port=5432;Database=nexus_db;Username=nexus_user;Password=NexusBilling2026!;"
TENANT_ID="aaaaaaaa-0000-0000-0000-000000000001"

echo "=== Nexus Billing Deploy $(date) ==="

# 1. Build frontend Angular
echo "--- [1/4] Build frontend..."
cd "$(dirname "$0")/src/Presentation/NexusBilling.Web"
npm run build -- --configuration=production --output-path=/tmp/nexus-web-dist 2>&1 | tail -5
cd "$(dirname "$0")"

# 2. Publish .NET API
echo "--- [2/4] Publish API..."
dotnet publish src/Presentation/NexusBilling.Api/NexusBilling.Api.csproj \
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
     --restart unless-stopped \
     $IMAGE && \
   echo '=== Container started ===' && \
   docker ps | grep $CONTAINER"

# Seed no-series (idempotente)
echo "--- Seed no-series..."
sshpass -p "$SERVER_PASS" ssh -o StrictHostKeyChecking=no "$SERVER" \
  "docker exec synset-postgres psql -U nexus_user -d nexus_db" <<SQL 2>/dev/null || true
INSERT INTO administration.no_series ("Id", tenant_id, code, description, default_nos, manual_nos, date_order, "CreatedAt", "UpdatedAt")
VALUES
  (gen_random_uuid(), '$TENANT_ID', 'CUST', 'Clientes',            true, false, false, NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'ITEM', 'Artículos',           true, false, false, NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'PV',   'Pedidos de Venta',    true, false, false, NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'COT',  'Cotizaciones',        true, false, false, NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'FAC',  'Facturas Directas',   true, false, false, NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'SI',   'Facturas Publicadas', true, false, false, NOW(), NOW())
ON CONFLICT DO NOTHING;

INSERT INTO administration.no_series_line ("Id", tenant_id, series_code, line_no, starting_no, ending_no, warning_no, increment_by_no, last_no_used, open, "CreatedAt", "UpdatedAt")
SELECT gen_random_uuid(), '$TENANT_ID', code, 10,
  CASE code
    WHEN 'CUST' THEN 'C-00001'   WHEN 'ITEM' THEN 'ART-00001'
    WHEN 'PV'   THEN 'PV-000001' WHEN 'COT'  THEN 'COT-000001'
    WHEN 'FAC'  THEN 'FAC-000001' WHEN 'SI'  THEN 'SI-000001'
  END,
  CASE code
    WHEN 'CUST' THEN 'C-99999'   WHEN 'ITEM' THEN 'ART-99999'
    WHEN 'PV'   THEN 'PV-999999' WHEN 'COT'  THEN 'COT-999999'
    WHEN 'FAC'  THEN 'FAC-999999' WHEN 'SI'  THEN 'SI-999999'
  END,
  '', 1, '', true, NOW(), NOW()
FROM administration.no_series
WHERE tenant_id = '$TENANT_ID' AND code IN ('CUST','ITEM','PV','COT','FAC','SI')
  AND NOT EXISTS (
    SELECT 1 FROM administration.no_series_line l
    WHERE l.tenant_id = '$TENANT_ID' AND l.series_code = no_series.code
  );
SQL

echo "=== Deploy completado ==="
