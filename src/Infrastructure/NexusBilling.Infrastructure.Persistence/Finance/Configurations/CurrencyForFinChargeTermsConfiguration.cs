using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CurrencyForFinChargeTermsConfiguration : IEntityTypeConfiguration<CurrencyForFinChargeTerms>
{
    public void Configure(EntityTypeBuilder<CurrencyForFinChargeTerms> builder)
    {
        builder.ToTable("currency_for_fin_charge_terms", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.FinChargeTermsCode).HasColumnName("fin_charge_terms_code");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.AdditionalFee).HasColumnName("additional_fee").HasPrecision(18, 5);
    }
}
