using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class WorkflowRuleConfiguration : IEntityTypeConfiguration<WorkflowRule>
{
    public void Configure(EntityTypeBuilder<WorkflowRule> builder)
    {
        builder.ToTable("workflow_rule", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.FieldNo).HasColumnName("field_no");
        builder.Property(x => x.Operator).HasColumnName("operator");
        builder.Property(x => x.WorkflowCode).HasColumnName("workflow_code");
        builder.Property(x => x.WorkflowStepId).HasColumnName("workflow_step_id");
        builder.Property(x => x.WorkflowStepInstanceId).HasColumnName("workflow_step_instance_id");
    }
}
