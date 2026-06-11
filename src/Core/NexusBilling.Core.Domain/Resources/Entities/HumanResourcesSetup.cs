using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class HumanResourcesSetup : Entity
{
    private HumanResourcesSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string EmployeeNos { get; private set; }
    public string BaseUnitOfMeasure { get; private set; }

    public static OperationResult<HumanResourcesSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<HumanResourcesSetup, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new HumanResourcesSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<HumanResourcesSetup, DomainError>.Ok(entity);
    }
}
