using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class PostedInvtPickHeaderConfiguration : IEntityTypeConfiguration<PostedInvtPickHeader>
{
    public void Configure(EntityTypeBuilder<PostedInvtPickHeader> builder)
    {
        builder.ToTable("posted_invt_pick_header", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.AssignedUserId).HasColumnName("assigned_user_id");
        builder.Property(x => x.AssignmentDate).HasColumnName("assignment_date");
        builder.Property(x => x.AssignmentTime).HasColumnName("assignment_time");
        builder.Property(x => x.RegisteringDate).HasColumnName("registering_date");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.InvtPickNo).HasColumnName("invt_pick_no");
        builder.Property(x => x.NoPrinted).HasColumnName("no_printed");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.SourceNo).HasColumnName("source_no");
        builder.Property(x => x.SourceDocument).HasColumnName("source_document");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceSubtype).HasColumnName("source_subtype");
        builder.Property(x => x.DestinationType).HasColumnName("destination_type");
        builder.Property(x => x.DestinationNo).HasColumnName("destination_no");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.ShipmentDate).HasColumnName("shipment_date");
        builder.Property(x => x.ExternalDocumentNo2).HasColumnName("external_document_no_2");
    }
}
