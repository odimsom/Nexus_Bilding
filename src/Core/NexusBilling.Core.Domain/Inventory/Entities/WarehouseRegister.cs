using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WarehouseRegister : Entity
{
    private WarehouseRegister() { }

    public TenantIdentifier TenantId { get; private set; }
    public int No { get; private set; }
    public int FromEntryNo { get; private set; }
    public int ToEntryNo { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public string SourceCode { get; private set; }
    public string UserId { get; private set; }
    public string JournalBatchName { get; private set; }

    public static OperationResult<WarehouseRegister, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WarehouseRegister, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WarehouseRegister()
        {
            TenantId = tenantId
        };
        return OperationResult<WarehouseRegister, DomainError>.Ok(entity);
    }
}
