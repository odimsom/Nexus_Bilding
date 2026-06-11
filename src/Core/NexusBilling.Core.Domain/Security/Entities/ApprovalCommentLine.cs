using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class ApprovalCommentLine : Entity
{
    private ApprovalCommentLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public int TableId { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public string UserId { get; private set; }
    public DateTime? DateAndTime { get; private set; }
    public string Comment { get; private set; }
    public string RecordIdToApprove { get; private set; }
    public Guid WorkflowStepInstanceId { get; private set; }

    public static OperationResult<ApprovalCommentLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ApprovalCommentLine, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ApprovalCommentLine()
        {
            TenantId = tenantId
        };
        return OperationResult<ApprovalCommentLine, DomainError>.Ok(entity);
    }
}
