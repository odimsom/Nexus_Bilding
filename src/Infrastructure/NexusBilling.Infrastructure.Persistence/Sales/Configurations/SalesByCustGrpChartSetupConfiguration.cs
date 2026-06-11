using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class SalesByCustGrpChartSetupConfiguration : IEntityTypeConfiguration<SalesByCustGrpChartSetup>
{
    public void Configure(EntityTypeBuilder<SalesByCustGrpChartSetup> builder)
    {
        builder.ToTable("sales_by_cust_grp_chart_setup", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.StartDate).HasColumnName("start_date");
        builder.Property(x => x.PeriodLength).HasColumnName("period_length");
    }
}
