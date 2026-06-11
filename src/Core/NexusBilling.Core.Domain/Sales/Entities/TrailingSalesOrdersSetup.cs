using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class TrailingSalesOrdersSetup : Entity
{
    private TrailingSalesOrdersSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public short PeriodLength { get; private set; }
    public short ShowOrders { get; private set; }
    public bool UseWorkDateAsBase { get; private set; }
    public short ValueToCalculate { get; private set; }
    public short ChartType { get; private set; }

    public static OperationResult<TrailingSalesOrdersSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TrailingSalesOrdersSetup, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TrailingSalesOrdersSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<TrailingSalesOrdersSetup, DomainError>.Ok(entity);
    }
}
