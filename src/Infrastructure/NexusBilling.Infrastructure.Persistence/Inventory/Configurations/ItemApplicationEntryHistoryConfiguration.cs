using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemApplicationEntryHistoryConfiguration : IEntityTypeConfiguration<ItemApplicationEntryHistory>
{
    public void Configure(EntityTypeBuilder<ItemApplicationEntryHistory> builder)
    {
        builder.ToTable("item_application_entry_history", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.ItemLedgerEntryNo).HasColumnName("item_ledger_entry_no");
        builder.Property(x => x.InboundItemEntryNo).HasColumnName("inbound_item_entry_no");
        builder.Property(x => x.OutboundItemEntryNo).HasColumnName("outbound_item_entry_no");
        builder.Property(x => x.PrimaryEntryNo).HasColumnName("primary_entry_no");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.TransferredFromEntryNo).HasColumnName("transferred_from_entry_no");
        builder.Property(x => x.CreationDate).HasColumnName("creation_date");
        builder.Property(x => x.CreatedByUser).HasColumnName("created_by_user");
        builder.Property(x => x.LastModifiedDate).HasColumnName("last_modified_date");
        builder.Property(x => x.LastModifiedByUser).HasColumnName("last_modified_by_user");
        builder.Property(x => x.DeletedDate).HasColumnName("deleted_date");
        builder.Property(x => x.DeletedByUser).HasColumnName("deleted_by_user");
        builder.Property(x => x.CostApplication).HasColumnName("cost_application");
        builder.Property(x => x.OutputCompletelyInvdDate).HasColumnName("output_completely_invd_date");
    }
}
