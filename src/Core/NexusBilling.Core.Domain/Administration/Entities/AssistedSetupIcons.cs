using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AssistedSetupIcons : Entity
{
    private AssistedSetupIcons() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public Guid Image { get; private set; }

    public static OperationResult<AssistedSetupIcons, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AssistedSetupIcons, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AssistedSetupIcons()
        {
            TenantId = tenantId
        };
        return OperationResult<AssistedSetupIcons, DomainError>.Ok(entity);
    }
}
