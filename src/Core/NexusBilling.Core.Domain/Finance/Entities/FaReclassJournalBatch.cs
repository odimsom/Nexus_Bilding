using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaReclassJournalBatch : Entity
{
    private FaReclassJournalBatch() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public static OperationResult<FaReclassJournalBatch, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaReclassJournalBatch, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaReclassJournalBatch()
        {
            TenantId = tenantId
        };
        return OperationResult<FaReclassJournalBatch, DomainError>.Ok(entity);
    }
}
