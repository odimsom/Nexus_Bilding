using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class PlanningAssignmentConfiguration : IEntityTypeConfiguration<PlanningAssignment>
{
    public void Configure(EntityTypeBuilder<PlanningAssignment> builder)
    {
        builder.ToTable("planning_assignment", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ItemNo).HasColumnName("item_no");
        builder.Property(x => x.VariantCode).HasColumnName("variant_code");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.LatestDate).HasColumnName("latest_date");
        builder.Property(x => x.Inactive).HasColumnName("inactive");
        builder.Property(x => x.ActionMsgResponsePlanning).HasColumnName("action_msg_response_planning");
        builder.Property(x => x.NetChangePlanning).HasColumnName("net_change_planning");
    }
}
