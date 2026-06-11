using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TokenCache : Entity
{
    private TokenCache() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid UserSecurityId { get; private set; }
    public Guid UserUniqueId { get; private set; }
    public Guid TenantIdNav { get; private set; }
    public DateTime? CacheWriteTime { get; private set; }
    public byte[]? CacheData { get; private set; }

    public static OperationResult<TokenCache, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TokenCache, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TokenCache()
        {
            TenantId = tenantId
        };
        return OperationResult<TokenCache, DomainError>.Ok(entity);
    }
}
