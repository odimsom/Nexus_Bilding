using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemChargeAssignmentPurch : Entity
{
    private ItemChargeAssignmentPurch() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public int DocumentLineNo { get; private set; }
    public int LineNo { get; private set; }
    public string ItemChargeNo { get; private set; }
    public string ItemNo { get; private set; }
    public string Description { get; private set; }
    public decimal QtyToAssign { get; private set; }
    public decimal QtyAssigned { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal AmountToAssign { get; private set; }
    public short AppliesToDocType { get; private set; }
    public string AppliesToDocNo { get; private set; }
    public int AppliesToDocLineNo { get; private set; }
    public decimal AppliesToDocLineAmount { get; private set; }

    public static OperationResult<ItemChargeAssignmentPurch, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemChargeAssignmentPurch, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemChargeAssignmentPurch()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemChargeAssignmentPurch, DomainError>.Ok(entity);
    }
}
