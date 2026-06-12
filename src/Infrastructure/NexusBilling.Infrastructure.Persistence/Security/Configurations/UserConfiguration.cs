using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Username).HasColumnName("username");
        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.PasswordHash).HasColumnName("password_hash");
        builder.Property(x => x.IsActive).HasColumnName("is_active");
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.FullName).HasColumnName("full_name").HasDefaultValue(string.Empty);
        builder.Property(x => x.EmployeeNo).HasColumnName("employee_no").HasDefaultValue(string.Empty);
    }
}
