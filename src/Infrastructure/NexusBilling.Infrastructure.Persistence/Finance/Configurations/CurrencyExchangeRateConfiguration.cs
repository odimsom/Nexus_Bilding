using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CurrencyExchangeRateConfiguration : IEntityTypeConfiguration<CurrencyExchangeRate>
{
    public void Configure(EntityTypeBuilder<CurrencyExchangeRate> builder)
    {
        builder.ToTable("currency_exchange_rate", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.ExchangeRateAmount).HasColumnName("exchange_rate_amount").HasPrecision(18, 5);
        builder.Property(x => x.AdjustmentExchRateAmount).HasColumnName("adjustment_exch_rate_amount").HasPrecision(18, 5);
        builder.Property(x => x.RelationalCurrencyCode).HasColumnName("relational_currency_code");
        builder.Property(x => x.RelationalExchRateAmount).HasColumnName("relational_exch_rate_amount").HasPrecision(18, 5);
        builder.Property(x => x.FixExchangeRateAmount).HasColumnName("fix_exchange_rate_amount");
        builder.Property(x => x.RelationalAdjmtExchRateAmt).HasColumnName("relational_adjmt_exch_rate_amt").HasPrecision(18, 5);
    }
}
