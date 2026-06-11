using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DimensionCombinationConfiguration : IEntityTypeConfiguration<DimensionCombination>
{
    public void Configure(EntityTypeBuilder<DimensionCombination> builder)
    {
        builder.ToTable("dimension_combination", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Dimension1Code).HasColumnName("dimension_1_code");
        builder.Property(x => x.Dimension2Code).HasColumnName("dimension_2_code");
        builder.Property(x => x.CombinationRestriction).HasColumnName("combination_restriction");
    }
}
