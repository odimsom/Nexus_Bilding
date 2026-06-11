using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class JobUsageLinkConfiguration : IEntityTypeConfiguration<JobUsageLink>
{
    public void Configure(EntityTypeBuilder<JobUsageLink> builder)
    {
        builder.ToTable("job_usage_link", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JobNo).HasColumnName("job_no");
        builder.Property(x => x.JobTaskNo).HasColumnName("job_task_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
    }
}
