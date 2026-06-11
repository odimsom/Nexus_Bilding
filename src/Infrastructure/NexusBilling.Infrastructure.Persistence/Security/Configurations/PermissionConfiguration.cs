using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permission", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.RoleId).HasColumnName("role_id");
        builder.Property(x => x.ObjectType).HasColumnName("object_type");
        builder.Property(x => x.ObjectId).HasColumnName("object_id");
        builder.Property(x => x.ReadPermission).HasColumnName("read_permission");
        builder.Property(x => x.InsertPermission).HasColumnName("insert_permission");
        builder.Property(x => x.ModifyPermission).HasColumnName("modify_permission");
        builder.Property(x => x.DeletePermission).HasColumnName("delete_permission");
        builder.Property(x => x.ExecutePermission).HasColumnName("execute_permission");
        builder.Property(x => x.SecurityFilter).HasColumnName("security_filter");
    }
}
