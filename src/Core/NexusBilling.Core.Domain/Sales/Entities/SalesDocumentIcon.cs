using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesDocumentIcon : Entity
{
    private SalesDocumentIcon() { }

    public TenantIdentifier TenantId { get; private set; }
    public short Type { get; private set; }
    public Guid Picture { get; private set; }

    public static OperationResult<SalesDocumentIcon, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalesDocumentIcon, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalesDocumentIcon()
        {
            TenantId = tenantId
        };
        return OperationResult<SalesDocumentIcon, DomainError>.Ok(entity);
    }
}
