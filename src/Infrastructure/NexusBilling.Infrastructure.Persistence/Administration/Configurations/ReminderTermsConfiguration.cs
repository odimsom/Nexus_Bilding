using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ReminderTermsConfiguration : IEntityTypeConfiguration<ReminderTerms>
{
    public void Configure(EntityTypeBuilder<ReminderTerms> builder)
    {
        builder.ToTable("reminder_terms", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.PostInterest).HasColumnName("post_interest");
        builder.Property(x => x.PostAdditionalFee).HasColumnName("post_additional_fee");
        builder.Property(x => x.MaxNoOfReminders).HasColumnName("max_no_of_reminders");
        builder.Property(x => x.MinimumAmountLcy).HasColumnName("minimum_amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.PostAddFeePerLine).HasColumnName("post_add_fee_per_line");
        builder.Property(x => x.NoteAboutLineFeeOnReport).HasColumnName("note_about_line_fee_on_report");
    }
}
