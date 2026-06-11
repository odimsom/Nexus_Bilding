using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceStatusPrioritySetup : Entity
{
    private ServiceStatusPrioritySetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public short ServiceOrderStatus { get; private set; }
    public short Priority { get; private set; }

    public static OperationResult<ServiceStatusPrioritySetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceStatusPrioritySetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceStatusPrioritySetup()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceStatusPrioritySetup, DomainError>.Ok(entity);
    }
}
