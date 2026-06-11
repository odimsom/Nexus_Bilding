using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AccountSchedulesChartSetupConfiguration : IEntityTypeConfiguration<AccountSchedulesChartSetup>
{
    public void Configure(EntityTypeBuilder<AccountSchedulesChartSetup> builder)
    {
        builder.ToTable("account_schedules_chart_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.AccountScheduleName).HasColumnName("account_schedule_name");
        builder.Property(x => x.ColumnLayoutName).HasColumnName("column_layout_name");
        builder.Property(x => x.BaseXAxisOn).HasColumnName("base_x_axis_on");
        builder.Property(x => x.StartDate).HasColumnName("start_date");
        builder.Property(x => x.EndDate).HasColumnName("end_date");
        builder.Property(x => x.PeriodLength).HasColumnName("period_length");
        builder.Property(x => x.NoOfPeriods).HasColumnName("no_of_periods");
        builder.Property(x => x.LastViewed).HasColumnName("last_viewed");
        builder.Property(x => x.LookAhead).HasColumnName("look_ahead");
    }
}
