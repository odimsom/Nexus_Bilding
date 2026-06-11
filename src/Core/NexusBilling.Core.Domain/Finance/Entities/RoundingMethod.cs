using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class RoundingMethod : Entity
{
    private RoundingMethod() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public decimal MinimumAmount { get; private set; }
    public decimal AmountAddedBefore { get; private set; }
    public short Type { get; private set; }
    public decimal Precision { get; private set; }
    public decimal AmountAddedAfter { get; private set; }

    public static OperationResult<RoundingMethod, DomainError> Create(
        TenantIdentifier tenantId,
        string code,
        decimal minimumAmount,
        decimal amountAddedBefore,
        short type,
        decimal precision,
        decimal amountAddedAfter)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<RoundingMethod, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(code))
            return OperationResult<RoundingMethod, DomainError>.Fail(DomainError.Validation("finance.code_required", "El campo code es obligatorio."));

        var entity = new RoundingMethod()
        {
            TenantId = tenantId,
            Code = code.Trim(),
            MinimumAmount = minimumAmount,
            AmountAddedBefore = amountAddedBefore,
            Type = type,
            Precision = precision,
            AmountAddedAfter = amountAddedAfter,
        };

        return OperationResult<RoundingMethod, DomainError>.Ok(entity);
    }
}
