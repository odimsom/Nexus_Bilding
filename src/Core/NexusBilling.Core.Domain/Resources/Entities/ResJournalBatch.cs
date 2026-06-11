using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class ResJournalBatch : Entity
{
    private ResJournalBatch() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ReasonCode { get; private set; }
    public string NoSeries { get; private set; }
    public string PostingNoSeries { get; private set; }

    public static OperationResult<ResJournalBatch, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ResJournalBatch, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ResJournalBatch()
        {
            TenantId = tenantId
        };
        return OperationResult<ResJournalBatch, DomainError>.Ok(entity);
    }
}
