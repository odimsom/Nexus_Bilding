using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ZoneConfiguration : IEntityTypeConfiguration<Zone>
{
    public void Configure(EntityTypeBuilder<Zone> builder)
    {
        builder.ToTable("zone", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.BinTypeCode).HasColumnName("bin_type_code");
        builder.Property(x => x.WarehouseClassCode).HasColumnName("warehouse_class_code");
        builder.Property(x => x.SpecialEquipmentCode).HasColumnName("special_equipment_code");
        builder.Property(x => x.ZoneRanking).HasColumnName("zone_ranking");
        builder.Property(x => x.CrossDockBinZone).HasColumnName("cross_dock_bin_zone");
    }
}
