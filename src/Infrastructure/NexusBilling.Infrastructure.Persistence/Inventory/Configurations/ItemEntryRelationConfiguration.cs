using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemEntryRelationConfiguration : IEntityTypeConfiguration<ItemEntryRelation>
{
    public void Configure(EntityTypeBuilder<ItemEntryRelation> builder)
    {
        builder.ToTable("item_entry_relation", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ItemEntryNo).HasColumnName("item_entry_no");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceSubtype).HasColumnName("source_subtype");
        builder.Property(x => x.SourceId).HasColumnName("source_id");
        builder.Property(x => x.SourceBatchName).HasColumnName("source_batch_name");
        builder.Property(x => x.SourceProdOrderLine).HasColumnName("source_prod_order_line");
        builder.Property(x => x.SourceRefNo).HasColumnName("source_ref_no");
        builder.Property(x => x.SerialNo).HasColumnName("serial_no");
        builder.Property(x => x.LotNo).HasColumnName("lot_no");
        builder.Property(x => x.OrderNo).HasColumnName("order_no");
        builder.Property(x => x.OrderLineNo).HasColumnName("order_line_no");
    }
}
