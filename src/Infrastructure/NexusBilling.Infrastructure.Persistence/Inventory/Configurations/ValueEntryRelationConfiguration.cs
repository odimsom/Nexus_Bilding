using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ValueEntryRelationConfiguration : IEntityTypeConfiguration<ValueEntryRelation>
{
    public void Configure(EntityTypeBuilder<ValueEntryRelation> builder)
    {
        builder.ToTable("value_entry_relation", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ValueEntryNo).HasColumnName("value_entry_no");
        builder.Property(x => x.SourceRowid).HasColumnName("source_rowid");
    }
}
