using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AnalysisReportName : Entity
{
    private AnalysisReportName() { }

    public TenantIdentifier TenantId { get; private set; }
    public short AnalysisArea { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string AnalysisLineTemplateName { get; private set; }
    public string AnalysisColumnTemplateName { get; private set; }

    public static OperationResult<AnalysisReportName, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AnalysisReportName, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AnalysisReportName()
        {
            TenantId = tenantId
        };
        return OperationResult<AnalysisReportName, DomainError>.Ok(entity);
    }
}
