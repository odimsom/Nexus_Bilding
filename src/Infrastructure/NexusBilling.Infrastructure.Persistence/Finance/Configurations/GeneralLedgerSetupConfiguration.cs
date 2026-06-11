using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class GeneralLedgerSetupConfiguration : IEntityTypeConfiguration<GeneralLedgerSetup>
{
    public void Configure(EntityTypeBuilder<GeneralLedgerSetup> builder)
    {
        builder.ToTable("general_ledger_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.AllowPostingFrom).HasColumnName("allow_posting_from");
        builder.Property(x => x.AllowPostingTo).HasColumnName("allow_posting_to");
        builder.Property(x => x.RegisterTime).HasColumnName("register_time");
        builder.Property(x => x.PmtDiscExclVat).HasColumnName("pmt_disc_excl_vat");
        builder.Property(x => x.UnrealizedVat).HasColumnName("unrealized_vat");
        builder.Property(x => x.AdjustForPaymentDisc).HasColumnName("adjust_for_payment_disc");
        builder.Property(x => x.MarkCrMemosAsCorrections).HasColumnName("mark_cr_memos_as_corrections");
        builder.Property(x => x.LocalAddressFormat).HasColumnName("local_address_format");
        builder.Property(x => x.InvRoundingPrecisionLcy).HasColumnName("inv_rounding_precision_lcy");
        builder.Property(x => x.InvRoundingTypeLcy).HasColumnName("inv_rounding_type_lcy");
        builder.Property(x => x.LocalContAddrFormat).HasColumnName("local_cont_addr_format");
        builder.Property(x => x.BankAccountNos).HasColumnName("bank_account_nos");
        builder.Property(x => x.SummarizeGLEntries).HasColumnName("summarize_gl_entries");
        builder.Property(x => x.AmountDecimalPlaces).HasColumnName("amount_decimal_places");
        builder.Property(x => x.UnitAmountDecimalPlaces).HasColumnName("unit_amount_decimal_places");
        builder.Property(x => x.AdditionalReportingCurrency).HasColumnName("additional_reporting_currency");
        builder.Property(x => x.VatTolerance).HasColumnName("vat_tolerance");
        builder.Property(x => x.EmuCurrency).HasColumnName("emu_currency");
        builder.Property(x => x.LcyCode).HasColumnName("lcy_code");
        builder.Property(x => x.VatExchangeRateAdjustment).HasColumnName("vat_exchange_rate_adjustment");
        builder.Property(x => x.AmountRoundingPrecision).HasColumnName("amount_rounding_precision");
        builder.Property(x => x.UnitAmountRoundingPrecision).HasColumnName("unit_amount_rounding_precision");
        builder.Property(x => x.ApplnRoundingPrecision).HasColumnName("appln_rounding_precision");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension2_code");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension2_code");
        builder.Property(x => x.ShortcutDimension3Code).HasColumnName("shortcut_dimension3_code");
        builder.Property(x => x.ShortcutDimension4Code).HasColumnName("shortcut_dimension4_code");
        builder.Property(x => x.ShortcutDimension5Code).HasColumnName("shortcut_dimension5_code");
        builder.Property(x => x.ShortcutDimension6Code).HasColumnName("shortcut_dimension6_code");
        builder.Property(x => x.ShortcutDimension7Code).HasColumnName("shortcut_dimension7_code");
        builder.Property(x => x.ShortcutDimension8Code).HasColumnName("shortcut_dimension8_code");
        builder.Property(x => x.MaxVatDifferenceAllowed).HasColumnName("max_vat_difference_allowed");
        builder.Property(x => x.VatRoundingType).HasColumnName("vat_rounding_type");
        builder.Property(x => x.PmtDiscTolerancePosting).HasColumnName("pmt_disc_tolerance_posting");
        builder.Property(x => x.PaymentDiscountGracePeriod).HasColumnName("payment_discount_grace_period");
        builder.Property(x => x.PaymentTolerance).HasColumnName("payment_tolerance");
        builder.Property(x => x.MaxPaymentToleranceAmount).HasColumnName("max_payment_tolerance_amount");
        builder.Property(x => x.AdaptMainMenuToPermissions).HasColumnName("adapt_main_menu_to_permissions");
        builder.Property(x => x.AllowGLAccDeletionBefore).HasColumnName("allow_gl_acc_deletion_before");
        builder.Property(x => x.CheckGLAccountUsage).HasColumnName("check_gl_account_usage");
        builder.Property(x => x.PaymentTolerancePosting).HasColumnName("payment_tolerance_posting");
        builder.Property(x => x.PmtDiscToleranceWarning).HasColumnName("pmt_disc_tolerance_warning");
        builder.Property(x => x.PaymentToleranceWarning).HasColumnName("payment_tolerance_warning");
        builder.Property(x => x.LastIcTransactionNo).HasColumnName("last_ic_transaction_no");
        builder.Property(x => x.BillToSellToVatCalc).HasColumnName("bill_to_sell_to_vat_calc");
        builder.Property(x => x.AccSchedForBalanceSheet).HasColumnName("acc_sched_for_balance_sheet");
        builder.Property(x => x.AccSchedForIncomeStmt).HasColumnName("acc_sched_for_income_stmt");
        builder.Property(x => x.AccSchedForCashFlowStmt).HasColumnName("acc_sched_for_cash_flow_stmt");
        builder.Property(x => x.AccSchedForRetainedEarn).HasColumnName("acc_sched_for_retained_earn");
        builder.Property(x => x.PrintVatSpecificationInLcy).HasColumnName("print_vat_specification_in_lcy");
        builder.Property(x => x.PrepaymentUnrealizedVat).HasColumnName("prepayment_unrealized_vat");
        builder.Property(x => x.UseLegacyGLEntryLocking).HasColumnName("use_legacy_gl_entry_locking");
        builder.Property(x => x.PayrollTransImportFormat).HasColumnName("payroll_trans_import_format");
        builder.Property(x => x.VatRegNoValidationUrl).HasColumnName("vat_reg_no_validation_url");
        builder.Property(x => x.LocalCurrencySymbol).HasColumnName("local_currency_symbol");
    }
}
