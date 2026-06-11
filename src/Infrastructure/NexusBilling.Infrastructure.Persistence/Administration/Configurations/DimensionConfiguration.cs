using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DimensionConfiguration : IEntityTypeConfiguration<Dimension>
{
    public void Configure(EntityTypeBuilder<Dimension> builder)
    {
        builder.ToTable("dimension", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.CodeCaption).HasColumnName("code_caption");
        builder.Property(x => x.FilterCaption).HasColumnName("filter_caption");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.ConsolidationCode).HasColumnName("consolidation_code");
        builder.Property(x => x.MapToIcDimensionCode).HasColumnName("map_to_ic_dimension_code");
    }
}
