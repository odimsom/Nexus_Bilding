using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class BinType : Entity
{
    private BinType() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public bool Receive { get; private set; }
    public bool Ship { get; private set; }
    public bool PutAway { get; private set; }
    public bool Pick { get; private set; }

    public static OperationResult<BinType, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BinType, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BinType()
        {
            TenantId = tenantId
        };
        return OperationResult<BinType, DomainError>.Ok(entity);
    }
}
