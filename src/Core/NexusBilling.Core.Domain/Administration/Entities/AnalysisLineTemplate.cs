using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AnalysisLineTemplate : Entity
{
    private AnalysisLineTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public short AnalysisArea { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string DefaultColumnTemplateName { get; private set; }
    public string ItemAnalysisViewCode { get; private set; }

    public static OperationResult<AnalysisLineTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AnalysisLineTemplate, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AnalysisLineTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<AnalysisLineTemplate, DomainError>.Ok(entity);
    }
}
