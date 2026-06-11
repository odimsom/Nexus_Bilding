using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user", "security");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();
    }
}
