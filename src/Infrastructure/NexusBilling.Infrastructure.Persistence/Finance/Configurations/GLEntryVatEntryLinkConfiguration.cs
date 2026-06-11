using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class GLEntryVatEntryLinkConfiguration : IEntityTypeConfiguration<GLEntryVatEntryLink>
{
    public void Configure(EntityTypeBuilder<GLEntryVatEntryLink> builder)
    {
        builder.ToTable("g_l_entry_vat_entry_link", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.GLEntryNo).HasColumnName("g_l_entry_no");
        builder.Property(x => x.VatEntryNo).HasColumnName("vat_entry_no");
    }
}
