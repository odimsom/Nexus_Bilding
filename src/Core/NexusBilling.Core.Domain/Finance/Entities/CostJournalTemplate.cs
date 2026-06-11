using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class CostJournalTemplate : Entity
{
    private CostJournalTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ReasonCode { get; private set; }
    public string SourceCode { get; private set; }
    public int PostingReportId { get; private set; }

    public static OperationResult<CostJournalTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CostJournalTemplate, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CostJournalTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<CostJournalTemplate, DomainError>.Ok(entity);
    }
}
