using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class TrailingSalesOrdersSetupConfiguration : IEntityTypeConfiguration<TrailingSalesOrdersSetup>
{
    public void Configure(EntityTypeBuilder<TrailingSalesOrdersSetup> builder)
    {
        builder.ToTable("trailing_sales_orders_setup", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.PeriodLength).HasColumnName("period_length");
        builder.Property(x => x.ShowOrders).HasColumnName("show_orders");
        builder.Property(x => x.UseWorkDateAsBase).HasColumnName("use_work_date_as_base");
        builder.Property(x => x.ValueToCalculate).HasColumnName("value_to_calculate");
        builder.Property(x => x.ChartType).HasColumnName("chart_type");
    }
}
