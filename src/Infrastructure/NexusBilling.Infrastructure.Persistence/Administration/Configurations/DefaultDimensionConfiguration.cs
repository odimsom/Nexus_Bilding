using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DefaultDimensionConfiguration : IEntityTypeConfiguration<DefaultDimension>
{
    public void Configure(EntityTypeBuilder<DefaultDimension> builder)
    {
        builder.ToTable("default_dimension", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.DimensionCode).HasColumnName("dimension_code");
        builder.Property(x => x.DimensionValueCode).HasColumnName("dimension_value_code");
        builder.Property(x => x.ValuePosting).HasColumnName("value_posting");
        builder.Property(x => x.MultiSelectionAction).HasColumnName("multi_selection_action");
    }
}
