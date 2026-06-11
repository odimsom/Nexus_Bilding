using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class InventorySetupConfiguration : IEntityTypeConfiguration<InventorySetup>
{
    public void Configure(EntityTypeBuilder<InventorySetup> builder)
    {
        builder.ToTable("inventory_setup", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.AutomaticCostPosting).HasColumnName("automatic_cost_posting");
        builder.Property(x => x.LocationMandatory).HasColumnName("location_mandatory");
        builder.Property(x => x.ItemNos).HasColumnName("item_nos");
        builder.Property(x => x.AutomaticCostAdjustment).HasColumnName("automatic_cost_adjustment");
        builder.Property(x => x.PreventNegativeInventory).HasColumnName("prevent_negative_inventory");
        builder.Property(x => x.TransferOrderNos).HasColumnName("transfer_order_nos");
        builder.Property(x => x.PostedTransferShptNos).HasColumnName("posted_transfer_shpt_nos");
        builder.Property(x => x.PostedTransferRcptNos).HasColumnName("posted_transfer_rcpt_nos");
        builder.Property(x => x.CopyCommentsOrderToShpt).HasColumnName("copy_comments_order_to_shpt");
        builder.Property(x => x.CopyCommentsOrderToRcpt).HasColumnName("copy_comments_order_to_rcpt");
        builder.Property(x => x.NonstockItemNos).HasColumnName("nonstock_item_nos");
        builder.Property(x => x.OutboundWhseHandlingTime).HasColumnName("outbound_whse_handling_time");
        builder.Property(x => x.InboundWhseHandlingTime).HasColumnName("inbound_whse_handling_time");
        builder.Property(x => x.ExpectedCostPostingToGL).HasColumnName("expected_cost_posting_to_g_l");
        builder.Property(x => x.AverageCostCalcType).HasColumnName("average_cost_calc_type");
        builder.Property(x => x.AverageCostPeriod).HasColumnName("average_cost_period");
        builder.Property(x => x.ItemGroupDimensionCode).HasColumnName("item_group_dimension_code");
        builder.Property(x => x.InventoryPutAwayNos).HasColumnName("inventory_put_away_nos");
        builder.Property(x => x.InventoryPickNos).HasColumnName("inventory_pick_nos");
        builder.Property(x => x.PostedInvtPutAwayNos).HasColumnName("posted_invt_put_away_nos");
        builder.Property(x => x.PostedInvtPickNos).HasColumnName("posted_invt_pick_nos");
        builder.Property(x => x.InventoryMovementNos).HasColumnName("inventory_movement_nos");
        builder.Property(x => x.RegisteredInvtMovementNos).HasColumnName("registered_invt_movement_nos");
        builder.Property(x => x.InternalMovementNos).HasColumnName("internal_movement_nos");
    }
}
