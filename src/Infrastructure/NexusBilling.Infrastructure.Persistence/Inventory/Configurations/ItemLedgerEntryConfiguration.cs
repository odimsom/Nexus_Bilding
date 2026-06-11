using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ItemLedgerEntryConfiguration : IEntityTypeConfiguration<ItemLedgerEntry>
{
    public void Configure(EntityTypeBuilder<ItemLedgerEntry> builder)
    {
        builder.ToTable("item_ledger_entry", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.EntryType).HasColumnName("entry_type");
        builder.Property(x => x.SourceNo).HasColumnName("source_no");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.Quantity).HasColumnName("quantity");
        builder.Property(x => x.RemainingQuantity).HasColumnName("remaining_quantity");
        builder.Property(x => x.InvoicedQuantity).HasColumnName("invoiced_quantity");
        builder.Property(x => x.AppliesToEntry).HasColumnName("applies_to_entry");
        builder.Property(x => x.Open).HasColumnName("open");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension2_code");
        builder.Property(x => x.Positive).HasColumnName("positive");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.DropShipment).HasColumnName("drop_shipment");
        builder.Property(x => x.TransactionType).HasColumnName("transaction_type");
        builder.Property(x => x.TransportMethod).HasColumnName("transport_method");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.EntryExitPoint).HasColumnName("entry_exit_point");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.Area).HasColumnName("area");
        builder.Property(x => x.TransactionSpecification).HasColumnName("transaction_specification");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentLineNo).HasColumnName("document_line_no");
        builder.Property(x => x.OrderType).HasColumnName("order_type");
        builder.Property(x => x.OrderNo).HasColumnName("order_no");
        builder.Property(x => x.OrderLineNo).HasColumnName("order_line_no");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.AssembleToOrder).HasColumnName("assemble_to_order");
        builder.Property(x => x.JobNo).HasColumnName("job_no");
        builder.Property(x => x.JobTaskNo).HasColumnName("job_task_no");
        builder.Property(x => x.JobPurchase).HasColumnName("job_purchase");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.DerivedFromBlanketOrder).HasColumnName("derived_from_blanket_order");
        builder.Property(x => x.CrossReferenceNo).HasColumnName("cross_reference_no");
        builder.Property(x => x.OriginallyOrderedNo).HasColumnName("originally_ordered_no");
        builder.Property(x => x.OriginallyOrderedVarCode).HasColumnName("originally_ordered_var_code");
        builder.Property(x => x.OutOfStockSubstitution).HasColumnName("out_of_stock_substitution");
        builder.Property(x => x.ItemCategoryCode).HasColumnName("item_category_code");
        builder.Property(x => x.Nonstock).HasColumnName("nonstock");
        builder.Property(x => x.PurchasingCode).HasColumnName("purchasing_code");
        builder.Property(x => x.ProductGroupCode).HasColumnName("product_group_code");
        builder.Property(x => x.CompletelyInvoiced).HasColumnName("completely_invoiced");
        builder.Property(x => x.LastInvoiceDate).HasColumnName("last_invoice_date");
        builder.Property(x => x.AppliedEntryToAdjust).HasColumnName("applied_entry_to_adjust");
        builder.Property(x => x.Correction).HasColumnName("correction");
        builder.Property(x => x.ShippedQtyNotReturned).HasColumnName("shipped_qty_not_returned");
        builder.Property(x => x.ProdOrderCompLineNo).HasColumnName("prod_order_comp_line_no");
        builder.Property(x => x.SerialNo).HasColumnName("serial_no");
        builder.Property(x => x.LotNo).HasColumnName("lot_no");
        builder.Property(x => x.WarrantyDate).HasColumnName("warranty_date");
        builder.Property(x => x.ExpirationDate).HasColumnName("expiration_date");
        builder.Property(x => x.ItemTracking).HasColumnName("item_tracking");
        builder.Property(x => x.ReturnReasonCode).HasColumnName("return_reason_code");
    }
}
