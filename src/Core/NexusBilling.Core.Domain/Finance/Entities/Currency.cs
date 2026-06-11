using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class Currency : Entity
{
    private Currency() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public DateTime? LastDateAdjusted { get; private set; }
    public string UnrealizedGainsAcc { get; private set; }
    public string RealizedGainsAcc { get; private set; }
    public string UnrealizedLossesAcc { get; private set; }
    public string RealizedLossesAcc { get; private set; }
    public decimal InvoiceRoundingPrecision { get; private set; }
    public short InvoiceRoundingType { get; private set; }
    public decimal AmountRoundingPrecision { get; private set; }
    public decimal UnitAmountRoundingPrecision { get; private set; }
    public string Description { get; private set; }
    public string AmountDecimalPlaces { get; private set; }
    public string UnitAmountDecimalPlaces { get; private set; }
    public string RealizedGLGainsAccount { get; private set; }
    public string RealizedGLLossesAccount { get; private set; }
    public decimal ApplnRoundingPrecision { get; private set; }
    public bool EmuCurrency { get; private set; }
    public decimal CurrencyFactor { get; private set; }
    public string ResidualGainsAccount { get; private set; }
    public string ResidualLossesAccount { get; private set; }
    public string ConvLcyRndgDebitAcc { get; private set; }
    public string ConvLcyRndgCreditAcc { get; private set; }
    public decimal MaxVatDifferenceAllowed { get; private set; }
    public short VatRoundingType { get; private set; }
    public decimal PaymentTolerance { get; private set; }
    public decimal MaxPaymentToleranceAmount { get; private set; }
    public string Symbol { get; private set; }

    public static OperationResult<Currency, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Currency, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Currency()
        {
            TenantId = tenantId
        };
        return OperationResult<Currency, DomainError>.Ok(entity);
    }
}
