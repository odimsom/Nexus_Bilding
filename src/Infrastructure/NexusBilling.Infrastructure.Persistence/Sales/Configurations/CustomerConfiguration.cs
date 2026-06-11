using NexusBilling.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using NexusBilling.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customer", "sales");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();

        builder.Property(x => x.No).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Address).HasMaxLength(50);
        builder.Property(x => x.City).HasMaxLength(30);
        builder.Property(x => x.Contact).HasMaxLength(50);
        builder.Property(x => x.Blocked).IsRequired();

        builder.HasIndex(x => new { x.TenantId, x.No }).IsUnique();
    }
}
