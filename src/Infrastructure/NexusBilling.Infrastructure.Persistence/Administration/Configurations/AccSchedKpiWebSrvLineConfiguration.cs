using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AccSchedKpiWebSrvLineConfiguration : IEntityTypeConfiguration<AccSchedKpiWebSrvLine>
{
    public void Configure(EntityTypeBuilder<AccSchedKpiWebSrvLine> builder)
    {
        builder.ToTable("acc_sched_kpi_web_srv_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AccScheduleName).HasColumnName("acc_schedule_name");
    }
}
