using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class StandardCostWorksheetName : Entity
{
    private StandardCostWorksheetName() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<StandardCostWorksheetName, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<StandardCostWorksheetName, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new StandardCostWorksheetName()
        {
            TenantId = tenantId
        };
        return OperationResult<StandardCostWorksheetName, DomainError>.Ok(entity);
    }
}
