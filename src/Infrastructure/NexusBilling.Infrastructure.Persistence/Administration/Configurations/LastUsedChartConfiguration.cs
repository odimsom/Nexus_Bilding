using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class LastUsedChartConfiguration : IEntityTypeConfiguration<LastUsedChart>
{
    public void Configure(EntityTypeBuilder<LastUsedChart> builder)
    {
        builder.ToTable("last_used_chart", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Uid).HasColumnName("uid");
        builder.Property(x => x.CodeUnitId).HasColumnName("code_unit_id");
        builder.Property(x => x.ChartName).HasColumnName("chart_name");
    }
}
