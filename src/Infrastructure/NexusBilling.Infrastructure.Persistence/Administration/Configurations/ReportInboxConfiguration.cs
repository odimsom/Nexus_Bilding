using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ReportInboxConfiguration : IEntityTypeConfiguration<ReportInbox>
{
    public void Configure(EntityTypeBuilder<ReportInbox> builder)
    {
        builder.ToTable("report_inbox", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.ReportOutput).HasColumnName("report_output");
        builder.Property(x => x.CreatedDateTime).HasColumnName("created_date_time");
        builder.Property(x => x.JobQueueLogEntryId).HasColumnName("job_queue_log_entry_id");
        builder.Property(x => x.OutputType).HasColumnName("output_type");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ReportId).HasColumnName("report_id");
        builder.Property(x => x.Read).HasColumnName("read");
    }
}
