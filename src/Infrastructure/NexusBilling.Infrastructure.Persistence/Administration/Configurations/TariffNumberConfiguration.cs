using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class TariffNumberConfiguration : IEntityTypeConfiguration<TariffNumber>
{
    public void Configure(EntityTypeBuilder<TariffNumber> builder)
    {
        builder.ToTable("tariff_number", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.SupplementaryUnits).HasColumnName("supplementary_units");
    }
}
