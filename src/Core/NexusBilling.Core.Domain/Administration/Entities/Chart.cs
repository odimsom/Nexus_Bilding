using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class Chart : Entity
{
    private Chart() { }

    public TenantIdentifier TenantId { get; private set; }
    public string IdNav { get; private set; }
    public string Name { get; private set; }
    public byte[]? Blob { get; private set; }

    public static OperationResult<Chart, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Chart, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Chart()
        {
            TenantId = tenantId
        };
        return OperationResult<Chart, DomainError>.Ok(entity);
    }
}
