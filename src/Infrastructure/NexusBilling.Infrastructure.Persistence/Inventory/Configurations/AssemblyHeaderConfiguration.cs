using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class AssemblyHeaderConfiguration : IEntityTypeConfiguration<AssemblyHeader>
{
    public void Configure(EntityTypeBuilder<AssemblyHeader> builder)
    {
        builder.ToTable("assembly_header", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.SearchDescription).HasColumnName("search_description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.CreationDate).HasColumnName("creation_date");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.InventoryPostingGroup).HasColumnName("inventory_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension_1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension_2_code");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.DueDate).HasColumnName("due_date");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.EndingDate).HasColumnName("ending_date");
        builder.Property(x => x.BinCode).HasColumnName("bin_code");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.QuantityBase).HasColumnName("quantity_base").HasPrecision(18, 5);
        builder.Property(x => x.RemainingQuantity).HasColumnName("remaining_quantity").HasPrecision(18, 5);
        builder.Property(x => x.RemainingQuantityBase).HasColumnName("remaining_quantity_base").HasPrecision(18, 5);
        builder.Property(x => x.AssembledQuantity).HasColumnName("assembled_quantity").HasPrecision(18, 5);
        builder.Property(x => x.AssembledQuantityBase).HasColumnName("assembled_quantity_base").HasPrecision(18, 5);
        builder.Property(x => x.QuantityToAssemble).HasColumnName("quantity_to_assemble").HasPrecision(18, 5);
        builder.Property(x => x.QuantityToAssembleBase).HasColumnName("quantity_to_assemble_base").HasPrecision(18, 5);
        builder.Property(x => x.PlanningFlexibility).HasColumnName("planning_flexibility");
        builder.Property(x => x.MpsOrder).HasColumnName("mps_order");
        builder.Property(x => x.PostingNo).HasColumnName("posting_no");
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.CostAmount).HasColumnName("cost_amount").HasPrecision(18, 5);
        builder.Property(x => x.IndirectCost).HasColumnName("indirect_cost").HasPrecision(18, 5);
        builder.Property(x => x.OverheadRate).HasColumnName("overhead_rate").HasPrecision(18, 5);
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.PostingNoSeries).HasColumnName("posting_no_series");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.AssignedUserId).HasColumnName("assigned_user_id");
    }
}
