using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaReclassJournalBatchConfiguration : IEntityTypeConfiguration<FaReclassJournalBatch>
{
    public void Configure(EntityTypeBuilder<FaReclassJournalBatch> builder)
    {
        builder.ToTable("fa_reclass_journal_batch", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
    }
}
