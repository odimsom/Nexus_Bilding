using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaJournalBatchConfiguration : IEntityTypeConfiguration<FaJournalBatch>
{
    public void Configure(EntityTypeBuilder<FaJournalBatch> builder)
    {
        builder.ToTable("fa_journal_batch", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.PostingNoSeries).HasColumnName("posting_no_series");
    }
}
