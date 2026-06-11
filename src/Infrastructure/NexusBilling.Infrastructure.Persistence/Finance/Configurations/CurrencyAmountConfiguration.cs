using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CurrencyAmountConfiguration : IEntityTypeConfiguration<CurrencyAmount>
{
    public void Configure(EntityTypeBuilder<CurrencyAmount> builder)
    {
        builder.ToTable("currency_amount", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
    }
}
