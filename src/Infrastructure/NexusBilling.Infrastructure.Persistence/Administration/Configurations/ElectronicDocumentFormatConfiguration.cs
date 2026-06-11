using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ElectronicDocumentFormatConfiguration : IEntityTypeConfiguration<ElectronicDocumentFormat>
{
    public void Configure(EntityTypeBuilder<ElectronicDocumentFormat> builder)
    {
        builder.ToTable("electronic_document_format", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Usage).HasColumnName("usage");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CodeunitId).HasColumnName("codeunit_id");
        builder.Property(x => x.DeliveryCodeunitId).HasColumnName("delivery_codeunit_id");
    }
}
