using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ScheduledTaskConfiguration : IEntityTypeConfiguration<ScheduledTask>
{
    public void Configure(EntityTypeBuilder<ScheduledTask> builder)
    {
        builder.ToTable("scheduled_task", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.UserName).HasColumnName("user_name");
        builder.Property(x => x.UserLanguageId).HasColumnName("user_language_id");
        builder.Property(x => x.UserFormatId).HasColumnName("user_format_id");
        builder.Property(x => x.UserTimeZone).HasColumnName("user_time_zone");
        builder.Property(x => x.Company).HasColumnName("company");
        builder.Property(x => x.IsReady).HasColumnName("is_ready");
        builder.Property(x => x.NotBefore).HasColumnName("not_before");
        builder.Property(x => x.RunCodeunit).HasColumnName("run_codeunit");
        builder.Property(x => x.FailureCodeunit).HasColumnName("failure_codeunit");
        builder.Property(x => x.Record).HasColumnName("record");
    }
}
