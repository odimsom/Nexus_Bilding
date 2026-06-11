using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class WorkflowUserGroupMemberConfiguration : IEntityTypeConfiguration<WorkflowUserGroupMember>
{
    public void Configure(EntityTypeBuilder<WorkflowUserGroupMember> builder)
    {
        builder.ToTable("workflow_user_group_member", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.WorkflowUserGroupCode).HasColumnName("workflow_user_group_code");
        builder.Property(x => x.UserName).HasColumnName("user_name");
        builder.Property(x => x.SequenceNo).HasColumnName("sequence_no");
    }
}
