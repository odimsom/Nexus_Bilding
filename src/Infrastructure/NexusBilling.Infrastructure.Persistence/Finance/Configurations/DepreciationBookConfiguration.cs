using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class DepreciationBookConfiguration : IEntityTypeConfiguration<DepreciationBook>
{
    public void Configure(EntityTypeBuilder<DepreciationBook> builder)
    {
        builder.ToTable("depreciation_book", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.GLIntegrationAcqCost).HasColumnName("g_l_integration_acq_cost");
        builder.Property(x => x.GLIntegrationDepreciation).HasColumnName("g_l_integration_depreciation");
        builder.Property(x => x.GLIntegrationWriteDown).HasColumnName("g_l_integration_write_down");
        builder.Property(x => x.GLIntegrationAppreciation).HasColumnName("g_l_integration_appreciation");
        builder.Property(x => x.GLIntegrationCustom1).HasColumnName("g_l_integration_custom_1");
        builder.Property(x => x.GLIntegrationCustom2).HasColumnName("g_l_integration_custom_2");
        builder.Property(x => x.GLIntegrationDisposal).HasColumnName("g_l_integration_disposal");
        builder.Property(x => x.GLIntegrationMaintenance).HasColumnName("g_l_integration_maintenance");
        builder.Property(x => x.DisposalCalculationMethod).HasColumnName("disposal_calculation_method");
        builder.Property(x => x.UseCustom1Depreciation).HasColumnName("use_custom_1_depreciation");
        builder.Property(x => x.AllowDeprBelowZero).HasColumnName("allow_depr_below_zero");
        builder.Property(x => x.UseFaExchRateInDuplic).HasColumnName("use_fa_exch_rate_in_duplic");
        builder.Property(x => x.PartOfDuplicationList).HasColumnName("part_of_duplication_list");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.AllowIndexation).HasColumnName("allow_indexation");
        builder.Property(x => x.UseSameFaGLPostingDates).HasColumnName("use_same_fa_g_l_posting_dates");
        builder.Property(x => x.DefaultExchangeRate).HasColumnName("default_exchange_rate").HasPrecision(18, 5);
        builder.Property(x => x.UseFaLedgerCheck).HasColumnName("use_fa_ledger_check");
        builder.Property(x => x.UseRoundingInPeriodicDepr).HasColumnName("use_rounding_in_periodic_depr");
        builder.Property(x => x.NewFiscalYearStartingDate).HasColumnName("new_fiscal_year_starting_date");
        builder.Property(x => x.NoOfDaysInFiscalYear).HasColumnName("no_of_days_in_fiscal_year");
        builder.Property(x => x.AllowChangesInDeprFields).HasColumnName("allow_changes_in_depr_fields");
        builder.Property(x => x.DefaultFinalRoundingAmount).HasColumnName("default_final_rounding_amount").HasPrecision(18, 5);
        builder.Property(x => x.DefaultEndingBookValue).HasColumnName("default_ending_book_value").HasPrecision(18, 5);
        builder.Property(x => x.PeriodicDeprDateCalc).HasColumnName("periodic_depr_date_calc");
        builder.Property(x => x.MarkErrorsAsCorrections).HasColumnName("mark_errors_as_corrections");
        builder.Property(x => x.AddCurrExchRateAcqCost).HasColumnName("add_curr_exch_rate_acq_cost");
        builder.Property(x => x.AddCurrExchRateDepr).HasColumnName("add_curr_exch_rate_depr");
        builder.Property(x => x.AddCurrExchRateWriteDown).HasColumnName("add_curr_exch_rate_write_down");
        builder.Property(x => x.AddCurrExchRateApprec).HasColumnName("add_curr_exch_rate_apprec");
        builder.Property(x => x.AddCurrExchRateCustom1).HasColumnName("add_curr_exch_rate_custom_1");
        builder.Property(x => x.AddCurrExchRateCustom2).HasColumnName("add_curr_exch_rate_custom_2");
        builder.Property(x => x.AddCurrExchRateDisp).HasColumnName("add_curr_exch_rate_disp");
        builder.Property(x => x.AddCurrExchRateMaint).HasColumnName("add_curr_exch_rate_maint");
        builder.Property(x => x.UseDefaultDimension).HasColumnName("use_default_dimension");
        builder.Property(x => x.SubtractDiscInPurchInv).HasColumnName("subtract_disc_in_purch_inv");
        builder.Property(x => x.AllowCorrectionOfDisposal).HasColumnName("allow_correction_of_disposal");
        builder.Property(x => x.AllowMoreThan360365Days).HasColumnName("allow_more_than_360_365_days");
        builder.Property(x => x.VatOnNetDisposalEntries).HasColumnName("vat_on_net_disposal_entries");
        builder.Property(x => x.AllowAcqCostBelowZero).HasColumnName("allow_acq_cost_below_zero");
        builder.Property(x => x.AllowIdenticalDocumentNo).HasColumnName("allow_identical_document_no");
        builder.Property(x => x.FiscalYear365Days).HasColumnName("fiscal_year_365_days");
    }
}
