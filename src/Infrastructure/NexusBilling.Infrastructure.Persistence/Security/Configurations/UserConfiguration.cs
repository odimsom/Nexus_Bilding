using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user", "erp");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.TenantId, b =>
        {
            b.Property(t => t.Value).HasColumnName("tenant_id").IsRequired();
        });

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
