using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaJournalBatch : Entity
{
    private FaJournalBatch() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ReasonCode { get; private set; }
    public string NoSeries { get; private set; }
    public string PostingNoSeries { get; private set; }

    public static OperationResult<FaJournalBatch, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaJournalBatch, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaJournalBatch()
        {
            TenantId = tenantId
        };
        return OperationResult<FaJournalBatch, DomainError>.Ok(entity);
    }
}
