using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class BankAccountLedgerEntryConfiguration : IEntityTypeConfiguration<BankAccountLedgerEntry>
{
    public void Configure(EntityTypeBuilder<BankAccountLedgerEntry> builder)
    {
        builder.ToTable("bank_account_ledger_entry", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.BankAccountNo).HasColumnName("bank_account_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.RemainingAmount).HasColumnName("remaining_amount").HasPrecision(18, 5);
        builder.Property(x => x.AmountLcy).HasColumnName("amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.BankAccPostingGroup).HasColumnName("bank_acc_posting_group");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.OurContactCode).HasColumnName("our_contact_code");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.Open).HasColumnName("open");
        builder.Property(x => x.Positive).HasColumnName("positive");
        builder.Property(x => x.ClosedByEntryNo).HasColumnName("closed_by_entry_no");
        builder.Property(x => x.ClosedAtDate).HasColumnName("closed_at_date");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.BalAccountType).HasColumnName("bal_account_type");
        builder.Property(x => x.BalAccountNo).HasColumnName("bal_account_no");
        builder.Property(x => x.TransactionNo).HasColumnName("transaction_no");
        builder.Property(x => x.StatementStatus).HasColumnName("statement_status");
        builder.Property(x => x.StatementNo).HasColumnName("statement_no");
        builder.Property(x => x.StatementLineNo).HasColumnName("statement_line_no");
        builder.Property(x => x.DebitAmount).HasColumnName("debit_amount").HasPrecision(18, 5);
        builder.Property(x => x.CreditAmount).HasColumnName("credit_amount").HasPrecision(18, 5);
        builder.Property(x => x.DebitAmountLcy).HasColumnName("debit_amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.CreditAmountLcy).HasColumnName("credit_amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.Reversed).HasColumnName("reversed");
        builder.Property(x => x.ReversedByEntryNo).HasColumnName("reversed_by_entry_no");
        builder.Property(x => x.ReversedEntryNo).HasColumnName("reversed_entry_no");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
    }
}
