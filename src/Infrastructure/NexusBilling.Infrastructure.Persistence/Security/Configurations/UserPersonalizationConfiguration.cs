using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class UserPersonalizationConfiguration : IEntityTypeConfiguration<UserPersonalization>
{
    public void Configure(EntityTypeBuilder<UserPersonalization> builder)
    {
        builder.ToTable("user_personalization", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserSid).HasColumnName("user_sid");
        builder.Property(x => x.ProfileId).HasColumnName("profile_id");
        builder.Property(x => x.LanguageId).HasColumnName("language_id");
        builder.Property(x => x.Company).HasColumnName("company");
        builder.Property(x => x.DebuggerBreakOnError).HasColumnName("debugger_break_on_error");
        builder.Property(x => x.DebuggerBreakOnRecChanges).HasColumnName("debugger_break_on_rec_changes");
        builder.Property(x => x.DebuggerSkipSystemTriggers).HasColumnName("debugger_skip_system_triggers");
        builder.Property(x => x.LocaleId).HasColumnName("locale_id");
        builder.Property(x => x.TimeZone).HasColumnName("time_zone");
    }
}
