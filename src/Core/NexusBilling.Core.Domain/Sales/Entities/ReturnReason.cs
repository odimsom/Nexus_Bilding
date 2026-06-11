using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class ReturnReason : Entity
{
    private ReturnReason() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string DefaultLocationCode { get; private set; }
    public bool InventoryValueZero { get; private set; }

    public static OperationResult<ReturnReason, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ReturnReason, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ReturnReason()
        {
            TenantId = tenantId
        };
        return OperationResult<ReturnReason, DomainError>.Ok(entity);
    }
}
