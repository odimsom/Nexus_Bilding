using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class CustInvoiceDiscConfiguration : IEntityTypeConfiguration<CustInvoiceDisc>
{
    public void Configure(EntityTypeBuilder<CustInvoiceDisc> builder)
    {
        builder.ToTable("cust_invoice_disc", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.MinimumAmount).HasColumnName("minimum_amount").HasPrecision(18, 5);
        builder.Property(x => x.Discount).HasColumnName("discount").HasPrecision(18, 5);
        builder.Property(x => x.ServiceCharge).HasColumnName("service_charge").HasPrecision(18, 5);
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
    }
}
