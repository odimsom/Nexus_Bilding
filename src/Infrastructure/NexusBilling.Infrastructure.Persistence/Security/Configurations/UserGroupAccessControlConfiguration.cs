using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class UserGroupAccessControlConfiguration : IEntityTypeConfiguration<UserGroupAccessControl>
{
    public void Configure(EntityTypeBuilder<UserGroupAccessControl> builder)
    {
        builder.ToTable("user_group_access_control", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserGroupCode).HasColumnName("user_group_code");
        builder.Property(x => x.UserSecurityId).HasColumnName("user_security_id");
        builder.Property(x => x.RoleId).HasColumnName("role_id");
        builder.Property(x => x.CompanyName).HasColumnName("company_name");
        builder.Property(x => x.Scope).HasColumnName("scope");
        builder.Property(x => x.AppId).HasColumnName("app_id");
    }
}
