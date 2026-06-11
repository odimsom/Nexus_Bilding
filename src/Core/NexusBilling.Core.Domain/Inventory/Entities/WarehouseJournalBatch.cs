using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WarehouseJournalBatch : Entity
{
    private WarehouseJournalBatch() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ReasonCode { get; private set; }
    public string NoSeries { get; private set; }
    public string RegisteringNoSeries { get; private set; }
    public string LocationCode { get; private set; }
    public string AssignedUserId { get; private set; }

    public static OperationResult<WarehouseJournalBatch, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WarehouseJournalBatch, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WarehouseJournalBatch()
        {
            TenantId = tenantId
        };
        return OperationResult<WarehouseJournalBatch, DomainError>.Ok(entity);
    }
}
