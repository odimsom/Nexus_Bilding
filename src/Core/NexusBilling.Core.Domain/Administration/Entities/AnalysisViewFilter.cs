using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AnalysisViewFilter : Entity
{
    private AnalysisViewFilter() { }

    public TenantIdentifier TenantId { get; private set; }
    public string AnalysisViewCode { get; private set; }
    public string DimensionCode { get; private set; }
    public string DimensionValueFilter { get; private set; }

    public static OperationResult<AnalysisViewFilter, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AnalysisViewFilter, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AnalysisViewFilter()
        {
            TenantId = tenantId
        };
        return OperationResult<AnalysisViewFilter, DomainError>.Ok(entity);
    }
}
