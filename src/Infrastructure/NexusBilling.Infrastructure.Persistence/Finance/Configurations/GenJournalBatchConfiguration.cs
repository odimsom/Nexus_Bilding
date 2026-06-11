using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class GenJournalBatchConfiguration : IEntityTypeConfiguration<GenJournalBatch>
{
    public void Configure(EntityTypeBuilder<GenJournalBatch> builder)
    {
        builder.ToTable("gen_journal_batch", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.BalAccountType).HasColumnName("bal_account_type");
        builder.Property(x => x.BalAccountNo).HasColumnName("bal_account_no");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.PostingNoSeries).HasColumnName("posting_no_series");
        builder.Property(x => x.CopyVatSetupToJnlLines).HasColumnName("copy_vat_setup_to_jnl_lines");
        builder.Property(x => x.AllowVatDifference).HasColumnName("allow_vat_difference");
        builder.Property(x => x.AllowPaymentExport).HasColumnName("allow_payment_export");
        builder.Property(x => x.BankStatementImportFormat).HasColumnName("bank_statement_import_format");
        builder.Property(x => x.SuggestBalancingAmount).HasColumnName("suggest_balancing_amount");
    }
}
