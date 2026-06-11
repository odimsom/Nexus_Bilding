using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class BinCreationWkshTemplate : Entity
{
    private BinCreationWkshTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int PageId { get; private set; }
    public short Type { get; private set; }

    public static OperationResult<BinCreationWkshTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BinCreationWkshTemplate, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BinCreationWkshTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<BinCreationWkshTemplate, DomainError>.Ok(entity);
    }
}
