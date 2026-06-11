using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DimensionSetTreeNodeConfiguration : IEntityTypeConfiguration<DimensionSetTreeNode>
{
    public void Configure(EntityTypeBuilder<DimensionSetTreeNode> builder)
    {
        builder.ToTable("dimension_set_tree_node", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ParentDimensionSetId).HasColumnName("parent_dimension_set_id");
        builder.Property(x => x.DimensionValueId).HasColumnName("dimension_value_id");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.InUse).HasColumnName("in_use");
    }
}
