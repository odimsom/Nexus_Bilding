using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class UserTimeRegisterConfiguration : IEntityTypeConfiguration<UserTimeRegister>
{
    public void Configure(EntityTypeBuilder<UserTimeRegister> builder)
    {
        builder.ToTable("user_time_register", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.Minutes).HasColumnName("minutes").HasPrecision(18, 5);
    }
}
