using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class StandardSalesCode : Entity
{
    private StandardSalesCode() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string CurrencyCode { get; private set; }

    public static OperationResult<StandardSalesCode, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<StandardSalesCode, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new StandardSalesCode()
        {
            TenantId = tenantId
        };
        return OperationResult<StandardSalesCode, DomainError>.Ok(entity);
    }
}
