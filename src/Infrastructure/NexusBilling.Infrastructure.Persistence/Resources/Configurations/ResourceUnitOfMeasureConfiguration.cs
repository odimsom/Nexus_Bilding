using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class ResourceUnitOfMeasureConfiguration : IEntityTypeConfiguration<ResourceUnitOfMeasure>
{
    public void Configure(EntityTypeBuilder<ResourceUnitOfMeasure> builder)
    {
        builder.ToTable("resource_unit_of_measure", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ResourceNo).HasColumnName("resource_no");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.RelatedToBaseUnitOfMeas).HasColumnName("related_to_base_unit_of_meas");
    }
}
