using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class TenantMediaConfiguration : IEntityTypeConfiguration<TenantMedia>
{
    public void Configure(EntityTypeBuilder<TenantMedia> builder)
    {
        builder.ToTable("tenant_media", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Content).HasColumnName("content");
        builder.Property(x => x.MimeType).HasColumnName("mime_type");
        builder.Property(x => x.Height).HasColumnName("height");
        builder.Property(x => x.Width).HasColumnName("width");
        builder.Property(x => x.CompanyName).HasColumnName("company_name");
        builder.Property(x => x.ExpirationDate).HasColumnName("expiration_date");
        builder.Property(x => x.ProhibitCache).HasColumnName("prohibit_cache");
        builder.Property(x => x.FileName).HasColumnName("file_name");
        builder.Property(x => x.SecurityToken).HasColumnName("security_token");
        builder.Property(x => x.CreatingUser).HasColumnName("creating_user");
    }
}
