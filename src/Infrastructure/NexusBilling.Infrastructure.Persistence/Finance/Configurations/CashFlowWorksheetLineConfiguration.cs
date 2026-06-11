using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CashFlowWorksheetLineConfiguration : IEntityTypeConfiguration<CashFlowWorksheetLine>
{
    public void Configure(EntityTypeBuilder<CashFlowWorksheetLine> builder)
    {
        builder.ToTable("cash_flow_worksheet_line", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.CashFlowForecastNo).HasColumnName("cash_flow_forecast_no");
        builder.Property(x => x.CashFlowDate).HasColumnName("cash_flow_date");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.CashFlowAccountNo).HasColumnName("cash_flow_account_no");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.PmtDiscountDate).HasColumnName("pmt_discount_date");
        builder.Property(x => x.PmtDiscToleranceDate).HasColumnName("pmt_disc_tolerance_date");
        builder.Property(x => x.PaymentTermsCode).HasColumnName("payment_terms_code");
        builder.Property(x => x.PaymentDiscount).HasColumnName("payment_discount").HasPrecision(18, 5);
        builder.Property(x => x.AssociatedEntryNo).HasColumnName("associated_entry_no");
        builder.Property(x => x.Overdue).HasColumnName("overdue");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension_2_code");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension_1_code");
        builder.Property(x => x.AmountLcy).HasColumnName("amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.SourceNo).HasColumnName("source_no");
        builder.Property(x => x.GLBudgetName).HasColumnName("g_l_budget_name");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
    }
}
