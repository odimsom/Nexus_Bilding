using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaJournalTemplateConfiguration : IEntityTypeConfiguration<FaJournalTemplate>
{
    public void Configure(EntityTypeBuilder<FaJournalTemplate> builder)
    {
        builder.ToTable("fa_journal_template", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.TestReportId).HasColumnName("test_report_id");
        builder.Property(x => x.PageId).HasColumnName("page_id");
        builder.Property(x => x.PostingReportId).HasColumnName("posting_report_id");
        builder.Property(x => x.ForcePostingReport).HasColumnName("force_posting_report");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.Recurring).HasColumnName("recurring");
        builder.Property(x => x.MaintPostingReportId).HasColumnName("maint_posting_report_id");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.PostingNoSeries).HasColumnName("posting_no_series");
    }
}
