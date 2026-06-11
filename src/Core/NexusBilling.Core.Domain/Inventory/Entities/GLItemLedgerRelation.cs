using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class GLItemLedgerRelation : Entity
{
    private GLItemLedgerRelation() { }

    public TenantIdentifier TenantId { get; private set; }
    public int GLEntryNo { get; private set; }
    public int ValueEntryNo { get; private set; }
    public int GLRegisterNo { get; private set; }

    public static OperationResult<GLItemLedgerRelation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GLItemLedgerRelation, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new GLItemLedgerRelation()
        {
            TenantId = tenantId
        };
        return OperationResult<GLItemLedgerRelation, DomainError>.Ok(entity);
    }
}
