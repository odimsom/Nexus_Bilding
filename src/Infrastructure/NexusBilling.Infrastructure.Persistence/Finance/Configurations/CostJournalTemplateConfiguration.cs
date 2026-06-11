using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CostJournalTemplateConfiguration : IEntityTypeConfiguration<CostJournalTemplate>
{
    public void Configure(EntityTypeBuilder<CostJournalTemplate> builder)
    {
        builder.ToTable("cost_journal_template", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.PostingReportId).HasColumnName("posting_report_id");
    }
}
