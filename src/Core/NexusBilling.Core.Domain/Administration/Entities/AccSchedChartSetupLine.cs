using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AccSchedChartSetupLine : Entity
{
    private AccSchedChartSetupLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public string Name { get; private set; }
    public string AccountScheduleName { get; private set; }
    public int AccountScheduleLineNo { get; private set; }
    public string ColumnLayoutName { get; private set; }
    public int ColumnLayoutLineNo { get; private set; }
    public string OriginalMeasureName { get; private set; }
    public string MeasureName { get; private set; }
    public string MeasureValue { get; private set; }
    public short ChartType { get; private set; }

    public static OperationResult<AccSchedChartSetupLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AccSchedChartSetupLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AccSchedChartSetupLine()
        {
            TenantId = tenantId
        };
        return OperationResult<AccSchedChartSetupLine, DomainError>.Ok(entity);
    }
}
