using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaJournalSetup : Entity
{
    private FaJournalSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string DepreciationBookCode { get; private set; }
    public string UserId { get; private set; }
    public string FaJnlTemplateName { get; private set; }
    public string FaJnlBatchName { get; private set; }
    public string GenJnlTemplateName { get; private set; }
    public string GenJnlBatchName { get; private set; }
    public string InsuranceJnlTemplateName { get; private set; }
    public string InsuranceJnlBatchName { get; private set; }

    public static OperationResult<FaJournalSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaJournalSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaJournalSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<FaJournalSetup, DomainError>.Ok(entity);
    }
}
