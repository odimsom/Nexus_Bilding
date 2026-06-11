using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class CertificateOfSupplyConfiguration : IEntityTypeConfiguration<CertificateOfSupply>
{
    public void Configure(EntityTypeBuilder<CertificateOfSupply> builder)
    {
        builder.ToTable("certificate_of_supply", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.ReceiptDate).HasColumnName("receipt_date");
        builder.Property(x => x.Printed).HasColumnName("printed");
        builder.Property(x => x.CustomerVendorName).HasColumnName("customer_vendor_name");
        builder.Property(x => x.ShipmentMethodCode).HasColumnName("shipment_method_code");
        builder.Property(x => x.ShipmentPostingDate).HasColumnName("shipment_posting_date");
        builder.Property(x => x.ShipToCountryRegionCode).HasColumnName("ship_to_country_region_code");
        builder.Property(x => x.CustomerVendorNo).HasColumnName("customer_vendor_no");
        builder.Property(x => x.VehicleRegistrationNo).HasColumnName("vehicle_registration_no");
    }
}
