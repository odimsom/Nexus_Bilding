using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class PostValueEntryToGLConfiguration : IEntityTypeConfiguration<PostValueEntryToGL>
{
    public void Configure(EntityTypeBuilder<PostValueEntryToGL> builder)
    {
        builder.ToTable("post_value_entry_to_g_l", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ValueEntryNo).HasColumnName("value_entry_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
    }
}
