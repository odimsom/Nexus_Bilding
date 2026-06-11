using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobPostingGroup : Entity
{
    private JobPostingGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string WipCostsAccount { get; private set; }
    public string WipAccruedCostsAccount { get; private set; }
    public string JobCostsAppliedAccount { get; private set; }
    public string JobCostsAdjustmentAccount { get; private set; }
    public string GLExpenseAccContract { get; private set; }
    public string JobSalesAdjustmentAccount { get; private set; }
    public string WipAccruedSalesAccount { get; private set; }
    public string WipInvoicedSalesAccount { get; private set; }
    public string JobSalesAppliedAccount { get; private set; }
    public string RecognizedCostsAccount { get; private set; }
    public string RecognizedSalesAccount { get; private set; }
    public string ItemCostsAppliedAccount { get; private set; }
    public string ResourceCostsAppliedAccount { get; private set; }
    public string GLCostsAppliedAccount { get; private set; }

    public static OperationResult<JobPostingGroup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobPostingGroup, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobPostingGroup()
        {
            TenantId = tenantId
        };
        return OperationResult<JobPostingGroup, DomainError>.Ok(entity);
    }
}
