using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class LastUsedChart : Entity
{
    private LastUsedChart() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Uid { get; private set; }
    public int CodeUnitId { get; private set; }
    public string ChartName { get; private set; }

    public static OperationResult<LastUsedChart, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<LastUsedChart, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new LastUsedChart()
        {
            TenantId = tenantId
        };
        return OperationResult<LastUsedChart, DomainError>.Ok(entity);
    }
}
