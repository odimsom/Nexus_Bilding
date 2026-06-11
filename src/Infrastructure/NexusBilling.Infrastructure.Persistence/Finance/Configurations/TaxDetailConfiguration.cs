using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class TaxDetailConfiguration : IEntityTypeConfiguration<TaxDetail>
{
    public void Configure(EntityTypeBuilder<TaxDetail> builder)
    {
        builder.ToTable("tax_detail", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TaxJurisdictionCode).HasColumnName("tax_jurisdiction_code");
        builder.Property(x => x.TaxGroupCode).HasColumnName("tax_group_code");
        builder.Property(x => x.TaxType).HasColumnName("tax_type");
        builder.Property(x => x.MaximumAmountQty).HasColumnName("maximum_amount_qty").HasPrecision(18, 5);
        builder.Property(x => x.TaxBelowMaximum).HasColumnName("tax_below_maximum").HasPrecision(18, 5);
        builder.Property(x => x.TaxAboveMaximum).HasColumnName("tax_above_maximum").HasPrecision(18, 5);
        builder.Property(x => x.EffectiveDate).HasColumnName("effective_date");
        builder.Property(x => x.CalculateTaxOnTax).HasColumnName("calculate_tax_on_tax");
    }
}
