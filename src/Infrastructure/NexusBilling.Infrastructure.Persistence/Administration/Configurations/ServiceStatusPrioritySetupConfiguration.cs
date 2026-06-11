using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceStatusPrioritySetupConfiguration : IEntityTypeConfiguration<ServiceStatusPrioritySetup>
{
    public void Configure(EntityTypeBuilder<ServiceStatusPrioritySetup> builder)
    {
        builder.ToTable("service_status_priority_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ServiceOrderStatus).HasColumnName("service_order_status");
        builder.Property(x => x.Priority).HasColumnName("priority");
    }
}
