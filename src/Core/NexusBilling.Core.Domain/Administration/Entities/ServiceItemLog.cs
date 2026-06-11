using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceItemLog : Entity
{
    private ServiceItemLog() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ServiceItemNo { get; private set; }
    public int EntryNo { get; private set; }
    public int EventNo { get; private set; }
    public string DocumentNo { get; private set; }
    public string After { get; private set; }
    public string Before { get; private set; }
    public DateTime? ChangeDate { get; private set; }
    public string ChangeTime { get; private set; }
    public string UserId { get; private set; }
    public short DocumentType { get; private set; }

    public static OperationResult<ServiceItemLog, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceItemLog, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceItemLog()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceItemLog, DomainError>.Ok(entity);
    }
}
