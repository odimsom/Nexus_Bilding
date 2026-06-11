using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class OrderPromisingSetupConfiguration : IEntityTypeConfiguration<OrderPromisingSetup>
{
    public void Configure(EntityTypeBuilder<OrderPromisingSetup> builder)
    {
        builder.ToTable("order_promising_setup", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.OffsetTime).HasColumnName("offset_time");
        builder.Property(x => x.OrderPromisingNos).HasColumnName("order_promising_nos");
        builder.Property(x => x.OrderPromisingTemplate).HasColumnName("order_promising_template");
        builder.Property(x => x.OrderPromisingWorksheet).HasColumnName("order_promising_worksheet");
    }
}
