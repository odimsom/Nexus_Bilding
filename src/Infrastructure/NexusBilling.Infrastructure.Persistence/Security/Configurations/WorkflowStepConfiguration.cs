using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class WorkflowStepConfiguration : IEntityTypeConfiguration<WorkflowStep>
{
    public void Configure(EntityTypeBuilder<WorkflowStep> builder)
    {
        builder.ToTable("workflow_step", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.WorkflowCode).HasColumnName("workflow_code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.EntryPoint).HasColumnName("entry_point");
        builder.Property(x => x.PreviousWorkflowStepId).HasColumnName("previous_workflow_step_id");
        builder.Property(x => x.NextWorkflowStepId).HasColumnName("next_workflow_step_id");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.FunctionName).HasColumnName("function_name");
        builder.Property(x => x.Argument).HasColumnName("argument");
        builder.Property(x => x.SequenceNo).HasColumnName("sequence_no");
    }
}
