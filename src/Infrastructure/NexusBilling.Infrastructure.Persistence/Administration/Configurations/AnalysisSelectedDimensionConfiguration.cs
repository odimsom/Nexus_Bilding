using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AnalysisSelectedDimensionConfiguration : IEntityTypeConfiguration<AnalysisSelectedDimension>
{
    public void Configure(EntityTypeBuilder<AnalysisSelectedDimension> builder)
    {
        builder.ToTable("analysis_selected_dimension", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.ObjectType).HasColumnName("object_type");
        builder.Property(x => x.ObjectId).HasColumnName("object_id");
        builder.Property(x => x.DimensionCode).HasColumnName("dimension_code");
        builder.Property(x => x.NewDimensionValueCode).HasColumnName("new_dimension_value_code");
        builder.Property(x => x.DimensionValueFilter).HasColumnName("dimension_value_filter");
        builder.Property(x => x.Level).HasColumnName("level");
        builder.Property(x => x.AnalysisViewCode).HasColumnName("analysis_view_code");
        builder.Property(x => x.AnalysisArea).HasColumnName("analysis_area");
    }
}
