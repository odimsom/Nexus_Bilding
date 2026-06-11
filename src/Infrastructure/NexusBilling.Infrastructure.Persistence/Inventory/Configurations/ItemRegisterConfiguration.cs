using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemRegisterConfiguration : IEntityTypeConfiguration<ItemRegister>
{
    public void Configure(EntityTypeBuilder<ItemRegister> builder)
    {
        builder.ToTable("item_register", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.FromEntryNo).HasColumnName("from_entry_no");
        builder.Property(x => x.ToEntryNo).HasColumnName("to_entry_no");
        builder.Property(x => x.CreationDate).HasColumnName("creation_date");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.FromPhysInventoryEntryNo).HasColumnName("from_phys_inventory_entry_no");
        builder.Property(x => x.ToPhysInventoryEntryNo).HasColumnName("to_phys_inventory_entry_no");
        builder.Property(x => x.FromValueEntryNo).HasColumnName("from_value_entry_no");
        builder.Property(x => x.ToValueEntryNo).HasColumnName("to_value_entry_no");
        builder.Property(x => x.FromCapacityEntryNo).HasColumnName("from_capacity_entry_no");
        builder.Property(x => x.ToCapacityEntryNo).HasColumnName("to_capacity_entry_no");
    }
}
