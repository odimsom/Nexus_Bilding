using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TroubleshootingSetup : Entity
{
    private TroubleshootingSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string TroubleshootingNo { get; private set; }

    public static OperationResult<TroubleshootingSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TroubleshootingSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TroubleshootingSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<TroubleshootingSetup, DomainError>.Ok(entity);
    }
}
