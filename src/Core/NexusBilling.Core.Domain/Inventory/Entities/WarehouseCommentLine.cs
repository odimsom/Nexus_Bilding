using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WarehouseCommentLine : Entity
{
    private WarehouseCommentLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short TableName { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public int LineNo { get; private set; }
    public DateTime? Date { get; private set; }
    public string Code { get; private set; }
    public string Comment { get; private set; }

    public static OperationResult<WarehouseCommentLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WarehouseCommentLine, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WarehouseCommentLine()
        {
            TenantId = tenantId
        };
        return OperationResult<WarehouseCommentLine, DomainError>.Ok(entity);
    }
}
