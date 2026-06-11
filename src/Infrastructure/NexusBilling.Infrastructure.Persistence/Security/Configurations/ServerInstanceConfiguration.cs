using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class ServerInstanceConfiguration : IEntityTypeConfiguration<ServerInstance>
{
    public void Configure(EntityTypeBuilder<ServerInstance> builder)
    {
        builder.ToTable("server_instance", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ServerInstanceId).HasColumnName("server_instance_id");
        builder.Property(x => x.ServiceName).HasColumnName("service_name");
        builder.Property(x => x.ServerComputerName).HasColumnName("server_computer_name");
        builder.Property(x => x.LastActive).HasColumnName("last_active");
        builder.Property(x => x.ServerInstanceName).HasColumnName("server_instance_name");
        builder.Property(x => x.ServerPort).HasColumnName("server_port");
        builder.Property(x => x.ManagementPort).HasColumnName("management_port");
        builder.Property(x => x.Status).HasColumnName("status");
    }
}
