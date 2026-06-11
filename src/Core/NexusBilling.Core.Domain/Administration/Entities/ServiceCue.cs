using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceCue : Entity
{
    private ServiceCue() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }

    public static OperationResult<ServiceCue, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceCue, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceCue()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceCue, DomainError>.Ok(entity);
    }
}
