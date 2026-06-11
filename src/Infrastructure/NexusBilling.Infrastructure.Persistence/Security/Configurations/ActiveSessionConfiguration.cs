using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class ActiveSessionConfiguration : IEntityTypeConfiguration<ActiveSession>
{
    public void Configure(EntityTypeBuilder<ActiveSession> builder)
    {
        builder.ToTable("active_session", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserSid).HasColumnName("user_sid");
        builder.Property(x => x.ServerInstanceId).HasColumnName("server_instance_id");
        builder.Property(x => x.SessionId).HasColumnName("session_id");
        builder.Property(x => x.ServerInstanceName).HasColumnName("server_instance_name");
        builder.Property(x => x.ServerComputerName).HasColumnName("server_computer_name");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.ClientType).HasColumnName("client_type");
        builder.Property(x => x.ClientComputerName).HasColumnName("client_computer_name");
        builder.Property(x => x.LoginDatetime).HasColumnName("login_datetime");
        builder.Property(x => x.DatabaseName).HasColumnName("database_name");
        builder.Property(x => x.SessionUniqueId).HasColumnName("session_unique_id");
    }
}
