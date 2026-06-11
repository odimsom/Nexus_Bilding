using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class TenantPermissionSetConfiguration : IEntityTypeConfiguration<TenantPermissionSet>
{
    public void Configure(EntityTypeBuilder<TenantPermissionSet> builder)
    {
        builder.ToTable("tenant_permission_set", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AppId).HasColumnName("app_id");
        builder.Property(x => x.RoleId).HasColumnName("role_id");
        builder.Property(x => x.Name).HasColumnName("name");
    }
}
