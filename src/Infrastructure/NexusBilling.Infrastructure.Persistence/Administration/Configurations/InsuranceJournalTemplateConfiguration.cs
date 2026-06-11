using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class InsuranceJournalTemplateConfiguration : IEntityTypeConfiguration<InsuranceJournalTemplate>
{
    public void Configure(EntityTypeBuilder<InsuranceJournalTemplate> builder)
    {
        builder.ToTable("insurance_journal_template", "administration");
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
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.PostingNoSeries).HasColumnName("posting_no_series");
    }
}
