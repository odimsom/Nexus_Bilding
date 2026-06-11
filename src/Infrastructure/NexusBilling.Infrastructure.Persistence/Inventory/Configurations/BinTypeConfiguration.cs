using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class BinTypeConfiguration : IEntityTypeConfiguration<BinType>
{
    public void Configure(EntityTypeBuilder<BinType> builder)
    {
        builder.ToTable("bin_type", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Receive).HasColumnName("receive");
        builder.Property(x => x.Ship).HasColumnName("ship");
        builder.Property(x => x.PutAway).HasColumnName("put_away");
        builder.Property(x => x.Pick).HasColumnName("pick");
    }
}
