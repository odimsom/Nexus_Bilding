using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class ShippingAgentConfiguration : IEntityTypeConfiguration<ShippingAgent>
{
    public void Configure(EntityTypeBuilder<ShippingAgent> builder)
    {
        builder.ToTable("shipping_agent", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.InternetAddress).HasColumnName("internet_address");
        builder.Property(x => x.AccountNo).HasColumnName("account_no");
    }
}
