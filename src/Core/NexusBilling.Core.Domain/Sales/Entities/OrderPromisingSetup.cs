using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class OrderPromisingSetup : Entity
{
    private OrderPromisingSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string OffsetTime { get; private set; }
    public string OrderPromisingNos { get; private set; }
    public string OrderPromisingTemplate { get; private set; }
    public string OrderPromisingWorksheet { get; private set; }

    public static OperationResult<OrderPromisingSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<OrderPromisingSetup, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new OrderPromisingSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<OrderPromisingSetup, DomainError>.Ok(entity);
    }
}
