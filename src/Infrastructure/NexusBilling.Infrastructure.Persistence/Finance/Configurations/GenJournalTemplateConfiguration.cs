using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class GenJournalTemplateConfiguration : IEntityTypeConfiguration<GenJournalTemplate>
{
    public void Configure(EntityTypeBuilder<GenJournalTemplate> builder)
    {
        builder.ToTable("gen_journal_template", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.TestReportId).HasColumnName("test_report_id");
        builder.Property(x => x.PageId).HasColumnName("page_id");
        builder.Property(x => x.PostingReportId).HasColumnName("posting_report_id");
        builder.Property(x => x.ForcePostingReport).HasColumnName("force_posting_report");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.Recurring).HasColumnName("recurring");
        builder.Property(x => x.ForceDocBalance).HasColumnName("force_doc_balance");
        builder.Property(x => x.BalAccountType).HasColumnName("bal_account_type");
        builder.Property(x => x.BalAccountNo).HasColumnName("bal_account_no");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.PostingNoSeries).HasColumnName("posting_no_series");
        builder.Property(x => x.CopyVatSetupToJnlLines).HasColumnName("copy_vat_setup_to_jnl_lines");
        builder.Property(x => x.AllowVatDifference).HasColumnName("allow_vat_difference");
        builder.Property(x => x.CustReceiptReportId).HasColumnName("cust_receipt_report_id");
        builder.Property(x => x.VendorReceiptReportId).HasColumnName("vendor_receipt_report_id");
    }
}
