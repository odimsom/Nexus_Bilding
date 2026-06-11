using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class WorkflowStepInstanceConfiguration : IEntityTypeConfiguration<WorkflowStepInstance>
{
    public void Configure(EntityTypeBuilder<WorkflowStepInstance> builder)
    {
        builder.ToTable("workflow_step_instance", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.WorkflowCode).HasColumnName("workflow_code");
        builder.Property(x => x.WorkflowStepId).HasColumnName("workflow_step_id");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.EntryPoint).HasColumnName("entry_point");
        builder.Property(x => x.RecordId).HasColumnName("record_id");
        builder.Property(x => x.CreatedDateTime).HasColumnName("created_date_time");
        builder.Property(x => x.CreatedByUserId).HasColumnName("created_by_user_id");
        builder.Property(x => x.LastModifiedDateTime).HasColumnName("last_modified_date_time");
        builder.Property(x => x.LastModifiedByUserId).HasColumnName("last_modified_by_user_id");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.PreviousWorkflowStepId).HasColumnName("previous_workflow_step_id");
        builder.Property(x => x.NextWorkflowStepId).HasColumnName("next_workflow_step_id");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.FunctionName).HasColumnName("function_name");
        builder.Property(x => x.Argument).HasColumnName("argument");
        builder.Property(x => x.OriginalWorkflowCode).HasColumnName("original_workflow_code");
        builder.Property(x => x.OriginalWorkflowStepId).HasColumnName("original_workflow_step_id");
        builder.Property(x => x.SequenceNo).HasColumnName("sequence_no");
    }
}
