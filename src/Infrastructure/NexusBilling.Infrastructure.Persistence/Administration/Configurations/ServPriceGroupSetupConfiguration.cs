using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServPriceGroupSetupConfiguration : IEntityTypeConfiguration<ServPriceGroupSetup>
{
    public void Configure(EntityTypeBuilder<ServPriceGroupSetup> builder)
    {
        builder.ToTable("serv_price_group_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ServicePriceGroupCode).HasColumnName("service_price_group_code");
        builder.Property(x => x.FaultAreaCode).HasColumnName("fault_area_code");
        builder.Property(x => x.CustPriceGroupCode).HasColumnName("cust_price_group_code");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.ServPriceAdjmtGrCode).HasColumnName("serv_price_adjmt_gr_code");
        builder.Property(x => x.IncludeDiscounts).HasColumnName("include_discounts");
        builder.Property(x => x.AdjustmentType).HasColumnName("adjustment_type");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.IncludeVat).HasColumnName("include_vat");
    }
}
