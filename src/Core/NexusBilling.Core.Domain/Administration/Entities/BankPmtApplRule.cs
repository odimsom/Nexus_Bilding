using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class BankPmtApplRule : Entity
{
    private BankPmtApplRule() { }

    public TenantIdentifier TenantId { get; private set; }
    public short MatchConfidence { get; private set; }
    public int Priority { get; private set; }
    public short RelatedPartyMatched { get; private set; }
    public short DocNoExtDocNoMatched { get; private set; }
    public short AmountInclToleranceMatched { get; private set; }
    public int Score { get; private set; }

    public static OperationResult<BankPmtApplRule, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BankPmtApplRule, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BankPmtApplRule()
        {
            TenantId = tenantId
        };
        return OperationResult<BankPmtApplRule, DomainError>.Ok(entity);
    }
}
