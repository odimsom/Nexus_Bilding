using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("item", "erp");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.TenantId, b =>
        {
            b.Property(t => t.Value).HasColumnName("tenant_id").IsRequired();
        });

        builder.Property(x => x.No)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.BaseUnitOfMeasure)
            .HasMaxLength(10);

        builder.Property(x => x.UnitPrice)
            .HasPrecision(18, 5);

        builder.Property(x => x.UnitCost)
            .HasPrecision(18, 5);

        builder.Property(x => x.Blocked)
            .IsRequired();
    }
}
