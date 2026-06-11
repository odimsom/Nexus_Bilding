using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TenantMedia : Entity
{
    private TenantMedia() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid IdNav { get; private set; }
    public string Description { get; private set; }
    public byte[]? Content { get; private set; }
    public string MimeType { get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set; }
    public string CompanyName { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public bool ProhibitCache { get; private set; }
    public string FileName { get; private set; }
    public string SecurityToken { get; private set; }
    public string CreatingUser { get; private set; }

    public static OperationResult<TenantMedia, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TenantMedia, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TenantMedia()
        {
            TenantId = tenantId
        };
        return OperationResult<TenantMedia, DomainError>.Ok(entity);
    }
}
