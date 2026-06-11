using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class FinanceChargeTermsConfiguration : IEntityTypeConfiguration<FinanceChargeTerms>
{
    public void Configure(EntityTypeBuilder<FinanceChargeTerms> builder)
    {
        builder.ToTable("finance_charge_terms", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.InterestRate).HasColumnName("interest_rate").HasPrecision(18, 5);
        builder.Property(x => x.MinimumAmountLcy).HasColumnName("minimum_amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.AdditionalFeeLcy).HasColumnName("additional_fee_lcy").HasPrecision(18, 5);
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.InterestCalculationMethod).HasColumnName("interest_calculation_method");
        builder.Property(x => x.InterestPeriodDays).HasColumnName("interest_period_days");
        builder.Property(x => x.GracePeriod).HasColumnName("grace_period");
        builder.Property(x => x.DueDateCalculation).HasColumnName("due_date_calculation");
        builder.Property(x => x.InterestCalculation).HasColumnName("interest_calculation");
        builder.Property(x => x.PostInterest).HasColumnName("post_interest");
        builder.Property(x => x.PostAdditionalFee).HasColumnName("post_additional_fee");
        builder.Property(x => x.LineDescription).HasColumnName("line_description");
        builder.Property(x => x.AddLineFeeInInterest).HasColumnName("add_line_fee_in_interest");
    }
}
