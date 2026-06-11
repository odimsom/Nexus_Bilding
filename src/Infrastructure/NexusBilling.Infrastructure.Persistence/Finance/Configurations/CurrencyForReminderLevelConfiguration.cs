using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CurrencyForReminderLevelConfiguration : IEntityTypeConfiguration<CurrencyForReminderLevel>
{
    public void Configure(EntityTypeBuilder<CurrencyForReminderLevel> builder)
    {
        builder.ToTable("currency_for_reminder_level", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ReminderTermsCode).HasColumnName("reminder_terms_code");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.AdditionalFee).HasColumnName("additional_fee").HasPrecision(18, 5);
        builder.Property(x => x.AddFeePerLine).HasColumnName("add_fee_per_line").HasPrecision(18, 5);
    }
}
