using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AnalysisViewFilterConfiguration : IEntityTypeConfiguration<AnalysisViewFilter>
{
    public void Configure(EntityTypeBuilder<AnalysisViewFilter> builder)
    {
        builder.ToTable("analysis_view_filter", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AnalysisViewCode).HasColumnName("analysis_view_code");
        builder.Property(x => x.DimensionCode).HasColumnName("dimension_code");
        builder.Property(x => x.DimensionValueFilter).HasColumnName("dimension_value_filter");
    }
}
