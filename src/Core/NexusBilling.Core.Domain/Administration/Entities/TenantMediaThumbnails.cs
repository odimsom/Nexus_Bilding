using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TenantMediaThumbnails : Entity
{
    private TenantMediaThumbnails() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid IdNav { get; private set; }
    public Guid MediaId { get; private set; }
    public byte[]? Content { get; private set; }
    public string MimeType { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }
    public string CompanyName { get; private set; }
    public bool Embedded { get; private set; }

    public static OperationResult<TenantMediaThumbnails, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TenantMediaThumbnails, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TenantMediaThumbnails()
        {
            TenantId = tenantId
        };
        return OperationResult<TenantMediaThumbnails, DomainError>.Ok(entity);
    }
}
