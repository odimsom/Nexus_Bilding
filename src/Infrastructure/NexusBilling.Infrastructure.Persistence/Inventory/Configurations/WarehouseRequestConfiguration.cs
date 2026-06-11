using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class WarehouseRequestConfiguration : IEntityTypeConfiguration<WarehouseRequest>
{
    public void Configure(EntityTypeBuilder<WarehouseRequest> builder)
    {
        builder.ToTable("warehouse_request", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceSubtype).HasColumnName("source_subtype");
        builder.Property(x => x.SourceNo).HasColumnName("source_no");
        builder.Property(x => x.SourceDocument).HasColumnName("source_document");
        builder.Property(x => x.DocumentStatus).HasColumnName("document_status");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.ShipmentMethodCode).HasColumnName("shipment_method_code");
        builder.Property(x => x.ShippingAgentCode).HasColumnName("shipping_agent_code");
        builder.Property(x => x.ShippingAdvice).HasColumnName("shipping_advice");
        builder.Property(x => x.DestinationType).HasColumnName("destination_type");
        builder.Property(x => x.DestinationNo).HasColumnName("destination_no");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.ExpectedReceiptDate).HasColumnName("expected_receipt_date");
        builder.Property(x => x.ShipmentDate).HasColumnName("shipment_date");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.CompletelyHandled).HasColumnName("completely_handled");
    }
}
