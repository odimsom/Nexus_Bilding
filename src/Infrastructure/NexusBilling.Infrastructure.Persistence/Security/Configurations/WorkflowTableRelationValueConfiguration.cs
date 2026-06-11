using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class WorkflowTableRelationValueConfiguration : IEntityTypeConfiguration<WorkflowTableRelationValue>
{
    public void Configure(EntityTypeBuilder<WorkflowTableRelationValue> builder)
    {
        builder.ToTable("workflow_table_relation_value", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.WorkflowStepInstanceId).HasColumnName("workflow_step_instance_id");
        builder.Property(x => x.WorkflowCode).HasColumnName("workflow_code");
        builder.Property(x => x.WorkflowStepId).HasColumnName("workflow_step_id");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.FieldId).HasColumnName("field_id");
        builder.Property(x => x.RelatedTableId).HasColumnName("related_table_id");
        builder.Property(x => x.RelatedFieldId).HasColumnName("related_field_id");
        builder.Property(x => x.Value).HasColumnName("value");
        builder.Property(x => x.RecordId).HasColumnName("record_id");
    }
}
