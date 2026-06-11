using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DocumentServiceConfiguration : IEntityTypeConfiguration<DocumentService>
{
    public void Configure(EntityTypeBuilder<DocumentService> builder)
    {
        builder.ToTable("document_service", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ServiceId).HasColumnName("service_id");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Location).HasColumnName("location");
        builder.Property(x => x.UserName).HasColumnName("user_name");
        builder.Property(x => x.Password).HasColumnName("password");
        builder.Property(x => x.DocumentRepository).HasColumnName("document_repository");
        builder.Property(x => x.Folder).HasColumnName("folder");
    }
}
