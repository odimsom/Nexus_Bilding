using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class CashFlowChartSetup : Entity
{
    private CashFlowChartSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public short PeriodLength { get; private set; }
    public short Show { get; private set; }
    public short StartDate { get; private set; }
    public short GroupBy { get; private set; }
    public short ChartType { get; private set; }

    public static OperationResult<CashFlowChartSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CashFlowChartSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CashFlowChartSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<CashFlowChartSetup, DomainError>.Ok(entity);
    }
}
