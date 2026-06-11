using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class ExchRateAdjmtRegConfiguration : IEntityTypeConfiguration<ExchRateAdjmtReg>
{
    public void Configure(EntityTypeBuilder<ExchRateAdjmtReg> builder)
    {
        builder.ToTable("exch_rate_adjmt_reg", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.CreationDate).HasColumnName("creation_date");
        builder.Property(x => x.AccountType).HasColumnName("account_type");
        builder.Property(x => x.PostingGroup).HasColumnName("posting_group");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.CurrencyFactor).HasColumnName("currency_factor").HasPrecision(18, 5);
        builder.Property(x => x.AdjustedBase).HasColumnName("adjusted_base").HasPrecision(18, 5);
        builder.Property(x => x.AdjustedBaseLcy).HasColumnName("adjusted_base_lcy").HasPrecision(18, 5);
        builder.Property(x => x.AdjustedAmtLcy).HasColumnName("adjusted_amt_lcy").HasPrecision(18, 5);
        builder.Property(x => x.AdjustedBaseAddCurr).HasColumnName("adjusted_base_add_curr").HasPrecision(18, 5);
        builder.Property(x => x.AdjustedAmtAddCurr).HasColumnName("adjusted_amt_add_curr").HasPrecision(18, 5);
    }
}
