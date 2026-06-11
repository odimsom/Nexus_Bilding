using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class PaymentTermsConfiguration : IEntityTypeConfiguration<PaymentTerms>
{
    public void Configure(EntityTypeBuilder<PaymentTerms> builder)
    {
        builder.ToTable("payment_terms", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.DueDateCalculation).HasColumnName("due_date_calculation");
        builder.Property(x => x.DiscountDateCalculation).HasColumnName("discount_date_calculation");
        builder.Property(x => x.Discount).HasColumnName("discount").HasPrecision(18, 5);
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CalcPmtDiscOnCrMemos).HasColumnName("calc_pmt_disc_on_cr_memos");
    }
}
