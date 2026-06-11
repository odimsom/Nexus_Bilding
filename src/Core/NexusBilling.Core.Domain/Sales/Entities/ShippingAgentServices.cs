using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class ShippingAgentServices : Entity
{
    private ShippingAgentServices() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ShippingAgentCode { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string ShippingTime { get; private set; }
    public string BaseCalendarCode { get; private set; }

    public static OperationResult<ShippingAgentServices, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ShippingAgentServices, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ShippingAgentServices()
        {
            TenantId = tenantId
        };
        return OperationResult<ShippingAgentServices, DomainError>.Ok(entity);
    }
}
