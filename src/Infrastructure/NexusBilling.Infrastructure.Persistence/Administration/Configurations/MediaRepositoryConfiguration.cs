using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class MediaRepositoryConfiguration : IEntityTypeConfiguration<MediaRepository>
{
    public void Configure(EntityTypeBuilder<MediaRepository> builder)
    {
        builder.ToTable("media_repository", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.FileName).HasColumnName("file_name");
        builder.Property(x => x.DisplayTarget).HasColumnName("display_target");
        builder.Property(x => x.Image).HasColumnName("image");
    }
}
