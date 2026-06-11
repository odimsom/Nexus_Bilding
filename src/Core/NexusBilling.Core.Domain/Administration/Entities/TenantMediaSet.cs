using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TenantMediaSet : Entity
{
    private TenantMediaSet() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid IdNav { get; private set; }
    public Guid MediaId { get; private set; }
    public string CompanyName { get; private set; }
    public long MediaIndex { get; private set; }

    public static OperationResult<TenantMediaSet, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TenantMediaSet, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TenantMediaSet()
        {
            TenantId = tenantId
        };
        return OperationResult<TenantMediaSet, DomainError>.Ok(entity);
    }
}
