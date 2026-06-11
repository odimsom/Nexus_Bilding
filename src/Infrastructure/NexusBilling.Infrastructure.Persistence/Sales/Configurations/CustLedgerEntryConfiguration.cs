using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class CustLedgerEntryConfiguration : IEntityTypeConfiguration<CustLedgerEntry>
{
    public void Configure(EntityTypeBuilder<CustLedgerEntry> builder)
    {
        builder.ToTable("cust_ledger_entry", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.CustomerNo).HasColumnName("customer_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.SalesLcy).HasColumnName("sales_lcy");
        builder.Property(x => x.ProfitLcy).HasColumnName("profit_lcy");
        builder.Property(x => x.InvDiscountLcy).HasColumnName("inv_discount_lcy");
        builder.Property(x => x.SellToCustomerNo).HasColumnName("sell_to_customer_no");
        builder.Property(x => x.CustomerPostingGroup).HasColumnName("customer_posting_group");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension2_code");
        builder.Property(x => x.SalespersonCode).HasColumnName("salesperson_code");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.OnHold).HasColumnName("on_hold");
        builder.Property(x => x.AppliesToDocType).HasColumnName("applies_to_doc_type");
        builder.Property(x => x.AppliesToDocNo).HasColumnName("applies_to_doc_no");
        builder.Property(x => x.Open).HasColumnName("open");
        builder.Property(x => x.DueDate).HasColumnName("due_date");
        builder.Property(x => x.PmtDiscountDate).HasColumnName("pmt_discount_date");
        builder.Property(x => x.OriginalPmtDiscPossible).HasColumnName("original_pmt_disc_possible");
        builder.Property(x => x.PmtDiscGivenLcy).HasColumnName("pmt_disc_given_lcy");
        builder.Property(x => x.Positive).HasColumnName("positive");
        builder.Property(x => x.ClosedByEntryNo).HasColumnName("closed_by_entry_no");
        builder.Property(x => x.ClosedAtDate).HasColumnName("closed_at_date");
        builder.Property(x => x.ClosedByAmount).HasColumnName("closed_by_amount");
        builder.Property(x => x.AppliesToId).HasColumnName("applies_to_id");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.BalAccountType).HasColumnName("bal_account_type");
        builder.Property(x => x.BalAccountNo).HasColumnName("bal_account_no");
        builder.Property(x => x.TransactionNo).HasColumnName("transaction_no");
        builder.Property(x => x.ClosedByAmountLcy).HasColumnName("closed_by_amount_lcy");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.CalculateInterest).HasColumnName("calculate_interest");
        builder.Property(x => x.ClosingInterestCalculated).HasColumnName("closing_interest_calculated");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.ClosedByCurrencyCode).HasColumnName("closed_by_currency_code");
        builder.Property(x => x.ClosedByCurrencyAmount).HasColumnName("closed_by_currency_amount");
        builder.Property(x => x.AdjustedCurrencyFactor).HasColumnName("adjusted_currency_factor");
        builder.Property(x => x.OriginalCurrencyFactor).HasColumnName("original_currency_factor");
        builder.Property(x => x.RemainingPmtDiscPossible).HasColumnName("remaining_pmt_disc_possible");
        builder.Property(x => x.PmtDiscToleranceDate).HasColumnName("pmt_disc_tolerance_date");
        builder.Property(x => x.MaxPaymentTolerance).HasColumnName("max_payment_tolerance");
        builder.Property(x => x.LastIssuedReminderLevel).HasColumnName("last_issued_reminder_level");
        builder.Property(x => x.AcceptedPaymentTolerance).HasColumnName("accepted_payment_tolerance");
        builder.Property(x => x.AcceptedPmtDiscTolerance).HasColumnName("accepted_pmt_disc_tolerance");
        builder.Property(x => x.PmtToleranceLcy).HasColumnName("pmt_tolerance_lcy");
        builder.Property(x => x.AmountToApply).HasColumnName("amount_to_apply");
        builder.Property(x => x.IcPartnerCode).HasColumnName("ic_partner_code");
        builder.Property(x => x.ApplyingEntry).HasColumnName("applying_entry");
        builder.Property(x => x.Reversed).HasColumnName("reversed");
        builder.Property(x => x.ReversedByEntryNo).HasColumnName("reversed_by_entry_no");
        builder.Property(x => x.ReversedEntryNo).HasColumnName("reversed_entry_no");
        builder.Property(x => x.Prepayment).HasColumnName("prepayment");
        builder.Property(x => x.PaymentMethodCode).HasColumnName("payment_method_code");
        builder.Property(x => x.AppliesToExtDocNo).HasColumnName("applies_to_ext_doc_no");
        builder.Property(x => x.RecipientBankAccount).HasColumnName("recipient_bank_account");
        builder.Property(x => x.MessageToRecipient).HasColumnName("message_to_recipient");
        builder.Property(x => x.ExportedToPaymentFile).HasColumnName("exported_to_payment_file");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.DirectDebitMandateId).HasColumnName("direct_debit_mandate_id");
    }
}
