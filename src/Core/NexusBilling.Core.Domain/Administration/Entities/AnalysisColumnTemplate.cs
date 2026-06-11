using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AnalysisColumnTemplate : Entity
{
    private AnalysisColumnTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public short AnalysisArea { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<AnalysisColumnTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AnalysisColumnTemplate, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AnalysisColumnTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<AnalysisColumnTemplate, DomainError>.Ok(entity);
    }
}
