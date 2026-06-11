using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceContractAccountGroup : Entity
{
    private ServiceContractAccountGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string NonPrepaidContractAcc { get; private set; }
    public string PrepaidContractAcc { get; private set; }

    public static OperationResult<ServiceContractAccountGroup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceContractAccountGroup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceContractAccountGroup()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceContractAccountGroup, DomainError>.Ok(entity);
    }
}
