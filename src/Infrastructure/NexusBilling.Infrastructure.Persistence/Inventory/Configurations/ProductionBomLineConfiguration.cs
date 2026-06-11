using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ProductionBomLineConfiguration : IEntityTypeConfiguration<ProductionBomLine>
{
    public void Configure(EntityTypeBuilder<ProductionBomLine> builder)
    {
        builder.ToTable("production_bom_line", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ProductionBomNo).HasColumnName("production_bom_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.VersionCode).HasColumnName("version_code");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.Position).HasColumnName("position");
        builder.Property(x => x.Position2).HasColumnName("position_2");
        builder.Property(x => x.Position3).HasColumnName("position_3");
        builder.Property(x => x.LeadTimeOffset).HasColumnName("lead_time_offset");
        builder.Property(x => x.RoutingLinkCode).HasColumnName("routing_link_code");
        builder.Property(x => x.Scrap).HasColumnName("scrap").HasPrecision(18, 5);
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.EndingDate).HasColumnName("ending_date");
        builder.Property(x => x.Length).HasColumnName("length").HasPrecision(18, 5);
        builder.Property(x => x.Width).HasColumnName("width").HasPrecision(18, 5);
        builder.Property(x => x.Weight).HasColumnName("weight").HasPrecision(18, 5);
        builder.Property(x => x.Depth).HasColumnName("depth").HasPrecision(18, 5);
        builder.Property(x => x.CalculationFormula).HasColumnName("calculation_formula");
        builder.Property(x => x.QuantityPer).HasColumnName("quantity_per").HasPrecision(18, 5);
    }
}
