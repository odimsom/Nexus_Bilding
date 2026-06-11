using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class CancelledDocumentConfiguration : IEntityTypeConfiguration<CancelledDocument>
{
    public void Configure(EntityTypeBuilder<CancelledDocument> builder)
    {
        builder.ToTable("cancelled_document", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.SourceId).HasColumnName("source_id");
        builder.Property(x => x.CancelledDocNo).HasColumnName("cancelled_doc_no");
        builder.Property(x => x.CancelledByDocNo).HasColumnName("cancelled_by_doc_no");
    }
}
