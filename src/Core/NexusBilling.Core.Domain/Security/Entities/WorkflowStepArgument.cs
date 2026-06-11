using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class WorkflowStepArgument : Entity
{
    private WorkflowStepArgument() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid IdNav { get; private set; }
    public short Type { get; private set; }
    public string GeneralJournalTemplateName { get; private set; }
    public string GeneralJournalBatchName { get; private set; }
    public string NotificationUserId { get; private set; }
    public string ResponseFunctionName { get; private set; }
    public int LinkTargetPage { get; private set; }
    public string CustomLink { get; private set; }
    public byte[]? EventConditions { get; private set; }
    public short ApproverType { get; private set; }
    public short ApproverLimitType { get; private set; }
    public string WorkflowUserGroupCode { get; private set; }
    public string DueDateFormula { get; private set; }
    public string Message { get; private set; }
    public short DelegateAfter { get; private set; }
    public bool ShowConfirmationMessage { get; private set; }
    public int TableNo { get; private set; }
    public int FieldNo { get; private set; }
    public string ApproverUserId { get; private set; }

    public static OperationResult<WorkflowStepArgument, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WorkflowStepArgument, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WorkflowStepArgument()
        {
            TenantId = tenantId
        };
        return OperationResult<WorkflowStepArgument, DomainError>.Ok(entity);
    }
}
