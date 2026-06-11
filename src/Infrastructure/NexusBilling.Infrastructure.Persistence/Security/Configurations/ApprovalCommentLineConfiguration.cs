using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class ApprovalCommentLineConfiguration : IEntityTypeConfiguration<ApprovalCommentLine>
{
    public void Configure(EntityTypeBuilder<ApprovalCommentLine> builder)
    {
        builder.ToTable("approval_comment_line", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.DateAndTime).HasColumnName("date_and_time");
        builder.Property(x => x.Comment).HasColumnName("comment");
        builder.Property(x => x.RecordIdToApprove).HasColumnName("record_id_to_approve");
        builder.Property(x => x.WorkflowStepInstanceId).HasColumnName("workflow_step_instance_id");
    }
}
