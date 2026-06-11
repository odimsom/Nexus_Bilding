using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ChartDefinitionConfiguration : IEntityTypeConfiguration<ChartDefinition>
{
    public void Configure(EntityTypeBuilder<ChartDefinition> builder)
    {
        builder.ToTable("chart_definition", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.CodeUnitId).HasColumnName("code_unit_id");
        builder.Property(x => x.ChartName).HasColumnName("chart_name");
        builder.Property(x => x.Enabled).HasColumnName("enabled");
    }
}
