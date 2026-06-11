using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AccSchedKpiWebSrvLine : Entity
{
    private AccSchedKpiWebSrvLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string AccScheduleName { get; private set; }

    public static OperationResult<AccSchedKpiWebSrvLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AccSchedKpiWebSrvLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AccSchedKpiWebSrvLine()
        {
            TenantId = tenantId
        };
        return OperationResult<AccSchedKpiWebSrvLine, DomainError>.Ok(entity);
    }
}
