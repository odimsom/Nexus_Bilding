using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class UserMetadataConfiguration : IEntityTypeConfiguration<UserMetadata>
{
    public void Configure(EntityTypeBuilder<UserMetadata> builder)
    {
        builder.ToTable("user_metadata", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserSid).HasColumnName("user_sid");
        builder.Property(x => x.PageId).HasColumnName("page_id");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.Time).HasColumnName("time");
        builder.Property(x => x.PersonalizationId).HasColumnName("personalization_id");
        builder.Property(x => x.PageMetadataDelta).HasColumnName("page_metadata_delta");
    }
}
