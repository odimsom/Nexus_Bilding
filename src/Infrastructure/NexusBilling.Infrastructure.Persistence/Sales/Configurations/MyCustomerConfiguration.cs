using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class MyCustomerConfiguration : IEntityTypeConfiguration<MyCustomer>
{
    public void Configure(EntityTypeBuilder<MyCustomer> builder)
    {
        builder.ToTable("my_customer", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.CustomerNo).HasColumnName("customer_no");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.PhoneNo).HasColumnName("phone_no");
    }
}
