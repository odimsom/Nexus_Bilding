using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceDocumentLog : Entity
{
    private ServiceDocumentLog() { }

    public TenantIdentifier TenantId { get; private set; }
    public string DocumentNo { get; private set; }
    public int EntryNo { get; private set; }
    public int EventNo { get; private set; }
    public int ServiceItemLineNo { get; private set; }
    public string After { get; private set; }
    public string Before { get; private set; }
    public DateTime? ChangeDate { get; private set; }
    public string ChangeTime { get; private set; }
    public string UserId { get; private set; }
    public short DocumentType { get; private set; }

    public static OperationResult<ServiceDocumentLog, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceDocumentLog, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceDocumentLog()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceDocumentLog, DomainError>.Ok(entity);
    }
}
