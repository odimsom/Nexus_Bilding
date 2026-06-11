using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemVendorConfiguration : IEntityTypeConfiguration<ItemVendor>
{
    public void Configure(EntityTypeBuilder<ItemVendor> builder)
    {
        builder.ToTable("item_vendor", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.LeadTimeCalculation).HasColumnName("lead_time_calculation");
        builder.Property(x => x.VendorItemNo).HasColumnName("vendor_item_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
    }
}
