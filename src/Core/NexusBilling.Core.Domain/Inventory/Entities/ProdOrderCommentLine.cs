using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ProdOrderCommentLine : Entity
{
    private ProdOrderCommentLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short Status { get; private set; }
    public string ProdOrderNo { get; private set; }
    public int LineNo { get; private set; }
    public DateTime? Date { get; private set; }
    public string Code { get; private set; }
    public string Comment { get; private set; }

    public static OperationResult<ProdOrderCommentLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ProdOrderCommentLine, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ProdOrderCommentLine()
        {
            TenantId = tenantId
        };
        return OperationResult<ProdOrderCommentLine, DomainError>.Ok(entity);
    }
}
