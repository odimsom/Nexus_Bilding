using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class StandardCustomerSalesCodeConfiguration : IEntityTypeConfiguration<StandardCustomerSalesCode>
{
    public void Configure(EntityTypeBuilder<StandardCustomerSalesCode> builder)
    {
        builder.ToTable("standard_customer_sales_code", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.CustomerNo).HasColumnName("customer_no");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ValidFromDate).HasColumnName("valid_from_date");
        builder.Property(x => x.ValidToDate).HasColumnName("valid_to_date");
        builder.Property(x => x.PaymentMethodCode).HasColumnName("payment_method_code");
        builder.Property(x => x.PaymentTermsCode).HasColumnName("payment_terms_code");
        builder.Property(x => x.DirectDebitMandateId).HasColumnName("direct_debit_mandate_id");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
    }
}
