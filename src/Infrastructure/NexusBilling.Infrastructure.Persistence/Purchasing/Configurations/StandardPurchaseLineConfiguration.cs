using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class StandardPurchaseLineConfiguration : IEntityTypeConfiguration<StandardPurchaseLine>
{
    public void Configure(EntityTypeBuilder<StandardPurchaseLine> builder)
    {
        builder.ToTable("standard_purchase_line", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.StandardPurchaseCode).HasColumnName("standard_purchase_code");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.AmountExclVat).HasColumnName("amount_excl_vat").HasPrecision(18, 5);
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension_1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension_2_code");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
    }
}
