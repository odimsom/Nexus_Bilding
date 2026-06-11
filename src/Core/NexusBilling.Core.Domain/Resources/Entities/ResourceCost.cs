using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class ResourceCost : Entity
{
    private ResourceCost() { }

    public TenantIdentifier TenantId { get; private set; }
    public short Type { get; private set; }
    public string Code { get; private set; }
    public string WorkTypeCode { get; private set; }
    public short CostType { get; private set; }
    public decimal DirectUnitCost { get; private set; }
    public decimal UnitCost { get; private set; }

    public static OperationResult<ResourceCost, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ResourceCost, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ResourceCost()
        {
            TenantId = tenantId
        };
        return OperationResult<ResourceCost, DomainError>.Ok(entity);
    }
}
