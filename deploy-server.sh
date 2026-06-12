#!/bin/bash
# Ejecutar EN el servidor de producción
set -e

CONTAINER="nexus-billing-api"
IMAGE="nexus-billing:latest"
REPO_DIR="/opt/Nexus_Bilding"
DB_CONN="Host=synset-postgres;Port=5432;Database=nexus_db;Username=nexus_user;Password=NexusBilling2026!;"
TENANT_ID="aaaaaaaa-0000-0000-0000-000000000001"

echo "=== Nexus Billing Deploy $(date) ==="

# 1. Pull / clone código
if [ -d "$REPO_DIR" ]; then
  cd "$REPO_DIR" && git pull origin main
else
  git clone https://github.com/odimsom/Nexus_Bilding.git "$REPO_DIR"
  cd "$REPO_DIR"
fi

# 2. Build imagen
dotnet publish src/Presentation/NexusBilling.Api/NexusBilling.Api.csproj \
  -c Release -o /tmp/nexus-publish --nologo -v q

cat > /tmp/nexus-publish/Dockerfile <<'DOCKERFILE'
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "NexusBilling.Api.dll"]
DOCKERFILE

docker build -t $IMAGE /tmp/nexus-publish/ -q

# 3. Migraciones
ConnectionStrings__DefaultConnection="$DB_CONN" \
  dotnet ef database update \
  --project src/Infrastructure/NexusBilling.Infrastructure.Persistence \
  --startup-project src/Presentation/NexusBilling.Api \
  --no-build 2>&1 || true

# 4. Seed — grupos de usuarios y series de numeración
docker exec synset-postgres psql -U nexus_user -d nexus_db <<SQL 2>/dev/null || true
-- Grupos de usuarios
INSERT INTO security.user_group ("Id", tenant_id, code, name, default_profile_id, assign_to_all_new_users, "CreatedAt", "UpdatedAt")
VALUES
  (gen_random_uuid(), '$TENANT_ID', 'ADMIN',      'Administradores', '', true,  NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'VENTAS',     'Ventas',          '', false, NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'INVENTARIO', 'Inventario',      '', false, NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'FINANZAS',   'Finanzas',        '', false, NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'COMPRAS',    'Compras',         '', false, NOW(), NOW())
ON CONFLICT DO NOTHING;

-- Series de numeración
INSERT INTO administration.no_series ("Id", tenant_id, code, description, default_nos, manual_nos, date_order, "CreatedAt", "UpdatedAt")
VALUES
  (gen_random_uuid(), '$TENANT_ID', 'CUST', 'Clientes',           true, false, false, NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'ITEM', 'Artículos',          true, false, false, NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'PV',   'Pedidos de Venta',   true, false, false, NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'COT',  'Cotizaciones',       true, false, false, NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'FAC',  'Facturas Directas',  true, false, false, NOW(), NOW()),
  (gen_random_uuid(), '$TENANT_ID', 'SI',   'Facturas Publicadas', true, false, false, NOW(), NOW())
ON CONFLICT DO NOTHING;

-- Líneas de serie
INSERT INTO administration.no_series_line ("Id", tenant_id, series_code, line_no, starting_no, ending_no, warning_no, increment_by_no, last_no_used, open, "CreatedAt", "UpdatedAt")
SELECT gen_random_uuid(), '$TENANT_ID', code, 10,
  CASE code
    WHEN 'CUST' THEN 'C-00001'
    WHEN 'ITEM' THEN 'ART-00001'
    WHEN 'PV'   THEN 'PV-000001'
    WHEN 'COT'  THEN 'COT-000001'
    WHEN 'FAC'  THEN 'FAC-000001'
    WHEN 'SI'   THEN 'SI-000001'
  END,
  CASE code
    WHEN 'CUST' THEN 'C-99999'
    WHEN 'ITEM' THEN 'ART-99999'
    WHEN 'PV'   THEN 'PV-999999'
    WHEN 'COT'  THEN 'COT-999999'
    WHEN 'FAC'  THEN 'FAC-999999'
    WHEN 'SI'   THEN 'SI-999999'
  END,
  '', 1, '', true, NOW(), NOW()
FROM administration.no_series
WHERE tenant_id = '$TENANT_ID'
  AND code IN ('CUST','ITEM','PV','COT','FAC','SI')
  AND NOT EXISTS (
    SELECT 1 FROM administration.no_series_line l
    WHERE l.tenant_id = '$TENANT_ID' AND l.series_code = no_series.code
  );
SQL

# 5. Reemplazar container
docker stop $CONTAINER 2>/dev/null || true
docker rm   $CONTAINER 2>/dev/null || true

docker run -d \
  --name $CONTAINER \
  --network synset-db \
  --network synset-public \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e ASPNETCORE_URLS="http://+:8080" \
  -e "ConnectionStrings__DefaultConnection=$DB_CONN" \
  -e "Jwt__Key=NexusBilling_ProductionKey_2026_x7kQz9mP3nRvWsLd!" \
  -e "Jwt__Issuer=NexusBilling.Api" \
  -e "Jwt__Audience=NexusBilling.Web" \
  -e "Jwt__ExpiryMinutes=60" \
  -e "Cors__AllowedOrigins__0=https://nexus.server.synsetsolutions.com" \
  --restart unless-stopped \
  $IMAGE

# Alias para que nginx/otros servicios sigan encontrando el container por nombre
docker network connect synset-db     $CONTAINER --alias nexus-api 2>/dev/null || true
docker network connect synset-public $CONTAINER --alias nexus-api 2>/dev/null || true

echo "=== Deploy OK: $CONTAINER ==="
docker ps | grep $CONTAINER
