using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaReclassJournalTemplateConfiguration : IEntityTypeConfiguration<FaReclassJournalTemplate>
{
    public void Configure(EntityTypeBuilder<FaReclassJournalTemplate> builder)
    {
        builder.ToTable("fa_reclass_journal_template", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.PageId).HasColumnName("page_id");
    }
}
