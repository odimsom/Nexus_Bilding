using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AnalysisReportChartSetup : Entity
{
    private AnalysisReportChartSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public string Name { get; private set; }
    public short AnalysisArea { get; private set; }
    public string AnalysisReportName { get; private set; }
    public string AnalysisLineTemplateName { get; private set; }
    public string AnalysisColumnTemplateName { get; private set; }
    public short BaseXAxisOn { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public short PeriodLength { get; private set; }
    public int NoOfPeriods { get; private set; }
    public bool LastViewed { get; private set; }

    public static OperationResult<AnalysisReportChartSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AnalysisReportChartSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AnalysisReportChartSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<AnalysisReportChartSetup, DomainError>.Ok(entity);
    }
}
