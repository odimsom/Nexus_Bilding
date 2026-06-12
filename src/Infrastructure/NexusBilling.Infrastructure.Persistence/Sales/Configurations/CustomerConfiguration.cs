using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customer", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Address).HasColumnName("address");
        builder.Property(x => x.City).HasColumnName("city");
        builder.Property(x => x.Contact).HasColumnName("contact");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.PhoneNo).HasColumnName("phone_no");
        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.CreditLimit).HasColumnName("credit_limit");
        builder.Property(x => x.Balance).HasColumnName("balance");
        builder.Property(x => x.BalanceDue).HasColumnName("balance_due");
        builder.Property(x => x.VatRegistrationNo).HasColumnName("vat_registration_no");
        builder.Property(x => x.PaymentTermsCode).HasColumnName("payment_terms_code");
        builder.Property(x => x.PaymentMethodCode).HasColumnName("payment_method_code");
        builder.Property(x => x.SalespersonCode).HasColumnName("salesperson_code");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.CustomerPostingGroup).HasColumnName("customer_posting_group");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
    }
}
