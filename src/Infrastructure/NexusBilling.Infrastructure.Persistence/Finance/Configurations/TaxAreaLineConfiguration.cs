using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class TaxAreaLineConfiguration : IEntityTypeConfiguration<TaxAreaLine>
{
    public void Configure(EntityTypeBuilder<TaxAreaLine> builder)
    {
        builder.ToTable("tax_area_line", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TaxArea).HasColumnName("tax_area");
        builder.Property(x => x.TaxJurisdictionCode).HasColumnName("tax_jurisdiction_code");
        builder.Property(x => x.CalculationOrder).HasColumnName("calculation_order");
    }
}
