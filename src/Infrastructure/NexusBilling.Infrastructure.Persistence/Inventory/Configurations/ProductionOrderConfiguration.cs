using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ProductionOrderConfiguration : IEntityTypeConfiguration<ProductionOrder>
{
    public void Configure(EntityTypeBuilder<ProductionOrder> builder)
    {
        builder.ToTable("production_order", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.SearchDescription).HasColumnName("search_description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.CreationDate).HasColumnName("creation_date");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceNo).HasColumnName("source_no");
        builder.Property(x => x.RoutingNo).HasColumnName("routing_no");
        builder.Property(x => x.InventoryPostingGroup).HasColumnName("inventory_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.StartingTime).HasColumnName("starting_time");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.EndingTime).HasColumnName("ending_time");
        builder.Property(x => x.EndingDate).HasColumnName("ending_date");
        builder.Property(x => x.DueDate).HasColumnName("due_date");
        builder.Property(x => x.FinishedDate).HasColumnName("finished_date");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension_1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension_2_code");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.BinCode).HasColumnName("bin_code");
        builder.Property(x => x.ReplanRefNo).HasColumnName("replan_ref_no");
        builder.Property(x => x.ReplanRefStatus).HasColumnName("replan_ref_status");
        builder.Property(x => x.LowLevelCode).HasColumnName("low_level_code");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.CostAmount).HasColumnName("cost_amount").HasPrecision(18, 5);
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.PlannedOrderNo).HasColumnName("planned_order_no");
        builder.Property(x => x.FirmPlannedOrderNo).HasColumnName("firm_planned_order_no");
        builder.Property(x => x.SimulatedOrderNo).HasColumnName("simulated_order_no");
        builder.Property(x => x.StartingDateTime).HasColumnName("starting_date_time");
        builder.Property(x => x.EndingDateTime).HasColumnName("ending_date_time");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.AssignedUserId).HasColumnName("assigned_user_id");
    }
}
