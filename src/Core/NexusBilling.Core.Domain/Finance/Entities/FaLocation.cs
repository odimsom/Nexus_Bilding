using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaLocation : Entity
{
    private FaLocation() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }

    public static OperationResult<FaLocation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaLocation, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaLocation()
        {
            TenantId = tenantId
        };
        return OperationResult<FaLocation, DomainError>.Ok(entity);
    }
}
