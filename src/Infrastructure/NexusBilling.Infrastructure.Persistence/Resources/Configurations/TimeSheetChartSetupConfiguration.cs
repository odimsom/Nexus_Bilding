using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class TimeSheetChartSetupConfiguration : IEntityTypeConfiguration<TimeSheetChartSetup>
{
    public void Configure(EntityTypeBuilder<TimeSheetChartSetup> builder)
    {
        builder.ToTable("time_sheet_chart_setup", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.ShowBy).HasColumnName("show_by");
        builder.Property(x => x.MeasureType).HasColumnName("measure_type");
    }
}
