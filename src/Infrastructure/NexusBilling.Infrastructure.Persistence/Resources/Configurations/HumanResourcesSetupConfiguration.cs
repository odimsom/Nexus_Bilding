using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class HumanResourcesSetupConfiguration : IEntityTypeConfiguration<HumanResourcesSetup>
{
    public void Configure(EntityTypeBuilder<HumanResourcesSetup> builder)
    {
        builder.ToTable("human_resources_setup", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.EmployeeNos).HasColumnName("employee_nos");
        builder.Property(x => x.BaseUnitOfMeasure).HasColumnName("base_unit_of_measure");
    }
}
