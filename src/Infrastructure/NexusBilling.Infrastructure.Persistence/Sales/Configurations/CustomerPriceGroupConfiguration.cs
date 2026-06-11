using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class CustomerPriceGroupConfiguration : IEntityTypeConfiguration<CustomerPriceGroup>
{
    public void Configure(EntityTypeBuilder<CustomerPriceGroup> builder)
    {
        builder.ToTable("customer_price_group", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.PriceIncludesVat).HasColumnName("price_includes_vat");
        builder.Property(x => x.AllowInvoiceDisc).HasColumnName("allow_invoice_disc");
        builder.Property(x => x.VatBusPostingGrPrice).HasColumnName("vat_bus_posting_gr_price");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.AllowLineDisc).HasColumnName("allow_line_disc");
    }
}
