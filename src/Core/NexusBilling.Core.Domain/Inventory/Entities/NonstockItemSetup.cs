using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class NonstockItemSetup : Entity
{
    private NonstockItemSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public short NoFormat { get; private set; }
    public string NoFormatSeparator { get; private set; }

    public static OperationResult<NonstockItemSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<NonstockItemSetup, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new NonstockItemSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<NonstockItemSetup, DomainError>.Ok(entity);
    }
}
