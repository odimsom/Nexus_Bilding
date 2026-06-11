using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemAttributeValueConfiguration : IEntityTypeConfiguration<ItemAttributeValue>
{
    public void Configure(EntityTypeBuilder<ItemAttributeValue> builder)
    {
        builder.ToTable("item_attribute_value", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AttributeId).HasColumnName("attribute_id");
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.Value).HasColumnName("value");
        builder.Property(x => x.NumericValue).HasColumnName("numeric_value").HasPrecision(18, 5);
        builder.Property(x => x.Blocked).HasColumnName("blocked");
    }
}
