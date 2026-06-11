using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class PostedAssemblyLineConfiguration : IEntityTypeConfiguration<PostedAssemblyLine>
{
    public void Configure(EntityTypeBuilder<PostedAssemblyLine> builder)
    {
        builder.ToTable("posted_assembly_line", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.OrderNo).HasColumnName("order_no");
        builder.Property(x => x.OrderLineNo).HasColumnName("order_line_no");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.LeadTimeOffset).HasColumnName("lead_time_offset");
        builder.Property(x => x.ResourceUsageType).HasColumnName("resource_usage_type");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension_1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension_2_code");
        builder.Property(x => x.BinCode).HasColumnName("bin_code");
        builder.Property(x => x.Position).HasColumnName("position");
        builder.Property(x => x.Position2).HasColumnName("position_2");
        builder.Property(x => x.Position3).HasColumnName("position_3");
        builder.Property(x => x.ItemShptEntryNo).HasColumnName("item_shpt_entry_no");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.QuantityBase).HasColumnName("quantity_base").HasPrecision(18, 5);
        builder.Property(x => x.DueDate).HasColumnName("due_date");
        builder.Property(x => x.QuantityPer).HasColumnName("quantity_per").HasPrecision(18, 5);
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.InventoryPostingGroup).HasColumnName("inventory_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.CostAmount).HasColumnName("cost_amount").HasPrecision(18, 5);
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
    }
}
