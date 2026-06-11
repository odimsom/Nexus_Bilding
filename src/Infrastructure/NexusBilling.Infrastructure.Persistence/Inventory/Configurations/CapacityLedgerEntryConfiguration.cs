using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class CapacityLedgerEntryConfiguration : IEntityTypeConfiguration<CapacityLedgerEntry>
{
    public void Configure(EntityTypeBuilder<CapacityLedgerEntry> builder)
    {
        builder.ToTable("capacity_ledger_entry", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.OperationNo).HasColumnName("operation_no");
        builder.Property(x => x.WorkCenterNo).HasColumnName("work_center_no");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.SetupTime).HasColumnName("setup_time").HasPrecision(18, 5);
        builder.Property(x => x.RunTime).HasColumnName("run_time").HasPrecision(18, 5);
        builder.Property(x => x.StopTime).HasColumnName("stop_time").HasPrecision(18, 5);
        builder.Property(x => x.InvoicedQuantity).HasColumnName("invoiced_quantity").HasPrecision(18, 5);
        builder.Property(x => x.OutputQuantity).HasColumnName("output_quantity").HasPrecision(18, 5);
        builder.Property(x => x.ScrapQuantity).HasColumnName("scrap_quantity").HasPrecision(18, 5);
        builder.Property(x => x.ConcurrentCapacity).HasColumnName("concurrent_capacity").HasPrecision(18, 5);
        builder.Property(x => x.CapUnitOfMeasureCode).HasColumnName("cap_unit_of_measure_code");
        builder.Property(x => x.QtyPerCapUnitOfMeasure).HasColumnName("qty_per_cap_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.LastOutputLine).HasColumnName("last_output_line");
        builder.Property(x => x.CompletelyInvoiced).HasColumnName("completely_invoiced");
        builder.Property(x => x.StartingTime).HasColumnName("starting_time");
        builder.Property(x => x.EndingTime).HasColumnName("ending_time");
        builder.Property(x => x.RoutingNo).HasColumnName("routing_no");
        builder.Property(x => x.RoutingReferenceNo).HasColumnName("routing_reference_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.StopCode).HasColumnName("stop_code");
        builder.Property(x => x.ScrapCode).HasColumnName("scrap_code");
        builder.Property(x => x.WorkCenterGroupCode).HasColumnName("work_center_group_code");
        builder.Property(x => x.WorkShiftCode).HasColumnName("work_shift_code");
        builder.Property(x => x.Subcontracting).HasColumnName("subcontracting");
        builder.Property(x => x.OrderType).HasColumnName("order_type");
        builder.Property(x => x.OrderNo).HasColumnName("order_no");
        builder.Property(x => x.OrderLineNo).HasColumnName("order_line_no");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
    }
}
