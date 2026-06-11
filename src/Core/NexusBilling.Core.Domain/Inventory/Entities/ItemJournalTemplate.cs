using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemJournalTemplate : Entity
{
    private ItemJournalTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int TestReportId { get; private set; }
    public int PageId { get; private set; }
    public int PostingReportId { get; private set; }
    public bool ForcePostingReport { get; private set; }
    public short Type { get; private set; }
    public string SourceCode { get; private set; }
    public string ReasonCode { get; private set; }
    public bool Recurring { get; private set; }
    public string NoSeries { get; private set; }
    public string PostingNoSeries { get; private set; }
    public int WhseRegisterReportId { get; private set; }

    public static OperationResult<ItemJournalTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemJournalTemplate, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemJournalTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemJournalTemplate, DomainError>.Ok(entity);
    }
}
