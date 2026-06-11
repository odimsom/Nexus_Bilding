using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class WarehouseActivityLineConfiguration : IEntityTypeConfiguration<WarehouseActivityLine>
{
    public void Configure(EntityTypeBuilder<WarehouseActivityLine> builder)
    {
        builder.ToTable("warehouse_activity_line", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ActivityType).HasColumnName("activity_type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceSubtype).HasColumnName("source_subtype");
        builder.Property(x => x.SourceNo).HasColumnName("source_no");
        builder.Property(x => x.SourceLineNo).HasColumnName("source_line_no");
        builder.Property(x => x.SourceSublineNo).HasColumnName("source_subline_no");
        builder.Property(x => x.SourceDocument).HasColumnName("source_document");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.ShelfNo).HasColumnName("shelf_no");
        builder.Property(x => x.SortingSequenceNo).HasColumnName("sorting_sequence_no");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.QtyBase).HasColumnName("qty_base").HasPrecision(18, 5);
        builder.Property(x => x.QtyOutstanding).HasColumnName("qty_outstanding").HasPrecision(18, 5);
        builder.Property(x => x.QtyOutstandingBase).HasColumnName("qty_outstanding_base").HasPrecision(18, 5);
        builder.Property(x => x.QtyToHandle).HasColumnName("qty_to_handle").HasPrecision(18, 5);
        builder.Property(x => x.QtyToHandleBase).HasColumnName("qty_to_handle_base").HasPrecision(18, 5);
        builder.Property(x => x.QtyHandled).HasColumnName("qty_handled").HasPrecision(18, 5);
        builder.Property(x => x.QtyHandledBase).HasColumnName("qty_handled_base").HasPrecision(18, 5);
        builder.Property(x => x.ShippingAdvice).HasColumnName("shipping_advice");
        builder.Property(x => x.DueDate).HasColumnName("due_date");
        builder.Property(x => x.DestinationType).HasColumnName("destination_type");
        builder.Property(x => x.DestinationNo).HasColumnName("destination_no");
        builder.Property(x => x.ShippingAgentCode).HasColumnName("shipping_agent_code");
        builder.Property(x => x.ShippingAgentServiceCode).HasColumnName("shipping_agent_service_code");
        builder.Property(x => x.ShipmentMethodCode).HasColumnName("shipment_method_code");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.AssembleToOrder).HasColumnName("assemble_to_order");
        builder.Property(x => x.AtoComponent).HasColumnName("ato_component");
        builder.Property(x => x.SerialNo).HasColumnName("serial_no");
        builder.Property(x => x.LotNo).HasColumnName("lot_no");
        builder.Property(x => x.WarrantyDate).HasColumnName("warranty_date");
        builder.Property(x => x.ExpirationDate).HasColumnName("expiration_date");
        builder.Property(x => x.BinCode).HasColumnName("bin_code");
        builder.Property(x => x.ZoneCode).HasColumnName("zone_code");
        builder.Property(x => x.ActionType).HasColumnName("action_type");
        builder.Property(x => x.WhseDocumentType).HasColumnName("whse_document_type");
        builder.Property(x => x.WhseDocumentNo).HasColumnName("whse_document_no");
        builder.Property(x => x.WhseDocumentLineNo).HasColumnName("whse_document_line_no");
        builder.Property(x => x.BinRanking).HasColumnName("bin_ranking");
        builder.Property(x => x.Cubage).HasColumnName("cubage").HasPrecision(18, 5);
        builder.Property(x => x.Weight).HasColumnName("weight").HasPrecision(18, 5);
        builder.Property(x => x.SpecialEquipmentCode).HasColumnName("special_equipment_code");
        builder.Property(x => x.BinTypeCode).HasColumnName("bin_type_code");
        builder.Property(x => x.BreakbulkNo).HasColumnName("breakbulk_no");
        builder.Property(x => x.OriginalBreakbulk).HasColumnName("original_breakbulk");
        builder.Property(x => x.Breakbulk).HasColumnName("breakbulk");
        builder.Property(x => x.CrossDockInformation).HasColumnName("cross_dock_information");
        builder.Property(x => x.Dedicated).HasColumnName("dedicated");
    }
}
