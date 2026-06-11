using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class JobTaskDimensionConfiguration : IEntityTypeConfiguration<JobTaskDimension>
{
    public void Configure(EntityTypeBuilder<JobTaskDimension> builder)
    {
        builder.ToTable("job_task_dimension", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JobNo).HasColumnName("job_no");
        builder.Property(x => x.JobTaskNo).HasColumnName("job_task_no");
        builder.Property(x => x.DimensionCode).HasColumnName("dimension_code");
        builder.Property(x => x.DimensionValueCode).HasColumnName("dimension_value_code");
        builder.Property(x => x.MultipleSelectionAction).HasColumnName("multiple_selection_action");
    }
}
