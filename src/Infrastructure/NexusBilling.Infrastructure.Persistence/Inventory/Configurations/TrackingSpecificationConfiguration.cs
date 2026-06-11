using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class TrackingSpecificationConfiguration : IEntityTypeConfiguration<TrackingSpecification>
{
    public void Configure(EntityTypeBuilder<TrackingSpecification> builder)
    {
        builder.ToTable("tracking_specification", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.QuantityBase).HasColumnName("quantity_base").HasPrecision(18, 5);
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CreationDate).HasColumnName("creation_date");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceSubtype).HasColumnName("source_subtype");
        builder.Property(x => x.SourceId).HasColumnName("source_id");
        builder.Property(x => x.SourceBatchName).HasColumnName("source_batch_name");
        builder.Property(x => x.SourceProdOrderLine).HasColumnName("source_prod_order_line");
        builder.Property(x => x.SourceRefNo).HasColumnName("source_ref_no");
        builder.Property(x => x.ItemLedgerEntryNo).HasColumnName("item_ledger_entry_no");
        builder.Property(x => x.TransferItemEntryNo).HasColumnName("transfer_item_entry_no");
        builder.Property(x => x.SerialNo).HasColumnName("serial_no");
        builder.Property(x => x.Positive).HasColumnName("positive");
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.ApplToItemEntry).HasColumnName("appl_to_item_entry");
        builder.Property(x => x.WarrantyDate).HasColumnName("warranty_date");
        builder.Property(x => x.ExpirationDate).HasColumnName("expiration_date");
        builder.Property(x => x.QtyToHandleBase).HasColumnName("qty_to_handle_base").HasPrecision(18, 5);
        builder.Property(x => x.QtyToInvoiceBase).HasColumnName("qty_to_invoice_base").HasPrecision(18, 5);
        builder.Property(x => x.QuantityHandledBase).HasColumnName("quantity_handled_base").HasPrecision(18, 5);
        builder.Property(x => x.QuantityInvoicedBase).HasColumnName("quantity_invoiced_base").HasPrecision(18, 5);
        builder.Property(x => x.QtyToHandle).HasColumnName("qty_to_handle").HasPrecision(18, 5);
        builder.Property(x => x.QtyToInvoice).HasColumnName("qty_to_invoice").HasPrecision(18, 5);
        builder.Property(x => x.BufferStatus).HasColumnName("buffer_status");
        builder.Property(x => x.BufferStatus2).HasColumnName("buffer_status2");
        builder.Property(x => x.BufferValue1).HasColumnName("buffer_value1").HasPrecision(18, 5);
        builder.Property(x => x.BufferValue2).HasColumnName("buffer_value2").HasPrecision(18, 5);
        builder.Property(x => x.BufferValue3).HasColumnName("buffer_value3").HasPrecision(18, 5);
        builder.Property(x => x.BufferValue4).HasColumnName("buffer_value4").HasPrecision(18, 5);
        builder.Property(x => x.BufferValue5).HasColumnName("buffer_value5").HasPrecision(18, 5);
        builder.Property(x => x.NewSerialNo).HasColumnName("new_serial_no");
        builder.Property(x => x.NewLotNo).HasColumnName("new_lot_no");
        builder.Property(x => x.ProhibitCancellation).HasColumnName("prohibit_cancellation");
        builder.Property(x => x.LotNo).HasColumnName("lot_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.BinCode).HasColumnName("bin_code");
        builder.Property(x => x.ApplFromItemEntry).HasColumnName("appl_from_item_entry");
        builder.Property(x => x.Correction).HasColumnName("correction");
        builder.Property(x => x.NewExpirationDate).HasColumnName("new_expiration_date");
        builder.Property(x => x.QuantityActualHandledBase).HasColumnName("quantity_actual_handled_base").HasPrecision(18, 5);
    }
}
