using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class UserSecurityStatusConfiguration : IEntityTypeConfiguration<UserSecurityStatus>
{
    public void Configure(EntityTypeBuilder<UserSecurityStatus> builder)
    {
        builder.ToTable("user_security_status", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserSecurityId).HasColumnName("user_security_id");
        builder.Property(x => x.Reviewed).HasColumnName("reviewed");
    }
}
