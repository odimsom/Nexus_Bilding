using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ActionMessageEntryConfiguration : IEntityTypeConfiguration<ActionMessageEntry>
{
    public void Configure(EntityTypeBuilder<ActionMessageEntry> builder)
    {
        builder.ToTable("action_message_entry", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.ReservationEntry).HasColumnName("reservation_entry");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.NewDate).HasColumnName("new_date");
        builder.Property(x => x.Calculation).HasColumnName("calculation");
        builder.Property(x => x.SuppressedActionMsg).HasColumnName("suppressed_action_msg");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceSubtype).HasColumnName("source_subtype");
        builder.Property(x => x.SourceId).HasColumnName("source_id");
        builder.Property(x => x.SourceBatchName).HasColumnName("source_batch_name");
        builder.Property(x => x.SourceProdOrderLine).HasColumnName("source_prod_order_line");
        builder.Property(x => x.SourceRefNo).HasColumnName("source_ref_no");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.BinCode).HasColumnName("bin_code");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
    }
}
