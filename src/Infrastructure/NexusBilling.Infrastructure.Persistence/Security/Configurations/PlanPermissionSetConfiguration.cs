using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class PlanPermissionSetConfiguration : IEntityTypeConfiguration<PlanPermissionSet>
{
    public void Configure(EntityTypeBuilder<PlanPermissionSet> builder)
    {
        builder.ToTable("plan_permission_set", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PlanId).HasColumnName("plan_id");
        builder.Property(x => x.PermissionSetId).HasColumnName("permission_set_id");
    }
}
