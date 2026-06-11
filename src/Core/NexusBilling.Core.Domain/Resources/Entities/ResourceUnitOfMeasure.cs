using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class ResourceUnitOfMeasure : Entity
{
    private ResourceUnitOfMeasure() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ResourceNo { get; private set; }
    public string Code { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public bool RelatedToBaseUnitOfMeas { get; private set; }

    public static OperationResult<ResourceUnitOfMeasure, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ResourceUnitOfMeasure, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ResourceUnitOfMeasure()
        {
            TenantId = tenantId
        };
        return OperationResult<ResourceUnitOfMeasure, DomainError>.Ok(entity);
    }
}
