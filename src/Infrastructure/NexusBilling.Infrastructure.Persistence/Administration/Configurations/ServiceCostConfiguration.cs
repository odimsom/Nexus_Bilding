using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceCostConfiguration : IEntityTypeConfiguration<ServiceCost>
{
    public void Configure(EntityTypeBuilder<ServiceCost> builder)
    {
        builder.ToTable("service_cost", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.AccountNo).HasColumnName("account_no");
        builder.Property(x => x.DefaultUnitPrice).HasColumnName("default_unit_price").HasPrecision(18, 5);
        builder.Property(x => x.DefaultQuantity).HasColumnName("default_quantity").HasPrecision(18, 5);
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.CostType).HasColumnName("cost_type");
        builder.Property(x => x.ServiceZoneCode).HasColumnName("service_zone_code");
        builder.Property(x => x.DefaultUnitCost).HasColumnName("default_unit_cost").HasPrecision(18, 5);
    }
}
