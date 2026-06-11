using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class CommentLine : Entity
{
    private CommentLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short TableName { get; private set; }
    public string No { get; private set; }
    public int LineNo { get; private set; }
    public DateTime? Date { get; private set; }
    public string Code { get; private set; }
    public string Comment { get; private set; }

    public static OperationResult<CommentLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CommentLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CommentLine()
        {
            TenantId = tenantId
        };
        return OperationResult<CommentLine, DomainError>.Ok(entity);
    }
}
