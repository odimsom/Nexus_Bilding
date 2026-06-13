using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Ecf.Entities;
using NexusBilling.Core.Domain.Common;

namespace NexusBilling.Infrastructure.Persistence.Ecf.Configurations;

public class EcfDocumentConfiguration : IEntityTypeConfiguration<EcfDocument>
{
    public void Configure(EntityTypeBuilder<EcfDocument> builder)
    {
        builder.ToTable("ecf_document", "erp");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.TenantId)
            .HasConversion(v => v.Value, v => TenantIdentifier.Create(v))
            .HasColumnName("tenant_id")
            .IsRequired();

        builder.Property(e => e.Ncf)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.Status)
            .HasConversion<int>();

        builder.Property(e => e.TrackId)
            .HasMaxLength(100);

        builder.Property(e => e.SecurityCode)
            .HasMaxLength(100);

        builder.Property(e => e.RejectionReason)
            .HasMaxLength(1000);
            
        builder.HasIndex(e => new { e.TenantId, e.Ncf }).IsUnique();
        builder.HasIndex(e => e.SourceDocumentId);
    }
}
