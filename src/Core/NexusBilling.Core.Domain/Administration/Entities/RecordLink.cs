using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class RecordLink : Entity
{
    private RecordLink() { }

    public TenantIdentifier TenantId { get; private set; }
    public int LinkId { get; private set; }
    public string RecordId { get; private set; }
    public string Url1 { get; private set; }
    public string Url2 { get; private set; }
    public string Url3 { get; private set; }
    public string Url4 { get; private set; }
    public string Description { get; private set; }
    public short Type { get; private set; }
    public byte[]? Note { get; private set; }
    public DateTime? Created { get; private set; }
    public string UserId { get; private set; }
    public string Company { get; private set; }
    public bool Notify { get; private set; }
    public string ToUserId { get; private set; }

    public static OperationResult<RecordLink, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<RecordLink, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new RecordLink()
        {
            TenantId = tenantId
        };
        return OperationResult<RecordLink, DomainError>.Ok(entity);
    }
}
