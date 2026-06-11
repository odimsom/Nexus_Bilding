using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class WarehouseEmployeeConfiguration : IEntityTypeConfiguration<WarehouseEmployee>
{
    public void Configure(EntityTypeBuilder<WarehouseEmployee> builder)
    {
        builder.ToTable("warehouse_employee", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.Default).HasColumnName("default");
        builder.Property(x => x.AdcsUser).HasColumnName("adcs_user");
    }
}
