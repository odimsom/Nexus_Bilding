using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class PurchasePriceConfiguration : IEntityTypeConfiguration<PurchasePrice>
{
    public void Configure(EntityTypeBuilder<PurchasePrice> builder)
    {
        builder.ToTable("purchase_price", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.DirectUnitCost).HasColumnName("direct_unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.MinimumQuantity).HasColumnName("minimum_quantity").HasPrecision(18, 5);
        builder.Property(x => x.EndingDate).HasColumnName("ending_date");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
    }
}
