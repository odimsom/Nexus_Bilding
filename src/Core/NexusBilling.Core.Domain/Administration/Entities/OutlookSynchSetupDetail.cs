using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class OutlookSynchSetupDetail : Entity
{
    private OutlookSynchSetupDetail() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public string SynchEntityCode { get; private set; }
    public int ElementNo { get; private set; }
    public int TableNo { get; private set; }

    public static OperationResult<OutlookSynchSetupDetail, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<OutlookSynchSetupDetail, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new OutlookSynchSetupDetail()
        {
            TenantId = tenantId
        };
        return OperationResult<OutlookSynchSetupDetail, DomainError>.Ok(entity);
    }
}
