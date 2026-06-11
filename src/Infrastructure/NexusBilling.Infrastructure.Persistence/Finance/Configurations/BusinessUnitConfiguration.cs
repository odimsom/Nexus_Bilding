using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class BusinessUnitConfiguration : IEntityTypeConfiguration<BusinessUnit>
{
    public void Configure(EntityTypeBuilder<BusinessUnit> builder)
    {
        builder.ToTable("business_unit", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Consolidate).HasColumnName("consolidate");
        builder.Property(x => x.Consolidation).HasColumnName("consolidation");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.EndingDate).HasColumnName("ending_date");
        builder.Property(x => x.IncomeCurrencyFactor).HasColumnName("income_currency_factor");
        builder.Property(x => x.BalanceCurrencyFactor).HasColumnName("balance_currency_factor");
        builder.Property(x => x.ExchRateLossesAcc).HasColumnName("exch_rate_losses_acc");
        builder.Property(x => x.ExchRateGainsAcc).HasColumnName("exch_rate_gains_acc");
        builder.Property(x => x.ResidualAccount).HasColumnName("residual_account");
        builder.Property(x => x.LastBalanceCurrencyFactor).HasColumnName("last_balance_currency_factor");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.CompanyName).HasColumnName("company_name");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.CompExchRateGainsAcc).HasColumnName("comp_exch_rate_gains_acc");
        builder.Property(x => x.CompExchRateLossesAcc).HasColumnName("comp_exch_rate_losses_acc");
        builder.Property(x => x.EquityExchRateGainsAcc).HasColumnName("equity_exch_rate_gains_acc");
        builder.Property(x => x.EquityExchRateLossesAcc).HasColumnName("equity_exch_rate_losses_acc");
        builder.Property(x => x.MinorityExchRateGainsAcc).HasColumnName("minority_exch_rate_gains_acc");
        builder.Property(x => x.MinorityExchRateLossesAcc).HasColumnName("minority_exch_rate_losses_acc");
        builder.Property(x => x.CurrencyExchangeRateTable).HasColumnName("currency_exchange_rate_table");
        builder.Property(x => x.DataSource).HasColumnName("data_source");
        builder.Property(x => x.FileFormat).HasColumnName("file_format");
    }
}
