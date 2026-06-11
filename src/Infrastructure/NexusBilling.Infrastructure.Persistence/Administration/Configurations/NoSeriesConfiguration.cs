using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class NoSeriesConfiguration : IEntityTypeConfiguration<NoSeries>
{
    public void Configure(EntityTypeBuilder<NoSeries> builder)
    {
        builder.ToTable("no_series", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.DefaultNos).HasColumnName("default_nos");
        builder.Property(x => x.ManualNos).HasColumnName("manual_nos");
        builder.Property(x => x.DateOrder).HasColumnName("date_order");
    }
}
