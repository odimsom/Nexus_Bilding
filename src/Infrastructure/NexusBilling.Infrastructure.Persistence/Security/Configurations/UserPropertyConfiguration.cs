using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class UserPropertyConfiguration : IEntityTypeConfiguration<UserProperty>
{
    public void Configure(EntityTypeBuilder<UserProperty> builder)
    {
        builder.ToTable("user_property", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserSecurityId).HasColumnName("user_security_id");
        builder.Property(x => x.Password).HasColumnName("password");
        builder.Property(x => x.NameIdentifier).HasColumnName("name_identifier");
        builder.Property(x => x.AuthenticationKey).HasColumnName("authentication_key");
        builder.Property(x => x.WebservicesKey).HasColumnName("webservices_key");
        builder.Property(x => x.WebservicesKeyExpiryDate).HasColumnName("webservices_key_expiry_date");
        builder.Property(x => x.AuthenticationObjectId).HasColumnName("authentication_object_id");
    }
}
