using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class SalesLineDiscountConfiguration : IEntityTypeConfiguration<SalesLineDiscount>
{
    public void Configure(EntityTypeBuilder<SalesLineDiscount> builder)
    {
        builder.ToTable("sales_line_discount", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.SalesCode).HasColumnName("sales_code");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.LineDiscount).HasColumnName("line_discount").HasPrecision(18, 5);
        builder.Property(x => x.SalesType).HasColumnName("sales_type");
        builder.Property(x => x.MinimumQuantity).HasColumnName("minimum_quantity").HasPrecision(18, 5);
        builder.Property(x => x.EndingDate).HasColumnName("ending_date");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
    }
}
