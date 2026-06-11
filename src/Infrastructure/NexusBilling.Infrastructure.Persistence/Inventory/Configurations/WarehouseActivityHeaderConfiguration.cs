using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class WarehouseActivityHeaderConfiguration : IEntityTypeConfiguration<WarehouseActivityHeader>
{
    public void Configure(EntityTypeBuilder<WarehouseActivityHeader> builder)
    {
        builder.ToTable("warehouse_activity_header", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.AssignedUserId).HasColumnName("assigned_user_id");
        builder.Property(x => x.AssignmentDate).HasColumnName("assignment_date");
        builder.Property(x => x.AssignmentTime).HasColumnName("assignment_time");
        builder.Property(x => x.SortingMethod).HasColumnName("sorting_method");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.NoPrinted).HasColumnName("no_printed");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.RegisteringNo).HasColumnName("registering_no");
        builder.Property(x => x.LastRegisteringNo).HasColumnName("last_registering_no");
        builder.Property(x => x.RegisteringNoSeries).HasColumnName("registering_no_series");
        builder.Property(x => x.DateOfLastPrinting).HasColumnName("date_of_last_printing");
        builder.Property(x => x.TimeOfLastPrinting).HasColumnName("time_of_last_printing");
        builder.Property(x => x.BreakbulkFilter).HasColumnName("breakbulk_filter");
        builder.Property(x => x.SourceNo).HasColumnName("source_no");
        builder.Property(x => x.SourceDocument).HasColumnName("source_document");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceSubtype).HasColumnName("source_subtype");
        builder.Property(x => x.DestinationType).HasColumnName("destination_type");
        builder.Property(x => x.DestinationNo).HasColumnName("destination_no");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.ExpectedReceiptDate).HasColumnName("expected_receipt_date");
        builder.Property(x => x.ShipmentDate).HasColumnName("shipment_date");
        builder.Property(x => x.ExternalDocumentNo2).HasColumnName("external_document_no_2");
    }
}
