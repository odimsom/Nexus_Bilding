using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemBudgetName : Entity
{
    private ItemBudgetName() { }

    public TenantIdentifier TenantId { get; private set; }
    public short AnalysisArea { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool Blocked { get; private set; }
    public string BudgetDimension1Code { get; private set; }
    public string BudgetDimension2Code { get; private set; }
    public string BudgetDimension3Code { get; private set; }

    public static OperationResult<ItemBudgetName, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemBudgetName, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemBudgetName()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemBudgetName, DomainError>.Ok(entity);
    }
}
