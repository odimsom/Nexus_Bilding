using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class MyItemConfiguration : IEntityTypeConfiguration<MyItem>
{
    public void Configure(EntityTypeBuilder<MyItem> builder)
    {
        builder.ToTable("my_item", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.UnitPrice).HasColumnName("unit_price").HasPrecision(18, 5);
    }
}
