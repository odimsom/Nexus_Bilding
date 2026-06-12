#!/bin/bash
# Ejecutar EN el servidor de producción
set -e

CONTAINER="nexusbilling-api"
IMAGE="nexusbilling-api:latest"
PORT=5000
DB="Host=synset-postgres;Port=5432;Database=nexus_db;Username=nexus_user;Password=NexusBilling2026!;"

echo "=== Nexus Billing Deploy $(date) ==="

# 1. Pull código
if [ -d "/opt/Nexus_Bilding" ]; then
  cd /opt/Nexus_Bilding && git pull origin main
else
  git clone https://github.com/odimsom/Nexus_Bilding.git /opt/Nexus_Bilding
  cd /opt/Nexus_Bilding
fi

# 2. Build imagen
dotnet publish src/Presentation/NexusBilling.Api/NexusBilling.Api.csproj \
  -c Release -o /tmp/nexus-publish --nologo -v q
docker build -t $IMAGE /tmp/nexus-publish/ -q

# 3. Migraciones
ConnectionStrings__DefaultConnection="$DB" \
  dotnet ef database update \
  --project src/Infrastructure/NexusBilling.Infrastructure.Persistence \
  --startup-project src/Presentation/NexusBilling.Api \
  --no-build 2>&1 || true

# 4. Seed grupos si no existen
PGPASSWORD=NexusBilling2026! psql -h localhost -U nexus_user -d nexus_db <<'SQL' 2>/dev/null || true
INSERT INTO security.user_group ("Id", tenant_id, code, name, default_profile_id, assign_to_all_new_users, "CreatedAt", "UpdatedAt")
VALUES
  (gen_random_uuid(), 'aaaaaaaa-0000-0000-0000-000000000001', 'ADMIN', 'Administradores', '', true, NOW(), NOW()),
  (gen_random_uuid(), 'aaaaaaaa-0000-0000-0000-000000000001', 'VENTAS', 'Ventas', '', false, NOW(), NOW()),
  (gen_random_uuid(), 'aaaaaaaa-0000-0000-0000-000000000001', 'INVENTARIO', 'Inventario', '', false, NOW(), NOW()),
  (gen_random_uuid(), 'aaaaaaaa-0000-0000-0000-000000000001', 'FINANZAS', 'Finanzas', '', false, NOW(), NOW()),
  (gen_random_uuid(), 'aaaaaaaa-0000-0000-0000-000000000001', 'COMPRAS', 'Compras', '', false, NOW(), NOW())
ON CONFLICT DO NOTHING;
SQL

# 5. Reemplazar container
docker stop $CONTAINER 2>/dev/null || true
docker rm $CONTAINER 2>/dev/null || true

docker run -d \
  --name $CONTAINER \
  --network synset-network \
  -p $PORT:8080 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e "ConnectionStrings__DefaultConnection=$DB" \
  --restart unless-stopped \
  $IMAGE

echo "=== Deploy OK: $CONTAINER en :$PORT ==="
docker ps | grep $CONTAINER
