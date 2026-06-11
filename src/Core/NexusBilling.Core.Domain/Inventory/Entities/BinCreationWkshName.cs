using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class BinCreationWkshName : Entity
{
    private BinCreationWkshName() { }

    public TenantIdentifier TenantId { get; private set; }
    public string WorksheetTemplateName { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string LocationCode { get; private set; }

    public static OperationResult<BinCreationWkshName, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BinCreationWkshName, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BinCreationWkshName()
        {
            TenantId = tenantId
        };
        return OperationResult<BinCreationWkshName, DomainError>.Ok(entity);
    }
}
