using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceDocumentRegister : Entity
{
    private ServiceDocumentRegister() { }

    public TenantIdentifier TenantId { get; private set; }
    public short SourceDocumentType { get; private set; }
    public string SourceDocumentNo { get; private set; }
    public short DestinationDocumentType { get; private set; }
    public string DestinationDocumentNo { get; private set; }

    public static OperationResult<ServiceDocumentRegister, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceDocumentRegister, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceDocumentRegister()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceDocumentRegister, DomainError>.Ok(entity);
    }
}
