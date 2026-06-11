using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class ShippingAgent : Entity
{
    private ShippingAgent() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string InternetAddress { get; private set; }
    public string AccountNo { get; private set; }

    public static OperationResult<ShippingAgent, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ShippingAgent, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ShippingAgent()
        {
            TenantId = tenantId
        };
        return OperationResult<ShippingAgent, DomainError>.Ok(entity);
    }
}
