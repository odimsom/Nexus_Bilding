using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class BinConfiguration : IEntityTypeConfiguration<Bin>
{
    public void Configure(EntityTypeBuilder<Bin> builder)
    {
        builder.ToTable("bin", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ZoneCode).HasColumnName("zone_code");
        builder.Property(x => x.BinTypeCode).HasColumnName("bin_type_code");
        builder.Property(x => x.WarehouseClassCode).HasColumnName("warehouse_class_code");
        builder.Property(x => x.BlockMovement).HasColumnName("block_movement");
        builder.Property(x => x.SpecialEquipmentCode).HasColumnName("special_equipment_code");
        builder.Property(x => x.BinRanking).HasColumnName("bin_ranking");
        builder.Property(x => x.MaximumCubage).HasColumnName("maximum_cubage").HasPrecision(18, 5);
        builder.Property(x => x.MaximumWeight).HasColumnName("maximum_weight").HasPrecision(18, 5);
        builder.Property(x => x.Empty).HasColumnName("empty");
        builder.Property(x => x.CrossDockBin).HasColumnName("cross_dock_bin");
        builder.Property(x => x.Dedicated).HasColumnName("dedicated");
    }
}
