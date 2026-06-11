using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class MediaRepository : Entity
{
    private MediaRepository() { }

    public TenantIdentifier TenantId { get; private set; }
    public string FileName { get; private set; }
    public string DisplayTarget { get; private set; }
    public Guid Image { get; private set; }

    public static OperationResult<MediaRepository, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<MediaRepository, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new MediaRepository()
        {
            TenantId = tenantId
        };
        return OperationResult<MediaRepository, DomainError>.Ok(entity);
    }
}
