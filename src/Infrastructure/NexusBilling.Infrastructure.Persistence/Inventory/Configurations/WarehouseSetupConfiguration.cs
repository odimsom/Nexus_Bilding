using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class WarehouseSetupConfiguration : IEntityTypeConfiguration<WarehouseSetup>
{
    public void Configure(EntityTypeBuilder<WarehouseSetup> builder)
    {
        builder.ToTable("warehouse_setup", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.WhseReceiptNos).HasColumnName("whse_receipt_nos");
        builder.Property(x => x.WhsePutAwayNos).HasColumnName("whse_put_away_nos");
        builder.Property(x => x.WhsePickNos).HasColumnName("whse_pick_nos");
        builder.Property(x => x.WhseShipNos).HasColumnName("whse_ship_nos");
        builder.Property(x => x.RegisteredWhsePickNos).HasColumnName("registered_whse_pick_nos");
        builder.Property(x => x.RegisteredWhsePutAwayNos).HasColumnName("registered_whse_put_away_nos");
        builder.Property(x => x.RequireReceive).HasColumnName("require_receive");
        builder.Property(x => x.RequirePutAway).HasColumnName("require_put_away");
        builder.Property(x => x.RequirePick).HasColumnName("require_pick");
        builder.Property(x => x.RequireShipment).HasColumnName("require_shipment");
        builder.Property(x => x.LastWhsePostingRefNo).HasColumnName("last_whse_posting_ref_no");
        builder.Property(x => x.ReceiptPostingPolicy).HasColumnName("receipt_posting_policy");
        builder.Property(x => x.ShipmentPostingPolicy).HasColumnName("shipment_posting_policy");
        builder.Property(x => x.PostedWhseReceiptNos).HasColumnName("posted_whse_receipt_nos");
        builder.Property(x => x.PostedWhseShipmentNos).HasColumnName("posted_whse_shipment_nos");
        builder.Property(x => x.WhseInternalPutAwayNos).HasColumnName("whse_internal_put_away_nos");
        builder.Property(x => x.WhseInternalPickNos).HasColumnName("whse_internal_pick_nos");
        builder.Property(x => x.WhseMovementNos).HasColumnName("whse_movement_nos");
        builder.Property(x => x.RegisteredWhseMovementNos).HasColumnName("registered_whse_movement_nos");
    }
}
