using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class PageDocumentationConfiguration : IEntityTypeConfiguration<PageDocumentation>
{
    public void Configure(EntityTypeBuilder<PageDocumentation> builder)
    {
        builder.ToTable("page_documentation", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PageId).HasColumnName("page_id");
        builder.Property(x => x.RelativePath).HasColumnName("relative_path");
    }
}
