using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ElectronicDocumentFormat : Entity
{
    private ElectronicDocumentFormat() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public short Usage { get; private set; }
    public string Description { get; private set; }
    public int CodeunitId { get; private set; }
    public int DeliveryCodeunitId { get; private set; }

    public static OperationResult<ElectronicDocumentFormat, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ElectronicDocumentFormat, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ElectronicDocumentFormat()
        {
            TenantId = tenantId
        };
        return OperationResult<ElectronicDocumentFormat, DomainError>.Ok(entity);
    }
}
