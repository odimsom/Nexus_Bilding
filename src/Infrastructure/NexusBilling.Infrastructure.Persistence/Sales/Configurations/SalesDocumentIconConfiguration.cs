using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class SalesDocumentIconConfiguration : IEntityTypeConfiguration<SalesDocumentIcon>
{
    public void Configure(EntityTypeBuilder<SalesDocumentIcon> builder)
    {
        builder.ToTable("sales_document_icon", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.Picture).HasColumnName("picture");
    }
}
