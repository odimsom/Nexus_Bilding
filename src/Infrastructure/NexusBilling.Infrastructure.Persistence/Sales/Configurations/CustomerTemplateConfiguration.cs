using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class CustomerTemplateConfiguration : IEntityTypeConfiguration<CustomerTemplate>
{
    public void Configure(EntityTypeBuilder<CustomerTemplate> builder)
    {
        builder.ToTable("customer_template", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.TerritoryCode).HasColumnName("territory_code");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.CustomerPostingGroup).HasColumnName("customer_posting_group");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.CustomerPriceGroup).HasColumnName("customer_price_group");
        builder.Property(x => x.PaymentTermsCode).HasColumnName("payment_terms_code");
        builder.Property(x => x.ShipmentMethodCode).HasColumnName("shipment_method_code");
        builder.Property(x => x.InvoiceDiscCode).HasColumnName("invoice_disc_code");
        builder.Property(x => x.CustomerDiscGroup).HasColumnName("customer_disc_group");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.PaymentMethodCode).HasColumnName("payment_method_code");
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.VatBusPostingGroup).HasColumnName("vat_bus_posting_group");
        builder.Property(x => x.AllowLineDisc).HasColumnName("allow_line_disc");
    }
}
