using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class PlanningAssignment : Entity
{
    private PlanningAssignment() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ItemNo { get; private set; }
    public string VariantCode { get; private set; }
    public string LocationCode { get; private set; }
    public DateTime? LatestDate { get; private set; }
    public bool Inactive { get; private set; }
    public bool ActionMsgResponsePlanning { get; private set; }
    public bool NetChangePlanning { get; private set; }

    public static OperationResult<PlanningAssignment, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PlanningAssignment, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PlanningAssignment()
        {
            TenantId = tenantId
        };
        return OperationResult<PlanningAssignment, DomainError>.Ok(entity);
    }
}
