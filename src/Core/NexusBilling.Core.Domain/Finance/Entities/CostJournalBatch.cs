using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class CostJournalBatch : Entity
{
    private CostJournalBatch() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ReasonCode { get; private set; }
    public string BalCostTypeNo { get; private set; }
    public string BalCostCenterCode { get; private set; }
    public string BalCostObjectCode { get; private set; }
    public bool DeleteAfterPosting { get; private set; }

    public static OperationResult<CostJournalBatch, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CostJournalBatch, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CostJournalBatch()
        {
            TenantId = tenantId
        };
        return OperationResult<CostJournalBatch, DomainError>.Ok(entity);
    }
}
