using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class StockkeepingUnitConfiguration : IEntityTypeConfiguration<StockkeepingUnit>
{
    public void Configure(EntityTypeBuilder<StockkeepingUnit> builder)
    {
        builder.ToTable("stockkeeping_unit", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.ShelfNo).HasColumnName("shelf_no");
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.StandardCost).HasColumnName("standard_cost").HasPrecision(18, 5);
        builder.Property(x => x.LastDirectCost).HasColumnName("last_direct_cost").HasPrecision(18, 5);
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.VendorItemNo).HasColumnName("vendor_item_no");
        builder.Property(x => x.LeadTimeCalculation).HasColumnName("lead_time_calculation");
        builder.Property(x => x.ReorderPoint).HasColumnName("reorder_point").HasPrecision(18, 5);
        builder.Property(x => x.MaximumInventory).HasColumnName("maximum_inventory").HasPrecision(18, 5);
        builder.Property(x => x.ReorderQuantity).HasColumnName("reorder_quantity").HasPrecision(18, 5);
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.AssemblyPolicy).HasColumnName("assembly_policy");
        builder.Property(x => x.TransferLevelCode).HasColumnName("transfer_level_code");
        builder.Property(x => x.LotSize).HasColumnName("lot_size").HasPrecision(18, 5);
        builder.Property(x => x.DiscreteOrderQuantity).HasColumnName("discrete_order_quantity");
        builder.Property(x => x.MinimumOrderQuantity).HasColumnName("minimum_order_quantity").HasPrecision(18, 5);
        builder.Property(x => x.MaximumOrderQuantity).HasColumnName("maximum_order_quantity").HasPrecision(18, 5);
        builder.Property(x => x.SafetyStockQuantity).HasColumnName("safety_stock_quantity").HasPrecision(18, 5);
        builder.Property(x => x.OrderMultiple).HasColumnName("order_multiple").HasPrecision(18, 5);
        builder.Property(x => x.SafetyLeadTime).HasColumnName("safety_lead_time");
        builder.Property(x => x.ComponentsAtLocation).HasColumnName("components_at_location");
        builder.Property(x => x.FlushingMethod).HasColumnName("flushing_method");
        builder.Property(x => x.ReplenishmentSystem).HasColumnName("replenishment_system");
        builder.Property(x => x.TimeBucket).HasColumnName("time_bucket");
        builder.Property(x => x.ReorderingPolicy).HasColumnName("reordering_policy");
        builder.Property(x => x.IncludeInventory).HasColumnName("include_inventory");
        builder.Property(x => x.ManufacturingPolicy).HasColumnName("manufacturing_policy");
        builder.Property(x => x.ReschedulingPeriod).HasColumnName("rescheduling_period");
        builder.Property(x => x.LotAccumulationPeriod).HasColumnName("lot_accumulation_period");
        builder.Property(x => x.DampenerPeriod).HasColumnName("dampener_period");
        builder.Property(x => x.DampenerQuantity).HasColumnName("dampener_quantity").HasPrecision(18, 5);
        builder.Property(x => x.OverflowLevel).HasColumnName("overflow_level").HasPrecision(18, 5);
        builder.Property(x => x.TransferFromCode).HasColumnName("transfer_from_code");
        builder.Property(x => x.SpecialEquipmentCode).HasColumnName("special_equipment_code");
        builder.Property(x => x.PutAwayTemplateCode).HasColumnName("put_away_template_code");
        builder.Property(x => x.PutAwayUnitOfMeasureCode).HasColumnName("put_away_unit_of_measure_code");
        builder.Property(x => x.PhysInvtCountingPeriodCode).HasColumnName("phys_invt_counting_period_code");
        builder.Property(x => x.LastCountingPeriodUpdate).HasColumnName("last_counting_period_update");
        builder.Property(x => x.UseCrossDocking).HasColumnName("use_cross_docking");
        builder.Property(x => x.NextCountingStartDate).HasColumnName("next_counting_start_date");
        builder.Property(x => x.NextCountingEndDate).HasColumnName("next_counting_end_date");
    }
}
