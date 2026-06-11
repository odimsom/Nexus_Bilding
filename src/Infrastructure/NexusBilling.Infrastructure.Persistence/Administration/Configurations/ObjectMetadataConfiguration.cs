using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ObjectMetadataConfiguration : IEntityTypeConfiguration<ObjectMetadata>
{
    public void Configure(EntityTypeBuilder<ObjectMetadata> builder)
    {
        builder.ToTable("object_metadata", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ObjectType).HasColumnName("object_type");
        builder.Property(x => x.ObjectId).HasColumnName("object_id");
        builder.Property(x => x.Metadata).HasColumnName("metadata");
        builder.Property(x => x.UserCode).HasColumnName("user_code");
        builder.Property(x => x.UserAlCode).HasColumnName("user_al_code");
        builder.Property(x => x.MetadataVersion).HasColumnName("metadata_version");
        builder.Property(x => x.Hash).HasColumnName("hash");
        builder.Property(x => x.ObjectSubtype).HasColumnName("object_subtype");
        builder.Property(x => x.HasSubscribers).HasColumnName("has_subscribers");
    }
}
