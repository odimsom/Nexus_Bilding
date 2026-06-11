using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class SalesCueConfiguration : IEntityTypeConfiguration<SalesCue>
{
    public void Configure(EntityTypeBuilder<SalesCue> builder)
    {
        builder.ToTable("sales_cue", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.AverageDaysDelayed).HasColumnName("average_days_delayed").HasPrecision(18, 5);
    }
}
