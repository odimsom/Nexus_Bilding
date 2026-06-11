using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class TransferReceiptLineConfiguration : IEntityTypeConfiguration<TransferReceiptLine>
{
    public void Configure(EntityTypeBuilder<TransferReceiptLine> builder)
    {
        builder.ToTable("transfer_receipt_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.UnitOfMeasure).HasColumnName("unit_of_measure");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension_1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension_2_code");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.InventoryPostingGroup).HasColumnName("inventory_posting_group");
        builder.Property(x => x.QuantityBase).HasColumnName("quantity_base").HasPrecision(18, 5);
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.GrossWeight).HasColumnName("gross_weight").HasPrecision(18, 5);
        builder.Property(x => x.NetWeight).HasColumnName("net_weight").HasPrecision(18, 5);
        builder.Property(x => x.UnitVolume).HasColumnName("unit_volume").HasPrecision(18, 5);
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.UnitsPerParcel).HasColumnName("units_per_parcel").HasPrecision(18, 5);
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.TransferOrderNo).HasColumnName("transfer_order_no");
        builder.Property(x => x.ReceiptDate).HasColumnName("receipt_date");
        builder.Property(x => x.ShippingAgentCode).HasColumnName("shipping_agent_code");
        builder.Property(x => x.ShippingAgentServiceCode).HasColumnName("shipping_agent_service_code");
        builder.Property(x => x.InTransitCode).HasColumnName("in_transit_code");
        builder.Property(x => x.TransferFromCode).HasColumnName("transfer_from_code");
        builder.Property(x => x.TransferToCode).HasColumnName("transfer_to_code");
        builder.Property(x => x.ItemRcptEntryNo).HasColumnName("item_rcpt_entry_no");
        builder.Property(x => x.ShippingTime).HasColumnName("shipping_time");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.ItemCategoryCode).HasColumnName("item_category_code");
        builder.Property(x => x.ProductGroupCode).HasColumnName("product_group_code");
        builder.Property(x => x.TransferToBinCode).HasColumnName("transfer_to_bin_code");
    }
}
