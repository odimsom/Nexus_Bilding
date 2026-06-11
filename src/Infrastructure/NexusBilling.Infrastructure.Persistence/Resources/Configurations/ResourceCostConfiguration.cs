using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class ResourceCostConfiguration : IEntityTypeConfiguration<ResourceCost>
{
    public void Configure(EntityTypeBuilder<ResourceCost> builder)
    {
        builder.ToTable("resource_cost", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.WorkTypeCode).HasColumnName("work_type_code");
        builder.Property(x => x.CostType).HasColumnName("cost_type");
        builder.Property(x => x.DirectUnitCost).HasColumnName("direct_unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
    }
}
