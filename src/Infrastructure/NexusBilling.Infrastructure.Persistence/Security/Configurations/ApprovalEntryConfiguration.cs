using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class ApprovalEntryConfiguration : IEntityTypeConfiguration<ApprovalEntry>
{
    public void Configure(EntityTypeBuilder<ApprovalEntry> builder)
    {
        builder.ToTable("approval_entry", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.SequenceNo).HasColumnName("sequence_no");
        builder.Property(x => x.ApprovalCode).HasColumnName("approval_code");
        builder.Property(x => x.SenderId).HasColumnName("sender_id");
        builder.Property(x => x.SalespersPurchCode).HasColumnName("salespers_purch_code");
        builder.Property(x => x.ApproverId).HasColumnName("approver_id");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.DateTimeSentForApproval).HasColumnName("date_time_sent_for_approval");
        builder.Property(x => x.LastDateTimeModified).HasColumnName("last_date_time_modified");
        builder.Property(x => x.LastModifiedByUserId).HasColumnName("last_modified_by_user_id");
        builder.Property(x => x.DueDate).HasColumnName("due_date");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.AmountLcy).HasColumnName("amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.ApprovalType).HasColumnName("approval_type");
        builder.Property(x => x.LimitType).HasColumnName("limit_type");
        builder.Property(x => x.AvailableCreditLimitLcy).HasColumnName("available_credit_limit_lcy").HasPrecision(18, 5);
        builder.Property(x => x.RecordIdToApprove).HasColumnName("record_id_to_approve");
        builder.Property(x => x.DelegationDateFormula).HasColumnName("delegation_date_formula");
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.WorkflowStepInstanceId).HasColumnName("workflow_step_instance_id");
    }
}
