using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DocumentService : Entity
{
    private DocumentService() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ServiceId { get; private set; }
    public string Description { get; private set; }
    public string Location { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public string DocumentRepository { get; private set; }
    public string Folder { get; private set; }

    public static OperationResult<DocumentService, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DocumentService, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DocumentService()
        {
            TenantId = tenantId
        };
        return OperationResult<DocumentService, DomainError>.Ok(entity);
    }
}
