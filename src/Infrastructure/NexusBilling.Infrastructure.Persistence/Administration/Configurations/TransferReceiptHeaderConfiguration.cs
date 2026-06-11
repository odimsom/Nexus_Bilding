using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class TransferReceiptHeaderConfiguration : IEntityTypeConfiguration<TransferReceiptHeader>
{
    public void Configure(EntityTypeBuilder<TransferReceiptHeader> builder)
    {
        builder.ToTable("transfer_receipt_header", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.TransferFromCode).HasColumnName("transfer_from_code");
        builder.Property(x => x.TransferFromName).HasColumnName("transfer_from_name");
        builder.Property(x => x.TransferFromName2).HasColumnName("transfer_from_name_2");
        builder.Property(x => x.TransferFromAddress).HasColumnName("transfer_from_address");
        builder.Property(x => x.TransferFromAddress2).HasColumnName("transfer_from_address_2");
        builder.Property(x => x.TransferFromPostCode).HasColumnName("transfer_from_post_code");
        builder.Property(x => x.TransferFromCity).HasColumnName("transfer_from_city");
        builder.Property(x => x.TransferFromCounty).HasColumnName("transfer_from_county");
        builder.Property(x => x.TrsfFromCountryRegionCode).HasColumnName("trsf_from_country_region_code");
        builder.Property(x => x.TransferToCode).HasColumnName("transfer_to_code");
        builder.Property(x => x.TransferToName).HasColumnName("transfer_to_name");
        builder.Property(x => x.TransferToName2).HasColumnName("transfer_to_name_2");
        builder.Property(x => x.TransferToAddress).HasColumnName("transfer_to_address");
        builder.Property(x => x.TransferToAddress2).HasColumnName("transfer_to_address_2");
        builder.Property(x => x.TransferToPostCode).HasColumnName("transfer_to_post_code");
        builder.Property(x => x.TransferToCity).HasColumnName("transfer_to_city");
        builder.Property(x => x.TransferToCounty).HasColumnName("transfer_to_county");
        builder.Property(x => x.TrsfToCountryRegionCode).HasColumnName("trsf_to_country_region_code");
        builder.Property(x => x.TransferOrderDate).HasColumnName("transfer_order_date");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension_1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension_2_code");
        builder.Property(x => x.TransferOrderNo).HasColumnName("transfer_order_no");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.ShipmentDate).HasColumnName("shipment_date");
        builder.Property(x => x.ReceiptDate).HasColumnName("receipt_date");
        builder.Property(x => x.InTransitCode).HasColumnName("in_transit_code");
        builder.Property(x => x.TransferFromContact).HasColumnName("transfer_from_contact");
        builder.Property(x => x.TransferToContact).HasColumnName("transfer_to_contact");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.ShippingAgentCode).HasColumnName("shipping_agent_code");
        builder.Property(x => x.ShippingAgentServiceCode).HasColumnName("shipping_agent_service_code");
        builder.Property(x => x.ShipmentMethodCode).HasColumnName("shipment_method_code");
        builder.Property(x => x.TransactionType).HasColumnName("transaction_type");
        builder.Property(x => x.TransportMethod).HasColumnName("transport_method");
        builder.Property(x => x.EntryExitPoint).HasColumnName("entry_exit_point");
        builder.Property(x => x.Area).HasColumnName("area");
        builder.Property(x => x.TransactionSpecification).HasColumnName("transaction_specification");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
    }
}
