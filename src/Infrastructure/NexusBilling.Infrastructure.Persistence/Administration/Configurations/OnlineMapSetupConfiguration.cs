using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class OnlineMapSetupConfiguration : IEntityTypeConfiguration<OnlineMapSetup>
{
    public void Configure(EntityTypeBuilder<OnlineMapSetup> builder)
    {
        builder.ToTable("online_map_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.MapParameterSetupCode).HasColumnName("map_parameter_setup_code");
        builder.Property(x => x.DistanceIn).HasColumnName("distance_in");
        builder.Property(x => x.Route).HasColumnName("route");
    }
}
