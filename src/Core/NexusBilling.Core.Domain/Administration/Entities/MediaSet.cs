using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class MediaSet : Entity
{
    private MediaSet() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid IdNav { get; private set; }
    public Guid MediaId { get; private set; }
    public string CompanyName { get; private set; }
    public long MediaIndex { get; private set; }

    public static OperationResult<MediaSet, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<MediaSet, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new MediaSet()
        {
            TenantId = tenantId
        };
        return OperationResult<MediaSet, DomainError>.Ok(entity);
    }
}
