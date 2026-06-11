using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemAttributeValueMappingConfiguration : IEntityTypeConfiguration<ItemAttributeValueMapping>
{
    public void Configure(EntityTypeBuilder<ItemAttributeValueMapping> builder)
    {
        builder.ToTable("item_attribute_value_mapping", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.ItemAttributeId).HasColumnName("item_attribute_id");
        builder.Property(x => x.ItemAttributeValueId).HasColumnName("item_attribute_value_id");
    }
}
