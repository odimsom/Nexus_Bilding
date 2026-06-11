using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemRegister : Entity
{
    private ItemRegister() { }

    public TenantIdentifier TenantId { get; private set; }
    public int No { get; private set; }
    public int FromEntryNo { get; private set; }
    public int ToEntryNo { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public string SourceCode { get; private set; }
    public string UserId { get; private set; }
    public string JournalBatchName { get; private set; }
    public int FromPhysInventoryEntryNo { get; private set; }
    public int ToPhysInventoryEntryNo { get; private set; }
    public int FromValueEntryNo { get; private set; }
    public int ToValueEntryNo { get; private set; }
    public int FromCapacityEntryNo { get; private set; }
    public int ToCapacityEntryNo { get; private set; }

    public static OperationResult<ItemRegister, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemRegister, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemRegister()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemRegister, DomainError>.Ok(entity);
    }
}
