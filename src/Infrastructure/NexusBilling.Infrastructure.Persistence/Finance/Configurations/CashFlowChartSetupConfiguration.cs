using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CashFlowChartSetupConfiguration : IEntityTypeConfiguration<CashFlowChartSetup>
{
    public void Configure(EntityTypeBuilder<CashFlowChartSetup> builder)
    {
        builder.ToTable("cash_flow_chart_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.PeriodLength).HasColumnName("period_length");
        builder.Property(x => x.Show).HasColumnName("show");
        builder.Property(x => x.StartDate).HasColumnName("start_date");
        builder.Property(x => x.GroupBy).HasColumnName("group_by");
        builder.Property(x => x.ChartType).HasColumnName("chart_type");
    }
}
