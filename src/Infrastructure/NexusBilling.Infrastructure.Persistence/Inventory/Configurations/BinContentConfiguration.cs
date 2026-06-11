using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class BinContentConfiguration : IEntityTypeConfiguration<BinContent>
{
    public void Configure(EntityTypeBuilder<BinContent> builder)
    {
        builder.ToTable("bin_content", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.ZoneCode).HasColumnName("zone_code");
        builder.Property(x => x.BinCode).HasColumnName("bin_code");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.BinTypeCode).HasColumnName("bin_type_code");
        builder.Property(x => x.WarehouseClassCode).HasColumnName("warehouse_class_code");
        builder.Property(x => x.BlockMovement).HasColumnName("block_movement");
        builder.Property(x => x.MinQty).HasColumnName("min_qty").HasPrecision(18, 5);
        builder.Property(x => x.MaxQty).HasColumnName("max_qty").HasPrecision(18, 5);
        builder.Property(x => x.BinRanking).HasColumnName("bin_ranking");
        builder.Property(x => x.Fixed).HasColumnName("fixed");
        builder.Property(x => x.CrossDockBin).HasColumnName("cross_dock_bin");
        builder.Property(x => x.Default).HasColumnName("default");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.Dedicated).HasColumnName("dedicated");
    }
}
