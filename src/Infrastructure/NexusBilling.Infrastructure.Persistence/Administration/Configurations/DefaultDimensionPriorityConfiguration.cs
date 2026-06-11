using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DefaultDimensionPriorityConfiguration : IEntityTypeConfiguration<DefaultDimensionPriority>
{
    public void Configure(EntityTypeBuilder<DefaultDimensionPriority> builder)
    {
        builder.ToTable("default_dimension_priority", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.Priority).HasColumnName("priority");
    }
}
