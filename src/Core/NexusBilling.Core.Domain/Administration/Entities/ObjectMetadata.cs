using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ObjectMetadata : Entity
{
    private ObjectMetadata() { }

    public TenantIdentifier TenantId { get; private set; }
    public short ObjectType { get; private set; }
    public int ObjectId { get; private set; }
    public byte[]? Metadata { get; private set; }
    public byte[]? UserCode { get; private set; }
    public byte[]? UserAlCode { get; private set; }
    public int MetadataVersion { get; private set; }
    public string Hash { get; private set; }
    public string ObjectSubtype { get; private set; }
    public bool HasSubscribers { get; private set; }

    public static OperationResult<ObjectMetadata, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ObjectMetadata, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ObjectMetadata()
        {
            TenantId = tenantId
        };
        return OperationResult<ObjectMetadata, DomainError>.Ok(entity);
    }
}
