using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ProdOrderLineConfiguration : IEntityTypeConfiguration<ProdOrderLine>
{
    public void Configure(EntityTypeBuilder<ProdOrderLine> builder)
    {
        builder.ToTable("prod_order_line", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.ProdOrderNo).HasColumnName("prod_order_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension_1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension_2_code");
        builder.Property(x => x.BinCode).HasColumnName("bin_code");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.FinishedQuantity).HasColumnName("finished_quantity").HasPrecision(18, 5);
        builder.Property(x => x.RemainingQuantity).HasColumnName("remaining_quantity").HasPrecision(18, 5);
        builder.Property(x => x.Scrap).HasColumnName("scrap").HasPrecision(18, 5);
        builder.Property(x => x.DueDate).HasColumnName("due_date");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.StartingTime).HasColumnName("starting_time");
        builder.Property(x => x.EndingDate).HasColumnName("ending_date");
        builder.Property(x => x.EndingTime).HasColumnName("ending_time");
        builder.Property(x => x.PlanningLevelCode).HasColumnName("planning_level_code");
        builder.Property(x => x.Priority).HasColumnName("priority");
        builder.Property(x => x.ProductionBomNo).HasColumnName("production_bom_no");
        builder.Property(x => x.RoutingNo).HasColumnName("routing_no");
        builder.Property(x => x.InventoryPostingGroup).HasColumnName("inventory_posting_group");
        builder.Property(x => x.RoutingReferenceNo).HasColumnName("routing_reference_no");
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.CostAmount).HasColumnName("cost_amount").HasPrecision(18, 5);
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.QuantityBase).HasColumnName("quantity_base").HasPrecision(18, 5);
        builder.Property(x => x.FinishedQtyBase).HasColumnName("finished_qty_base").HasPrecision(18, 5);
        builder.Property(x => x.RemainingQtyBase).HasColumnName("remaining_qty_base").HasPrecision(18, 5);
        builder.Property(x => x.StartingDateTime).HasColumnName("starting_date_time");
        builder.Property(x => x.EndingDateTime).HasColumnName("ending_date_time");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.CostAmountAcy).HasColumnName("cost_amount_acy").HasPrecision(18, 5);
        builder.Property(x => x.UnitCostAcy).HasColumnName("unit_cost_acy").HasPrecision(18, 5);
        builder.Property(x => x.ProductionBomVersionCode).HasColumnName("production_bom_version_code");
        builder.Property(x => x.RoutingVersionCode).HasColumnName("routing_version_code");
        builder.Property(x => x.RoutingType).HasColumnName("routing_type");
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.MpsOrder).HasColumnName("mps_order");
        builder.Property(x => x.PlanningFlexibility).HasColumnName("planning_flexibility");
        builder.Property(x => x.IndirectCost).HasColumnName("indirect_cost").HasPrecision(18, 5);
        builder.Property(x => x.OverheadRate).HasColumnName("overhead_rate").HasPrecision(18, 5);
    }
}
