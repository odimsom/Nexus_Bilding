using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class PermissionSetConfiguration : IEntityTypeConfiguration<PermissionSet>
{
    public void Configure(EntityTypeBuilder<PermissionSet> builder)
    {
        builder.ToTable("permission_set", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.RoleId).HasColumnName("role_id");
        builder.Property(x => x.Name).HasColumnName("name");
    }
}
