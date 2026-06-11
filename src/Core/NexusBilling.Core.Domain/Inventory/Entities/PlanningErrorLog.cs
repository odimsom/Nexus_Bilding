using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class PlanningErrorLog : Entity
{
    private PlanningErrorLog() { }

    public TenantIdentifier TenantId { get; private set; }
    public string WorksheetTemplateName { get; private set; }
    public string JournalBatchName { get; private set; }
    public int EntryNo { get; private set; }
    public string ItemNo { get; private set; }
    public string ErrorDescription { get; private set; }
    public int TableId { get; private set; }
    public string TablePosition { get; private set; }

    public static OperationResult<PlanningErrorLog, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PlanningErrorLog, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PlanningErrorLog()
        {
            TenantId = tenantId
        };
        return OperationResult<PlanningErrorLog, DomainError>.Ok(entity);
    }
}
