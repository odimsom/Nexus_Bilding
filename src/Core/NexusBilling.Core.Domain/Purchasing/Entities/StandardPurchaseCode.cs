using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class StandardPurchaseCode : Entity
{
    private StandardPurchaseCode() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string CurrencyCode { get; private set; }

    public static OperationResult<StandardPurchaseCode, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<StandardPurchaseCode, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new StandardPurchaseCode()
        {
            TenantId = tenantId
        };
        return OperationResult<StandardPurchaseCode, DomainError>.Ok(entity);
    }
}
