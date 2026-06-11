using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class InventoryAdjmtEntryOrderConfiguration : IEntityTypeConfiguration<InventoryAdjmtEntryOrder>
{
    public void Configure(EntityTypeBuilder<InventoryAdjmtEntryOrder> builder)
    {
        builder.ToTable("inventory_adjmt_entry_order", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.OrderType).HasColumnName("order_type");
        builder.Property(x => x.OrderNo).HasColumnName("order_no");
        builder.Property(x => x.OrderLineNo).HasColumnName("order_line_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.RoutingNo).HasColumnName("routing_no");
        builder.Property(x => x.RoutingReferenceNo).HasColumnName("routing_reference_no");
        builder.Property(x => x.IndirectCost).HasColumnName("indirect_cost").HasPrecision(18, 5);
        builder.Property(x => x.OverheadRate).HasColumnName("overhead_rate").HasPrecision(18, 5);
        builder.Property(x => x.CostIsAdjusted).HasColumnName("cost_is_adjusted");
        builder.Property(x => x.AllowOnlineAdjustment).HasColumnName("allow_online_adjustment");
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.DirectCost).HasColumnName("direct_cost").HasPrecision(18, 5);
        builder.Property(x => x.IndirectCostNav).HasColumnName("indirect_cost_nav").HasPrecision(18, 5);
        builder.Property(x => x.SingleLevelMaterialCost).HasColumnName("single_level_material_cost").HasPrecision(18, 5);
        builder.Property(x => x.SingleLevelCapacityCost).HasColumnName("single_level_capacity_cost").HasPrecision(18, 5);
        builder.Property(x => x.SingleLevelSubcontrdCost).HasColumnName("single_level_subcontrd_cost").HasPrecision(18, 5);
        builder.Property(x => x.SingleLevelCapOvhdCost).HasColumnName("single_level_cap_ovhd_cost").HasPrecision(18, 5);
        builder.Property(x => x.SingleLevelMfgOvhdCost).HasColumnName("single_level_mfg_ovhd_cost").HasPrecision(18, 5);
        builder.Property(x => x.DirectCostAcy).HasColumnName("direct_cost_acy").HasPrecision(18, 5);
        builder.Property(x => x.IndirectCostAcy).HasColumnName("indirect_cost_acy").HasPrecision(18, 5);
        builder.Property(x => x.SingleLvlMaterialCostAcy).HasColumnName("single_lvl_material_cost_acy").HasPrecision(18, 5);
        builder.Property(x => x.SingleLvlCapacityCostAcy).HasColumnName("single_lvl_capacity_cost_acy").HasPrecision(18, 5);
        builder.Property(x => x.SingleLvlSubcontrdCostAcy).HasColumnName("single_lvl_subcontrd_cost_acy").HasPrecision(18, 5);
        builder.Property(x => x.SingleLvlCapOvhdCostAcy).HasColumnName("single_lvl_cap_ovhd_cost_acy").HasPrecision(18, 5);
        builder.Property(x => x.SingleLvlMfgOvhdCostAcy).HasColumnName("single_lvl_mfg_ovhd_cost_acy").HasPrecision(18, 5);
        builder.Property(x => x.CompletelyInvoiced).HasColumnName("completely_invoiced");
        builder.Property(x => x.IsFinished).HasColumnName("is_finished");
    }
}
