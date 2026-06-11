using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ResourceLocation : Entity
{
    private ResourceLocation() { }

    public TenantIdentifier TenantId { get; private set; }
    public string LocationCode { get; private set; }
    public string ResourceNo { get; private set; }
    public DateTime? StartingDate { get; private set; }

    public static OperationResult<ResourceLocation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ResourceLocation, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ResourceLocation()
        {
            TenantId = tenantId
        };
        return OperationResult<ResourceLocation, DomainError>.Ok(entity);
    }
}
