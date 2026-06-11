using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class NoSeriesRelationshipConfiguration : IEntityTypeConfiguration<NoSeriesRelationship>
{
    public void Configure(EntityTypeBuilder<NoSeriesRelationship> builder)
    {
        builder.ToTable("no_series_relationship", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.SeriesCode).HasColumnName("series_code");
    }
}
