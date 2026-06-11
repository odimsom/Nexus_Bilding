using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class ResourcePrice : Entity
{
    private ResourcePrice() { }

    public TenantIdentifier TenantId { get; private set; }
    public short Type { get; private set; }
    public string Code { get; private set; }
    public string WorkTypeCode { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string CurrencyCode { get; private set; }

    public static OperationResult<ResourcePrice, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ResourcePrice, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ResourcePrice()
        {
            TenantId = tenantId
        };
        return OperationResult<ResourcePrice, DomainError>.Ok(entity);
    }
}
