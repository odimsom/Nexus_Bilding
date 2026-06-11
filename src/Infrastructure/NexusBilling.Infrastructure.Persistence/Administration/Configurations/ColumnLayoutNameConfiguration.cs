using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ColumnLayoutNameConfiguration : IEntityTypeConfiguration<ColumnLayoutName>
{
    public void Configure(EntityTypeBuilder<ColumnLayoutName> builder)
    {
        builder.ToTable("column_layout_name", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.AnalysisViewName).HasColumnName("analysis_view_name");
    }
}
