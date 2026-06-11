using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class WorkflowResponseConfiguration : IEntityTypeConfiguration<WorkflowResponse>
{
    public void Configure(EntityTypeBuilder<WorkflowResponse> builder)
    {
        builder.ToTable("workflow_response", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.FunctionName).HasColumnName("function_name");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ResponseOptionGroup).HasColumnName("response_option_group");
        builder.Property(x => x.Independent).HasColumnName("independent");
    }
}
