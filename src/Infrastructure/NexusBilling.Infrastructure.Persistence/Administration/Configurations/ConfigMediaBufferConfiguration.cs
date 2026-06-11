using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ConfigMediaBufferConfiguration : IEntityTypeConfiguration<ConfigMediaBuffer>
{
    public void Configure(EntityTypeBuilder<ConfigMediaBuffer> builder)
    {
        builder.ToTable("config_media_buffer", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PackageCode).HasColumnName("package_code");
        builder.Property(x => x.MediaSetId).HasColumnName("media_set_id");
        builder.Property(x => x.MediaId).HasColumnName("media_id");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.MediaBlob).HasColumnName("media_blob");
        builder.Property(x => x.MediaSet).HasColumnName("media_set");
        builder.Property(x => x.Media).HasColumnName("media");
    }
}
