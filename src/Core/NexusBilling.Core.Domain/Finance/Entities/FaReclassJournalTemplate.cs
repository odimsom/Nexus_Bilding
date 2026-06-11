using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaReclassJournalTemplate : Entity
{
    private FaReclassJournalTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int PageId { get; private set; }

    public static OperationResult<FaReclassJournalTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaReclassJournalTemplate, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaReclassJournalTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<FaReclassJournalTemplate, DomainError>.Ok(entity);
    }
}
