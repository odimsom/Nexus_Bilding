using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class OutlookSynchUserSetup : Entity
{
    private OutlookSynchUserSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public string SynchEntityCode { get; private set; }
    public string Condition { get; private set; }
    public short SynchDirection { get; private set; }
    public DateTime? LastSynchTime { get; private set; }
    public Guid RecordGuid { get; private set; }

    public static OperationResult<OutlookSynchUserSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<OutlookSynchUserSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new OutlookSynchUserSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<OutlookSynchUserSetup, DomainError>.Ok(entity);
    }
}
