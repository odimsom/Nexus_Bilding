using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder.ToTable("vendor", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Address).HasColumnName("address");
        builder.Property(x => x.City).HasColumnName("city");
        builder.Property(x => x.Contact).HasColumnName("contact");
        builder.Property(x => x.PhoneNo).HasColumnName("phone_no").HasDefaultValue(string.Empty);
        builder.Property(x => x.PhoneNo2).HasColumnName("phone_no_2").HasDefaultValue(string.Empty);
        builder.Property(x => x.Email).HasColumnName("email").HasDefaultValue(string.Empty);
        builder.Property(x => x.WebSite).HasColumnName("web_site").HasDefaultValue(string.Empty);
        builder.Property(x => x.Rnc).HasColumnName("rnc").HasDefaultValue(string.Empty);
        builder.Property(x => x.Address2).HasColumnName("address_2").HasDefaultValue(string.Empty);
        builder.Property(x => x.Province).HasColumnName("province").HasDefaultValue(string.Empty);
        builder.Property(x => x.Country).HasColumnName("country").HasDefaultValue(string.Empty);
        builder.Property(x => x.PaymentTermsCode).HasColumnName("payment_terms_code").HasDefaultValue(string.Empty);
        builder.Property(x => x.PaymentMethodCode).HasColumnName("payment_method_code").HasDefaultValue(string.Empty);
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code").HasDefaultValue(string.Empty);
        builder.Property(x => x.CreditLimit).HasColumnName("credit_limit").HasDefaultValue(0m);
        builder.Property(x => x.VendorType).HasColumnName("vendor_type").HasDefaultValue(string.Empty);
        builder.Property(x => x.Blocked).HasColumnName("blocked");
    }
}
