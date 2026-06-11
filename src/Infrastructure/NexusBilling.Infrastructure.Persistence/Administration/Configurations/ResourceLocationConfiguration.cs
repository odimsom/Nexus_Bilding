using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ResourceLocationConfiguration : IEntityTypeConfiguration<ResourceLocation>
{
    public void Configure(EntityTypeBuilder<ResourceLocation> builder)
    {
        builder.ToTable("resource_location", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.ResourceNo).HasColumnName("resource_no");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
    }
}
