using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class SalesCycleConfiguration : IEntityTypeConfiguration<SalesCycle>
{
    public void Configure(EntityTypeBuilder<SalesCycle> builder)
    {
        builder.ToTable("sales_cycle", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ProbabilityCalculation).HasColumnName("probability_calculation");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
    }
}
