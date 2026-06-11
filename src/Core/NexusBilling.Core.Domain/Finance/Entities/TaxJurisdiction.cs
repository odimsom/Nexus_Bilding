using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class TaxJurisdiction : Entity
{
    private TaxJurisdiction() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string TaxAccountSales { get; private set; }
    public string TaxAccountPurchases { get; private set; }
    public string ReportToJurisdiction { get; private set; }
    public string UnrealTaxAccSales { get; private set; }
    public string UnrealTaxAccPurchases { get; private set; }
    public string ReverseChargePurchases { get; private set; }
    public string UnrealRevChargePurch { get; private set; }
    public short UnrealizedVatType { get; private set; }
    public bool CalculateTaxOnTax { get; private set; }
    public bool AdjustForPaymentDiscount { get; private set; }

    public static OperationResult<TaxJurisdiction, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TaxJurisdiction, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TaxJurisdiction()
        {
            TenantId = tenantId
        };
        return OperationResult<TaxJurisdiction, DomainError>.Ok(entity);
    }
}
