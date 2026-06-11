using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("payment_method", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.BalAccountType).HasColumnName("bal_account_type");
        builder.Property(x => x.BalAccountNo).HasColumnName("bal_account_no");
        builder.Property(x => x.DirectDebit).HasColumnName("direct_debit");
        builder.Property(x => x.DirectDebitPmtTermsCode).HasColumnName("direct_debit_pmt_terms_code");
        builder.Property(x => x.PmtExportLineDefinition).HasColumnName("pmt_export_line_definition");
        builder.Property(x => x.BankDataConversionPmtType).HasColumnName("bank_data_conversion_pmt_type");
    }
}
