using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AssistedSetupLog : Entity
{
    private AssistedSetupLog() { }

    public TenantIdentifier TenantId { get; private set; }
    public int No { get; private set; }
    public int EnteryNo { get; private set; }
    public DateTime? DateTime { get; private set; }
    public short InvokedAction { get; private set; }

    public static OperationResult<AssistedSetupLog, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AssistedSetupLog, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AssistedSetupLog()
        {
            TenantId = tenantId
        };
        return OperationResult<AssistedSetupLog, DomainError>.Ok(entity);
    }
}
