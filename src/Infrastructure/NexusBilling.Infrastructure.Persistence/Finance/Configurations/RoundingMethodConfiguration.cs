using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class RoundingMethodConfiguration : IEntityTypeConfiguration<RoundingMethod>
{
    public void Configure(EntityTypeBuilder<RoundingMethod> builder)
    {
        builder.ToTable("rounding_method", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.MinimumAmount).HasColumnName("minimum_amount");
        builder.Property(x => x.AmountAddedBefore).HasColumnName("amount_added_before");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.Precision).HasColumnName("precision");
        builder.Property(x => x.AmountAddedAfter).HasColumnName("amount_added_after");
    }
}
