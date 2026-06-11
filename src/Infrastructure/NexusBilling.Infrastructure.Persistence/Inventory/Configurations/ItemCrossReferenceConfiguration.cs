using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemCrossReferenceConfiguration : IEntityTypeConfiguration<ItemCrossReference>
{
    public void Configure(EntityTypeBuilder<ItemCrossReference> builder)
    {
        builder.ToTable("item_cross_reference", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.UnitOfMeasure).HasColumnName("unit_of_measure");
        builder.Property(x => x.CrossReferenceType).HasColumnName("cross_reference_type");
        builder.Property(x => x.CrossReferenceTypeNo).HasColumnName("cross_reference_type_no");
        builder.Property(x => x.CrossReferenceNo).HasColumnName("cross_reference_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.DiscontinueBarCode).HasColumnName("discontinue_bar_code");
    }
}
