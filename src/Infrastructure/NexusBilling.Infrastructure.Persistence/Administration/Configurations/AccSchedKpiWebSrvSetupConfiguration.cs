using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AccSchedKpiWebSrvSetupConfiguration : IEntityTypeConfiguration<AccSchedKpiWebSrvSetup>
{
    public void Configure(EntityTypeBuilder<AccSchedKpiWebSrvSetup> builder)
    {
        builder.ToTable("acc_sched_kpi_web_srv_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.ForecastedValuesStart).HasColumnName("forecasted_values_start");
        builder.Property(x => x.GLBudgetName).HasColumnName("g_l_budget_name");
        builder.Property(x => x.Period).HasColumnName("period");
        builder.Property(x => x.ViewBy).HasColumnName("view_by");
        builder.Property(x => x.WebServiceName).HasColumnName("web_service_name");
    }
}
