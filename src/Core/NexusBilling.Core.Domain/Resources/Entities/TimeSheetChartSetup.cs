using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class TimeSheetChartSetup : Entity
{
    private TimeSheetChartSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public short ShowBy { get; private set; }
    public short MeasureType { get; private set; }

    public static OperationResult<TimeSheetChartSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TimeSheetChartSetup, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TimeSheetChartSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<TimeSheetChartSetup, DomainError>.Ok(entity);
    }
}
