using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DimensionValueCombinationConfiguration : IEntityTypeConfiguration<DimensionValueCombination>
{
    public void Configure(EntityTypeBuilder<DimensionValueCombination> builder)
    {
        builder.ToTable("dimension_value_combination", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Dimension1Code).HasColumnName("dimension_1_code");
        builder.Property(x => x.Dimension1ValueCode).HasColumnName("dimension_1_value_code");
        builder.Property(x => x.Dimension2Code).HasColumnName("dimension_2_code");
        builder.Property(x => x.Dimension2ValueCode).HasColumnName("dimension_2_value_code");
    }
}
