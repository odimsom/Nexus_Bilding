using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class O365SalesCue : Entity
{
    private O365SalesCue() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }

    public static OperationResult<O365SalesCue, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<O365SalesCue, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new O365SalesCue()
        {
            TenantId = tenantId
        };
        return OperationResult<O365SalesCue, DomainError>.Ok(entity);
    }
}
