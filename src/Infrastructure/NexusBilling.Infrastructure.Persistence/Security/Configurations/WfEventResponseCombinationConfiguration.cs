using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class WfEventResponseCombinationConfiguration : IEntityTypeConfiguration<WfEventResponseCombination>
{
    public void Configure(EntityTypeBuilder<WfEventResponseCombination> builder)
    {
        builder.ToTable("wf_event_response_combination", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.FunctionName).HasColumnName("function_name");
        builder.Property(x => x.PredecessorType).HasColumnName("predecessor_type");
        builder.Property(x => x.PredecessorFunctionName).HasColumnName("predecessor_function_name");
    }
}
