using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WarehouseJournalTemplate : Entity
{
    private WarehouseJournalTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int TestReportId { get; private set; }
    public int PageId { get; private set; }
    public int RegisteringReportId { get; private set; }
    public bool ForceRegisteringReport { get; private set; }
    public short Type { get; private set; }
    public string SourceCode { get; private set; }
    public string ReasonCode { get; private set; }
    public string NoSeries { get; private set; }
    public string RegisteringNoSeries { get; private set; }

    public static OperationResult<WarehouseJournalTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WarehouseJournalTemplate, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WarehouseJournalTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<WarehouseJournalTemplate, DomainError>.Ok(entity);
    }
}
