using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class UserMetadata : Entity
{
    private UserMetadata() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid UserSid { get; private set; }
    public int PageId { get; private set; }
    public DateTime? Date { get; private set; }
    public string Time { get; private set; }
    public string PersonalizationId { get; private set; }
    public byte[]? PageMetadataDelta { get; private set; }

    public static OperationResult<UserMetadata, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<UserMetadata, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new UserMetadata()
        {
            TenantId = tenantId
        };
        return OperationResult<UserMetadata, DomainError>.Ok(entity);
    }
}
