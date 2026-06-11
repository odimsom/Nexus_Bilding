using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class TaxSetup : Entity
{
    private TaxSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public bool AutoCreateTaxDetails { get; private set; }
    public string NonTaxableTaxGroupCode { get; private set; }
    public string TaxAccountSales { get; private set; }
    public string TaxAccountPurchases { get; private set; }
    public string UnrealTaxAccSales { get; private set; }
    public string UnrealTaxAccPurchases { get; private set; }
    public string ReverseChargePurchases { get; private set; }
    public string UnrealRevChargePurch { get; private set; }

    public static OperationResult<TaxSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TaxSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TaxSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<TaxSetup, DomainError>.Ok(entity);
    }
}
