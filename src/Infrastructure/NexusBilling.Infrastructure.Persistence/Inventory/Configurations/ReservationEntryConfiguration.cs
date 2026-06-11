using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ReservationEntryConfiguration : IEntityTypeConfiguration<ReservationEntry>
{
    public void Configure(EntityTypeBuilder<ReservationEntry> builder)
    {
        builder.ToTable("reservation_entry", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.QuantityBase).HasColumnName("quantity_base").HasPrecision(18, 5);
        builder.Property(x => x.ReservationStatus).HasColumnName("reservation_status");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CreationDate).HasColumnName("creation_date");
        builder.Property(x => x.TransferredFromEntryNo).HasColumnName("transferred_from_entry_no");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceSubtype).HasColumnName("source_subtype");
        builder.Property(x => x.SourceId).HasColumnName("source_id");
        builder.Property(x => x.SourceBatchName).HasColumnName("source_batch_name");
        builder.Property(x => x.SourceProdOrderLine).HasColumnName("source_prod_order_line");
        builder.Property(x => x.SourceRefNo).HasColumnName("source_ref_no");
        builder.Property(x => x.ItemLedgerEntryNo).HasColumnName("item_ledger_entry_no");
        builder.Property(x => x.ExpectedReceiptDate).HasColumnName("expected_receipt_date");
        builder.Property(x => x.ShipmentDate).HasColumnName("shipment_date");
        builder.Property(x => x.SerialNo).HasColumnName("serial_no");
        builder.Property(x => x.CreatedBy).HasColumnName("created_by");
        builder.Property(x => x.ChangedBy).HasColumnName("changed_by");
        builder.Property(x => x.Positive).HasColumnName("positive");
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.Binding).HasColumnName("binding");
        builder.Property(x => x.SuppressedActionMsg).HasColumnName("suppressed_action_msg");
        builder.Property(x => x.PlanningFlexibility).HasColumnName("planning_flexibility");
        builder.Property(x => x.ApplToItemEntry).HasColumnName("appl_to_item_entry");
        builder.Property(x => x.WarrantyDate).HasColumnName("warranty_date");
        builder.Property(x => x.ExpirationDate).HasColumnName("expiration_date");
        builder.Property(x => x.QtyToHandleBase).HasColumnName("qty_to_handle_base").HasPrecision(18, 5);
        builder.Property(x => x.QtyToInvoiceBase).HasColumnName("qty_to_invoice_base").HasPrecision(18, 5);
        builder.Property(x => x.QuantityInvoicedBase).HasColumnName("quantity_invoiced_base").HasPrecision(18, 5);
        builder.Property(x => x.NewSerialNo).HasColumnName("new_serial_no");
        builder.Property(x => x.NewLotNo).HasColumnName("new_lot_no");
        builder.Property(x => x.DisallowCancellation).HasColumnName("disallow_cancellation");
        builder.Property(x => x.LotNo).HasColumnName("lot_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.ApplFromItemEntry).HasColumnName("appl_from_item_entry");
        builder.Property(x => x.Correction).HasColumnName("correction");
        builder.Property(x => x.NewExpirationDate).HasColumnName("new_expiration_date");
        builder.Property(x => x.ItemTracking).HasColumnName("item_tracking");
    }
}
