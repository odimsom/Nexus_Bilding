using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class PostedApprovalCommentLine : Entity
{
    private PostedApprovalCommentLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public int TableId { get; private set; }
    public string DocumentNo { get; private set; }
    public string UserId { get; private set; }
    public DateTime? DateAndTime { get; private set; }
    public string Comment { get; private set; }
    public string PostedRecordId { get; private set; }

    public static OperationResult<PostedApprovalCommentLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PostedApprovalCommentLine, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PostedApprovalCommentLine()
        {
            TenantId = tenantId
        };
        return OperationResult<PostedApprovalCommentLine, DomainError>.Ok(entity);
    }
}
