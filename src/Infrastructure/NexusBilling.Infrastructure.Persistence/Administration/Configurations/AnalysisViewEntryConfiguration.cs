using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AnalysisViewEntryConfiguration : IEntityTypeConfiguration<AnalysisViewEntry>
{
    public void Configure(EntityTypeBuilder<AnalysisViewEntry> builder)
    {
        builder.ToTable("analysis_view_entry", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AnalysisViewCode).HasColumnName("analysis_view_code");
        builder.Property(x => x.BusinessUnitCode).HasColumnName("business_unit_code");
        builder.Property(x => x.AccountNo).HasColumnName("account_no");
        builder.Property(x => x.Dimension1ValueCode).HasColumnName("dimension_1_value_code");
        builder.Property(x => x.Dimension2ValueCode).HasColumnName("dimension_2_value_code");
        builder.Property(x => x.Dimension3ValueCode).HasColumnName("dimension_3_value_code");
        builder.Property(x => x.Dimension4ValueCode).HasColumnName("dimension_4_value_code");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.DebitAmount).HasColumnName("debit_amount").HasPrecision(18, 5);
        builder.Property(x => x.CreditAmount).HasColumnName("credit_amount").HasPrecision(18, 5);
        builder.Property(x => x.AddCurrAmount).HasColumnName("add_curr_amount").HasPrecision(18, 5);
        builder.Property(x => x.AddCurrDebitAmount).HasColumnName("add_curr_debit_amount").HasPrecision(18, 5);
        builder.Property(x => x.AddCurrCreditAmount).HasColumnName("add_curr_credit_amount").HasPrecision(18, 5);
        builder.Property(x => x.AccountSource).HasColumnName("account_source");
        builder.Property(x => x.CashFlowForecastNo).HasColumnName("cash_flow_forecast_no");
    }
}
