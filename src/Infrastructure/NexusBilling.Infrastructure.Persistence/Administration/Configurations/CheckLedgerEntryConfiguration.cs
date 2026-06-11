using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class CheckLedgerEntryConfiguration : IEntityTypeConfiguration<CheckLedgerEntry>
{
    public void Configure(EntityTypeBuilder<CheckLedgerEntry> builder)
    {
        builder.ToTable("check_ledger_entry", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.BankAccountNo).HasColumnName("bank_account_no");
        builder.Property(x => x.BankAccountLedgerEntryNo).HasColumnName("bank_account_ledger_entry_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.CheckDate).HasColumnName("check_date");
        builder.Property(x => x.CheckNo).HasColumnName("check_no");
        builder.Property(x => x.CheckType).HasColumnName("check_type");
        builder.Property(x => x.BankPaymentType).HasColumnName("bank_payment_type");
        builder.Property(x => x.EntryStatus).HasColumnName("entry_status");
        builder.Property(x => x.OriginalEntryStatus).HasColumnName("original_entry_status");
        builder.Property(x => x.BalAccountType).HasColumnName("bal_account_type");
        builder.Property(x => x.BalAccountNo).HasColumnName("bal_account_no");
        builder.Property(x => x.Open).HasColumnName("open");
        builder.Property(x => x.StatementStatus).HasColumnName("statement_status");
        builder.Property(x => x.StatementNo).HasColumnName("statement_no");
        builder.Property(x => x.StatementLineNo).HasColumnName("statement_line_no");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.DataExchEntryNo).HasColumnName("data_exch_entry_no");
        builder.Property(x => x.DataExchVoidedEntryNo).HasColumnName("data_exch_voided_entry_no");
        builder.Property(x => x.PositivePayExported).HasColumnName("positive_pay_exported");
        builder.Property(x => x.RecordIdToPrint).HasColumnName("record_id_to_print");
    }
}
