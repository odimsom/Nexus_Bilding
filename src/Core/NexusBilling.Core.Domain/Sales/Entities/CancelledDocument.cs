using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class CancelledDocument : Entity
{
    private CancelledDocument() { }

    public TenantIdentifier TenantId { get; private set; }
    public int SourceId { get; private set; }
    public string CancelledDocNo { get; private set; }
    public string CancelledByDocNo { get; private set; }

    public static OperationResult<CancelledDocument, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CancelledDocument, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CancelledDocument()
        {
            TenantId = tenantId
        };
        return OperationResult<CancelledDocument, DomainError>.Ok(entity);
    }
}
