using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class MyVendorConfiguration : IEntityTypeConfiguration<MyVendor>
{
    public void Configure(EntityTypeBuilder<MyVendor> builder)
    {
        builder.ToTable("my_vendor", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.PhoneNo).HasColumnName("phone_no");
    }
}
