using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class WarehouseShipmentHeaderConfiguration : IEntityTypeConfiguration<WarehouseShipmentHeader>
{
    public void Configure(EntityTypeBuilder<WarehouseShipmentHeader> builder)
    {
        builder.ToTable("warehouse_shipment_header", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.AssignedUserId).HasColumnName("assigned_user_id");
        builder.Property(x => x.AssignmentDate).HasColumnName("assignment_date");
        builder.Property(x => x.AssignmentTime).HasColumnName("assignment_time");
        builder.Property(x => x.SortingMethod).HasColumnName("sorting_method");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.BinCode).HasColumnName("bin_code");
        builder.Property(x => x.ZoneCode).HasColumnName("zone_code");
        builder.Property(x => x.DocumentStatus).HasColumnName("document_status");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.ShippingAgentCode).HasColumnName("shipping_agent_code");
        builder.Property(x => x.ShippingAgentServiceCode).HasColumnName("shipping_agent_service_code");
        builder.Property(x => x.ShipmentMethodCode).HasColumnName("shipment_method_code");
        builder.Property(x => x.ShipmentDate).HasColumnName("shipment_date");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.CreatePostedHeader).HasColumnName("create_posted_header");
        builder.Property(x => x.ShippingNo).HasColumnName("shipping_no");
        builder.Property(x => x.LastShippingNo).HasColumnName("last_shipping_no");
        builder.Property(x => x.ShippingNoSeries).HasColumnName("shipping_no_series");
    }
}
