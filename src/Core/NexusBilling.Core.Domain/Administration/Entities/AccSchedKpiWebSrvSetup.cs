using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AccSchedKpiWebSrvSetup : Entity
{
    private AccSchedKpiWebSrvSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public short ForecastedValuesStart { get; private set; }
    public string GLBudgetName { get; private set; }
    public short Period { get; private set; }
    public short ViewBy { get; private set; }
    public string WebServiceName { get; private set; }

    public static OperationResult<AccSchedKpiWebSrvSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AccSchedKpiWebSrvSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AccSchedKpiWebSrvSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<AccSchedKpiWebSrvSetup, DomainError>.Ok(entity);
    }
}
