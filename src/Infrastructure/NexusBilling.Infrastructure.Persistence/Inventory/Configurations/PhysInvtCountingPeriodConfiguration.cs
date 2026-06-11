using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class PhysInvtCountingPeriodConfiguration : IEntityTypeConfiguration<PhysInvtCountingPeriod>
{
    public void Configure(EntityTypeBuilder<PhysInvtCountingPeriod> builder)
    {
        builder.ToTable("phys_invt_counting_period", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CountFrequencyPerYear).HasColumnName("count_frequency_per_year");
    }
}
