using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class SalesPriceConfiguration : IEntityTypeConfiguration<SalesPrice>
{
    public void Configure(EntityTypeBuilder<SalesPrice> builder)
    {
        builder.ToTable("sales_price", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.SalesCode).HasColumnName("sales_code");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.UnitPrice).HasColumnName("unit_price").HasPrecision(18, 5);
        builder.Property(x => x.PriceIncludesVat).HasColumnName("price_includes_vat");
        builder.Property(x => x.AllowInvoiceDisc).HasColumnName("allow_invoice_disc");
        builder.Property(x => x.VatBusPostingGrPrice).HasColumnName("vat_bus_posting_gr_price");
        builder.Property(x => x.SalesType).HasColumnName("sales_type");
        builder.Property(x => x.MinimumQuantity).HasColumnName("minimum_quantity").HasPrecision(18, 5);
        builder.Property(x => x.EndingDate).HasColumnName("ending_date");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.AllowLineDisc).HasColumnName("allow_line_disc");
    }
}
