using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceDocumentRegisterConfiguration : IEntityTypeConfiguration<ServiceDocumentRegister>
{
    public void Configure(EntityTypeBuilder<ServiceDocumentRegister> builder)
    {
        builder.ToTable("service_document_register", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.SourceDocumentType).HasColumnName("source_document_type");
        builder.Property(x => x.SourceDocumentNo).HasColumnName("source_document_no");
        builder.Property(x => x.DestinationDocumentType).HasColumnName("destination_document_type");
        builder.Property(x => x.DestinationDocumentNo).HasColumnName("destination_document_no");
    }
}
