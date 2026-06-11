using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class VendorInvoiceDiscConfiguration : IEntityTypeConfiguration<VendorInvoiceDisc>
{
    public void Configure(EntityTypeBuilder<VendorInvoiceDisc> builder)
    {
        builder.ToTable("vendor_invoice_disc", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.MinimumAmount).HasColumnName("minimum_amount").HasPrecision(18, 5);
        builder.Property(x => x.Discount).HasColumnName("discount").HasPrecision(18, 5);
        builder.Property(x => x.ServiceCharge).HasColumnName("service_charge").HasPrecision(18, 5);
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
    }
}
