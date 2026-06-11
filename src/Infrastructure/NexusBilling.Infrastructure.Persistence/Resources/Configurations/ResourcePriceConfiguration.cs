using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class ResourcePriceConfiguration : IEntityTypeConfiguration<ResourcePrice>
{
    public void Configure(EntityTypeBuilder<ResourcePrice> builder)
    {
        builder.ToTable("resource_price", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.WorkTypeCode).HasColumnName("work_type_code");
        builder.Property(x => x.UnitPrice).HasColumnName("unit_price").HasPrecision(18, 5);
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
    }
}
