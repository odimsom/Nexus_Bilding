using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class TenantMediaSetConfiguration : IEntityTypeConfiguration<TenantMediaSet>
{
    public void Configure(EntityTypeBuilder<TenantMediaSet> builder)
    {
        builder.ToTable("tenant_media_set", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.MediaId).HasColumnName("media_id");
        builder.Property(x => x.CompanyName).HasColumnName("company_name");
        builder.Property(x => x.MediaIndex).HasColumnName("media_index");
    }
}
