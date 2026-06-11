using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class WorkflowEventConfiguration : IEntityTypeConfiguration<WorkflowEvent>
{
    public void Configure(EntityTypeBuilder<WorkflowEvent> builder)
    {
        builder.ToTable("workflow_event", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.FunctionName).HasColumnName("function_name");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.RequestPageId).HasColumnName("request_page_id");
        builder.Property(x => x.DynamicReqPageEntityName).HasColumnName("dynamic_req_page_entity_name");
        builder.Property(x => x.UsedForRecordChange).HasColumnName("used_for_record_change");
        builder.Property(x => x.Independent).HasColumnName("independent");
    }
}
