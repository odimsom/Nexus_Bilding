using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AccSchedChartSetupLineConfiguration : IEntityTypeConfiguration<AccSchedChartSetupLine>
{
    public void Configure(EntityTypeBuilder<AccSchedChartSetupLine> builder)
    {
        builder.ToTable("acc_sched_chart_setup_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.AccountScheduleName).HasColumnName("account_schedule_name");
        builder.Property(x => x.AccountScheduleLineNo).HasColumnName("account_schedule_line_no");
        builder.Property(x => x.ColumnLayoutName).HasColumnName("column_layout_name");
        builder.Property(x => x.ColumnLayoutLineNo).HasColumnName("column_layout_line_no");
        builder.Property(x => x.OriginalMeasureName).HasColumnName("original_measure_name");
        builder.Property(x => x.MeasureName).HasColumnName("measure_name");
        builder.Property(x => x.MeasureValue).HasColumnName("measure_value");
        builder.Property(x => x.ChartType).HasColumnName("chart_type");
    }
}
