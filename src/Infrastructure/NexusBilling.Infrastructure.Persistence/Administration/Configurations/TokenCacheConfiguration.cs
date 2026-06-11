using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class TokenCacheConfiguration : IEntityTypeConfiguration<TokenCache>
{
    public void Configure(EntityTypeBuilder<TokenCache> builder)
    {
        builder.ToTable("token_cache", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserSecurityId).HasColumnName("user_security_id");
        builder.Property(x => x.UserUniqueId).HasColumnName("user_unique_id");
        builder.Property(x => x.TenantIdNav).HasColumnName("tenant_id_nav");
        builder.Property(x => x.CacheWriteTime).HasColumnName("cache_write_time");
        builder.Property(x => x.CacheData).HasColumnName("cache_data");
    }
}
