using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DimensionValueConfiguration : IEntityTypeConfiguration<DimensionValue>
{
    public void Configure(EntityTypeBuilder<DimensionValue> builder)
    {
        builder.ToTable("dimension_value", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DimensionCode).HasColumnName("dimension_code");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.DimensionValueType).HasColumnName("dimension_value_type");
        builder.Property(x => x.Totaling).HasColumnName("totaling");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.ConsolidationCode).HasColumnName("consolidation_code");
        builder.Property(x => x.Indentation).HasColumnName("indentation");
        builder.Property(x => x.GlobalDimensionNo).HasColumnName("global_dimension_no");
        builder.Property(x => x.MapToIcDimensionCode).HasColumnName("map_to_ic_dimension_code");
        builder.Property(x => x.MapToIcDimensionValueCode).HasColumnName("map_to_ic_dimension_value_code");
        builder.Property(x => x.DimensionValueId).HasColumnName("dimension_value_id");
    }
}
