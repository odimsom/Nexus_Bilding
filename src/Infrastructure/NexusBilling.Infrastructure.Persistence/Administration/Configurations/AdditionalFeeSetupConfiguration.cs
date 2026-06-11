using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AdditionalFeeSetupConfiguration : IEntityTypeConfiguration<AdditionalFeeSetup>
{
    public void Configure(EntityTypeBuilder<AdditionalFeeSetup> builder)
    {
        builder.ToTable("additional_fee_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ChargePerLine).HasColumnName("charge_per_line");
        builder.Property(x => x.ReminderTermsCode).HasColumnName("reminder_terms_code");
        builder.Property(x => x.ReminderLevelNo).HasColumnName("reminder_level_no");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.ThresholdRemainingAmount).HasColumnName("threshold_remaining_amount").HasPrecision(18, 5);
        builder.Property(x => x.AdditionalFeeAmount).HasColumnName("additional_fee_amount").HasPrecision(18, 5);
        builder.Property(x => x.AdditionalFee).HasColumnName("additional_fee").HasPrecision(18, 5);
        builder.Property(x => x.MinAdditionalFeeAmount).HasColumnName("min_additional_fee_amount").HasPrecision(18, 5);
        builder.Property(x => x.MaxAdditionalFeeAmount).HasColumnName("max_additional_fee_amount").HasPrecision(18, 5);
    }
}
