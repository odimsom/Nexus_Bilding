using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ChartDefinition : Entity
{
    private ChartDefinition() { }

    public TenantIdentifier TenantId { get; private set; }
    public int CodeUnitId { get; private set; }
    public string ChartName { get; private set; }
    public bool Enabled { get; private set; }

    public static OperationResult<ChartDefinition, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ChartDefinition, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ChartDefinition()
        {
            TenantId = tenantId
        };
        return OperationResult<ChartDefinition, DomainError>.Ok(entity);
    }
}
