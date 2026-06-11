using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemUnitOfMeasureConfiguration : IEntityTypeConfiguration<ItemUnitOfMeasure>
{
    public void Configure(EntityTypeBuilder<ItemUnitOfMeasure> builder)
    {
        builder.ToTable("item_unit_of_measure", "erp");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();

        builder.Property(x => x.ItemNo).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(10);
        builder.Property(x => x.QtyPerUnitOfMeasure).HasPrecision(18, 5);

        builder.HasIndex(x => new { x.TenantId, x.ItemNo, x.Code }).IsUnique();
    }
}
