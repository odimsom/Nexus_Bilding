using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class CurrencyExchangeRate : Entity
{
    private CurrencyExchangeRate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string CurrencyCode { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public decimal ExchangeRateAmount { get; private set; }
    public decimal AdjustmentExchRateAmount { get; private set; }
    public string RelationalCurrencyCode { get; private set; }
    public decimal RelationalExchRateAmount { get; private set; }
    public short FixExchangeRateAmount { get; private set; }
    public decimal RelationalAdjmtExchRateAmt { get; private set; }

    public static OperationResult<CurrencyExchangeRate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CurrencyExchangeRate, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CurrencyExchangeRate()
        {
            TenantId = tenantId
        };
        return OperationResult<CurrencyExchangeRate, DomainError>.Ok(entity);
    }
}
