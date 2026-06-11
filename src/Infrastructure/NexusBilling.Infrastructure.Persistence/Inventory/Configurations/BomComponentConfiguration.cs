using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class BomComponentConfiguration : IEntityTypeConfiguration<BomComponent>
{
    public void Configure(EntityTypeBuilder<BomComponent> builder)
    {
        builder.ToTable("bom_component", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ParentItemNo).HasColumnName("parent_item_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.QuantityPer).HasColumnName("quantity_per").HasPrecision(18, 5);
        builder.Property(x => x.Position).HasColumnName("position");
        builder.Property(x => x.Position2).HasColumnName("position_2");
        builder.Property(x => x.Position3).HasColumnName("position_3");
        builder.Property(x => x.MachineNo).HasColumnName("machine_no");
        builder.Property(x => x.LeadTimeOffset).HasColumnName("lead_time_offset");
        builder.Property(x => x.ResourceUsageType).HasColumnName("resource_usage_type");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.InstalledInLineNo).HasColumnName("installed_in_line_no");
        builder.Property(x => x.InstalledInItemNo).HasColumnName("installed_in_item_no");
    }
}
