using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class MainAssetComponent : Entity
{
    private MainAssetComponent() { }

    public TenantIdentifier TenantId { get; private set; }
    public string MainAssetNo { get; private set; }
    public string FaNo { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<MainAssetComponent, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<MainAssetComponent, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new MainAssetComponent()
        {
            TenantId = tenantId
        };
        return OperationResult<MainAssetComponent, DomainError>.Ok(entity);
    }
}
