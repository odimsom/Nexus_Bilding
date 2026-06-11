using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class AvgCostAdjmtEntryPointConfiguration : IEntityTypeConfiguration<AvgCostAdjmtEntryPoint>
{
    public void Configure(EntityTypeBuilder<AvgCostAdjmtEntryPoint> builder)
    {
        builder.ToTable("avg_cost_adjmt_entry_point", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.ValuationDate).HasColumnName("valuation_date");
        builder.Property(x => x.CostIsAdjusted).HasColumnName("cost_is_adjusted");
    }
}
