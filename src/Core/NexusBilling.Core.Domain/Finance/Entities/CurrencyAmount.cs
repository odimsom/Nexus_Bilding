using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class CurrencyAmount : Entity
{
    private CurrencyAmount() { }

    public TenantIdentifier TenantId { get; private set; }
    public string CurrencyCode { get; private set; }
    public DateTime? Date { get; private set; }
    public decimal Amount { get; private set; }

    public static OperationResult<CurrencyAmount, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CurrencyAmount, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CurrencyAmount()
        {
            TenantId = tenantId
        };
        return OperationResult<CurrencyAmount, DomainError>.Ok(entity);
    }
}
