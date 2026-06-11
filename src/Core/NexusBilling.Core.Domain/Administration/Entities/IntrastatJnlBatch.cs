using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class IntrastatJnlBatch : Entity
{
    private IntrastatJnlBatch() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool Reported { get; private set; }
    public string StatisticsPeriod { get; private set; }
    public bool AmountsInAddCurrency { get; private set; }
    public string CurrencyIdentifier { get; private set; }

    public static OperationResult<IntrastatJnlBatch, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IntrastatJnlBatch, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IntrastatJnlBatch()
        {
            TenantId = tenantId
        };
        return OperationResult<IntrastatJnlBatch, DomainError>.Ok(entity);
    }
}
