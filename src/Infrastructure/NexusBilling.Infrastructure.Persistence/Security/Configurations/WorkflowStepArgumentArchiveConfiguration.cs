using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class WorkflowStepArgumentArchiveConfiguration : IEntityTypeConfiguration<WorkflowStepArgumentArchive>
{
    public void Configure(EntityTypeBuilder<WorkflowStepArgumentArchive> builder)
    {
        builder.ToTable("workflow_step_argument_archive", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.GeneralJournalTemplateName).HasColumnName("general_journal_template_name");
        builder.Property(x => x.GeneralJournalBatchName).HasColumnName("general_journal_batch_name");
        builder.Property(x => x.NotificationUserId).HasColumnName("notification_user_id");
        builder.Property(x => x.ResponseFunctionName).HasColumnName("response_function_name");
        builder.Property(x => x.LinkTargetPage).HasColumnName("link_target_page");
        builder.Property(x => x.CustomLink).HasColumnName("custom_link");
        builder.Property(x => x.EventConditions).HasColumnName("event_conditions");
        builder.Property(x => x.ApproverType).HasColumnName("approver_type");
        builder.Property(x => x.ApproverLimitType).HasColumnName("approver_limit_type");
        builder.Property(x => x.WorkflowUserGroupCode).HasColumnName("workflow_user_group_code");
        builder.Property(x => x.DueDateFormula).HasColumnName("due_date_formula");
        builder.Property(x => x.Message).HasColumnName("message");
        builder.Property(x => x.DelegateAfter).HasColumnName("delegate_after");
        builder.Property(x => x.ShowConfirmationMessage).HasColumnName("show_confirmation_message");
        builder.Property(x => x.TableNo).HasColumnName("table_no");
        builder.Property(x => x.FieldNo).HasColumnName("field_no");
        builder.Property(x => x.ApproverUserId).HasColumnName("approver_user_id");
        builder.Property(x => x.OriginalRecordId).HasColumnName("original_record_id");
    }
}
