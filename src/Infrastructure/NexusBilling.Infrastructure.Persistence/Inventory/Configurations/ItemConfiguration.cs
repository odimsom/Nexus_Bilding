using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("item", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Description2).HasColumnName("description2");
        builder.Property(x => x.BaseUnitOfMeasure).HasColumnName("base_unit_of_measure");
        builder.Property(x => x.UnitPrice).HasColumnName("unit_price");
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.ItemCategoryCode).HasColumnName("item_category_code");
        builder.Property(x => x.InventoryPostingGroup).HasColumnName("inventory_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.VatProdPostingGroup).HasColumnName("vat_prod_posting_group");
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.VendorItemNo).HasColumnName("vendor_item_no");
        builder.Property(x => x.StandardCost).HasColumnName("standard_cost");
        builder.Property(x => x.LastDirectCost).HasColumnName("last_direct_cost");
    }
}
