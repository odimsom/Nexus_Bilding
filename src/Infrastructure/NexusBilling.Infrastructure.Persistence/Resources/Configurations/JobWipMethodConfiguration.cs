using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class JobWipMethodConfiguration : IEntityTypeConfiguration<JobWipMethod>
{
    public void Configure(EntityTypeBuilder<JobWipMethod> builder)
    {
        builder.ToTable("job_wip_method", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.WipCost).HasColumnName("wip_cost");
        builder.Property(x => x.WipSales).HasColumnName("wip_sales");
        builder.Property(x => x.RecognizedCosts).HasColumnName("recognized_costs");
        builder.Property(x => x.RecognizedSales).HasColumnName("recognized_sales");
        builder.Property(x => x.Valid).HasColumnName("valid");
        builder.Property(x => x.SystemDefined).HasColumnName("system_defined");
        builder.Property(x => x.SystemDefinedIndex).HasColumnName("system_defined_index");
    }
}
