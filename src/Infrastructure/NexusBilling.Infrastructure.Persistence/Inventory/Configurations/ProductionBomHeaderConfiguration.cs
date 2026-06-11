using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ProductionBomHeaderConfiguration : IEntityTypeConfiguration<ProductionBomHeader>
{
    public void Configure(EntityTypeBuilder<ProductionBomHeader> builder)
    {
        builder.ToTable("production_bom_header", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Description2).HasColumnName("description_2");
        builder.Property(x => x.SearchName).HasColumnName("search_name");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.LowLevelCode).HasColumnName("low_level_code");
        builder.Property(x => x.CreationDate).HasColumnName("creation_date");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.VersionNos).HasColumnName("version_nos");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
    }
}
