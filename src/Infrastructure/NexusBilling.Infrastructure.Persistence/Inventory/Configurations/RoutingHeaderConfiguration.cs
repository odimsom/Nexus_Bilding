using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class RoutingHeaderConfiguration : IEntityTypeConfiguration<RoutingHeader>
{
    public void Configure(EntityTypeBuilder<RoutingHeader> builder)
    {
        builder.ToTable("routing_header", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.SearchDescription).HasColumnName("search_description");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.VersionNos).HasColumnName("version_nos");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
    }
}
