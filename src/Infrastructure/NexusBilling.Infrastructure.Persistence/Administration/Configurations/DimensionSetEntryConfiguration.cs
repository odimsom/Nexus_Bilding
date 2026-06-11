using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DimensionSetEntryConfiguration : IEntityTypeConfiguration<DimensionSetEntry>
{
    public void Configure(EntityTypeBuilder<DimensionSetEntry> builder)
    {
        builder.ToTable("dimension_set_entry", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.DimensionCode).HasColumnName("dimension_code");
        builder.Property(x => x.DimensionValueCode).HasColumnName("dimension_value_code");
        builder.Property(x => x.DimensionValueId).HasColumnName("dimension_value_id");
    }
}
