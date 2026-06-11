using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesByCustGrpChartSetup : Entity
{
    private SalesByCustGrpChartSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public DateTime? StartDate { get; private set; }
    public short PeriodLength { get; private set; }

    public static OperationResult<SalesByCustGrpChartSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesByCustGrpChartSetup, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalesByCustGrpChartSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<SalesByCustGrpChartSetup, DomainError>.Ok(entity);
    }
}
