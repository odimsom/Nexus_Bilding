using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class SelectedDimensionConfiguration : IEntityTypeConfiguration<SelectedDimension>
{
    public void Configure(EntityTypeBuilder<SelectedDimension> builder)
    {
        builder.ToTable("selected_dimension", "administration");
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
    }
}
