using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class GLItemLedgerRelationConfiguration : IEntityTypeConfiguration<GLItemLedgerRelation>
{
    public void Configure(EntityTypeBuilder<GLItemLedgerRelation> builder)
    {
        builder.ToTable("g_l_item_ledger_relation", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.GLEntryNo).HasColumnName("g_l_entry_no");
        builder.Property(x => x.ValueEntryNo).HasColumnName("value_entry_no");
        builder.Property(x => x.GLRegisterNo).HasColumnName("g_l_register_no");
    }
}
