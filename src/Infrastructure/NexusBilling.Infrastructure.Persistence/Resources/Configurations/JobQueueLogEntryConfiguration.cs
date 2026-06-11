using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class JobQueueLogEntryConfiguration : IEntityTypeConfiguration<JobQueueLogEntry>
{
    public void Configure(EntityTypeBuilder<JobQueueLogEntry> builder)
    {
        builder.ToTable("job_queue_log_entry", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.StartDateTime).HasColumnName("start_date_time");
        builder.Property(x => x.EndDateTime).HasColumnName("end_date_time");
        builder.Property(x => x.ObjectTypeToRun).HasColumnName("object_type_to_run");
        builder.Property(x => x.ObjectIdToRun).HasColumnName("object_id_to_run");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ErrorMessage).HasColumnName("error_message");
        builder.Property(x => x.ErrorMessage2).HasColumnName("error_message_2");
        builder.Property(x => x.ErrorMessage3).HasColumnName("error_message_3");
        builder.Property(x => x.ErrorMessage4).HasColumnName("error_message_4");
        builder.Property(x => x.ProcessedByUserId).HasColumnName("processed_by_user_id");
        builder.Property(x => x.JobQueueCategoryCode).HasColumnName("job_queue_category_code");
    }
}
