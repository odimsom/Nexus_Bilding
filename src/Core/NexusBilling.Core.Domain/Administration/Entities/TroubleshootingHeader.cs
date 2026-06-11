using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TroubleshootingHeader : Entity
{
    private TroubleshootingHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string NoSeries { get; private set; }

    public static OperationResult<TroubleshootingHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TroubleshootingHeader, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TroubleshootingHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<TroubleshootingHeader, DomainError>.Ok(entity);
    }
}
