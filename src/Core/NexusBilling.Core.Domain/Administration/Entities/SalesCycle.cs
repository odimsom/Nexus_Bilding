using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class SalesCycle : Entity
{
    private SalesCycle() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public short ProbabilityCalculation { get; private set; }
    public bool Blocked { get; private set; }

    public static OperationResult<SalesCycle, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesCycle, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalesCycle()
        {
            TenantId = tenantId
        };
        return OperationResult<SalesCycle, DomainError>.Ok(entity);
    }
}
