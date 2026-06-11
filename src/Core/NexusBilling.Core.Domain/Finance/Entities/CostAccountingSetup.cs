using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class CostAccountingSetup : Entity
{
    private CostAccountingSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public DateTime? StartingDateForGLTransfer { get; private set; }
    public short AlignGLAccount { get; private set; }
    public short AlignCostCenterDimension { get; private set; }
    public short AlignCostObjectDimension { get; private set; }
    public string LastAllocationId { get; private set; }
    public string LastAllocationDocNo { get; private set; }
    public bool AutoTransferFromGL { get; private set; }
    public bool CheckGLPostings { get; private set; }
    public string CostCenterDimension { get; private set; }
    public string CostObjectDimension { get; private set; }

    public static OperationResult<CostAccountingSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CostAccountingSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CostAccountingSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<CostAccountingSetup, DomainError>.Ok(entity);
    }
}
