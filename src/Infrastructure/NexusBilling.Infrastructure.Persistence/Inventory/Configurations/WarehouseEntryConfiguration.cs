using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class WarehouseEntryConfiguration : IEntityTypeConfiguration<WarehouseEntry>
{
    public void Configure(EntityTypeBuilder<WarehouseEntry> builder)
    {
        builder.ToTable("warehouse_entry", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.RegisteringDate).HasColumnName("registering_date");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.ZoneCode).HasColumnName("zone_code");
        builder.Property(x => x.BinCode).HasColumnName("bin_code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.QtyBase).HasColumnName("qty_base").HasPrecision(18, 5);
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceSubtype).HasColumnName("source_subtype");
        builder.Property(x => x.SourceNo).HasColumnName("source_no");
        builder.Property(x => x.SourceLineNo).HasColumnName("source_line_no");
        builder.Property(x => x.SourceSublineNo).HasColumnName("source_subline_no");
        builder.Property(x => x.SourceDocument).HasColumnName("source_document");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.BinTypeCode).HasColumnName("bin_type_code");
        builder.Property(x => x.Cubage).HasColumnName("cubage").HasPrecision(18, 5);
        builder.Property(x => x.Weight).HasColumnName("weight").HasPrecision(18, 5);
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.WhseDocumentNo).HasColumnName("whse_document_no");
        builder.Property(x => x.WhseDocumentType).HasColumnName("whse_document_type");
        builder.Property(x => x.WhseDocumentLineNo).HasColumnName("whse_document_line_no");
        builder.Property(x => x.EntryType).HasColumnName("entry_type");
        builder.Property(x => x.ReferenceDocument).HasColumnName("reference_document");
        builder.Property(x => x.ReferenceNo).HasColumnName("reference_no");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.SerialNo).HasColumnName("serial_no");
        builder.Property(x => x.LotNo).HasColumnName("lot_no");
        builder.Property(x => x.WarrantyDate).HasColumnName("warranty_date");
        builder.Property(x => x.ExpirationDate).HasColumnName("expiration_date");
        builder.Property(x => x.PhysInvtCountingPeriodCode).HasColumnName("phys_invt_counting_period_code");
        builder.Property(x => x.PhysInvtCountingPeriodType).HasColumnName("phys_invt_counting_period_type");
        builder.Property(x => x.Dedicated).HasColumnName("dedicated");
    }
}
