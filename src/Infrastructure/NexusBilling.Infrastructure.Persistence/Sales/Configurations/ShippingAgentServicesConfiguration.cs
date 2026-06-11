using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class ShippingAgentServicesConfiguration : IEntityTypeConfiguration<ShippingAgentServices>
{
    public void Configure(EntityTypeBuilder<ShippingAgentServices> builder)
    {
        builder.ToTable("shipping_agent_services", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ShippingAgentCode).HasColumnName("shipping_agent_code");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ShippingTime).HasColumnName("shipping_time");
        builder.Property(x => x.BaseCalendarCode).HasColumnName("base_calendar_code");
    }
}
