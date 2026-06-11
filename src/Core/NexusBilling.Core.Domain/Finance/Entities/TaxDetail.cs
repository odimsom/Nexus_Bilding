using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class TaxDetail : Entity
{
    private TaxDetail() { }

    public TenantIdentifier TenantId { get; private set; }
    public string TaxJurisdictionCode { get; private set; }
    public string TaxGroupCode { get; private set; }
    public short TaxType { get; private set; }
    public decimal MaximumAmountQty { get; private set; }
    public decimal TaxBelowMaximum { get; private set; }
    public decimal TaxAboveMaximum { get; private set; }
    public DateTime? EffectiveDate { get; private set; }
    public bool CalculateTaxOnTax { get; private set; }

    public static OperationResult<TaxDetail, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TaxDetail, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TaxDetail()
        {
            TenantId = tenantId
        };
        return OperationResult<TaxDetail, DomainError>.Ok(entity);
    }
}
