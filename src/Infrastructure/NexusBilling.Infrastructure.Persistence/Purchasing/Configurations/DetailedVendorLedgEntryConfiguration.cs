using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class DetailedVendorLedgEntryConfiguration : IEntityTypeConfiguration<DetailedVendorLedgEntry>
{
    public void Configure(EntityTypeBuilder<DetailedVendorLedgEntry> builder)
    {
        builder.ToTable("detailed_vendor_ledg_entry", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.VendorLedgerEntryNo).HasColumnName("vendor_ledger_entry_no");
        builder.Property(x => x.EntryType).HasColumnName("entry_type");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.AmountLcy).HasColumnName("amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.TransactionNo).HasColumnName("transaction_no");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.DebitAmount).HasColumnName("debit_amount").HasPrecision(18, 5);
        builder.Property(x => x.CreditAmount).HasColumnName("credit_amount").HasPrecision(18, 5);
        builder.Property(x => x.DebitAmountLcy).HasColumnName("debit_amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.CreditAmountLcy).HasColumnName("credit_amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.InitialEntryDueDate).HasColumnName("initial_entry_due_date");
        builder.Property(x => x.InitialEntryGlobalDim1).HasColumnName("initial_entry_global_dim_1");
        builder.Property(x => x.InitialEntryGlobalDim2).HasColumnName("initial_entry_global_dim_2");
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.UseTax).HasColumnName("use_tax");
        builder.Property(x => x.VatBusPostingGroup).HasColumnName("vat_bus_posting_group");
        builder.Property(x => x.VatProdPostingGroup).HasColumnName("vat_prod_posting_group");
        builder.Property(x => x.InitialDocumentType).HasColumnName("initial_document_type");
        builder.Property(x => x.AppliedVendLedgerEntryNo).HasColumnName("applied_vend_ledger_entry_no");
        builder.Property(x => x.Unapplied).HasColumnName("unapplied");
        builder.Property(x => x.UnappliedByEntryNo).HasColumnName("unapplied_by_entry_no");
        builder.Property(x => x.RemainingPmtDiscPossible).HasColumnName("remaining_pmt_disc_possible").HasPrecision(18, 5);
        builder.Property(x => x.MaxPaymentTolerance).HasColumnName("max_payment_tolerance").HasPrecision(18, 5);
        builder.Property(x => x.TaxJurisdictionCode).HasColumnName("tax_jurisdiction_code");
        builder.Property(x => x.ApplicationNo).HasColumnName("application_no");
        builder.Property(x => x.LedgerEntryAmount).HasColumnName("ledger_entry_amount");
    }
}
