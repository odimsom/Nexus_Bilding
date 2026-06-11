using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class IcDimensionConfiguration : IEntityTypeConfiguration<IcDimension>
{
    public void Configure(EntityTypeBuilder<IcDimension> builder)
    {
        builder.ToTable("ic_dimension", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.MapToDimensionCode).HasColumnName("map_to_dimension_code");
    }
}
