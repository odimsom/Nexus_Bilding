using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class InsuranceJournalBatch : Entity
{
    private InsuranceJournalBatch() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ReasonCode { get; private set; }
    public string NoSeries { get; private set; }
    public string PostingNoSeries { get; private set; }

    public static OperationResult<InsuranceJournalBatch, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<InsuranceJournalBatch, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new InsuranceJournalBatch()
        {
            TenantId = tenantId
        };
        return OperationResult<InsuranceJournalBatch, DomainError>.Ok(entity);
    }
}
