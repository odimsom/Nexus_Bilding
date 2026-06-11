using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class CurrencyForFinChargeTerms : Entity
{
    private CurrencyForFinChargeTerms() { }

    public TenantIdentifier TenantId { get; private set; }
    public string FinChargeTermsCode { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal AdditionalFee { get; private set; }

    public static OperationResult<CurrencyForFinChargeTerms, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CurrencyForFinChargeTerms, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CurrencyForFinChargeTerms()
        {
            TenantId = tenantId
        };
        return OperationResult<CurrencyForFinChargeTerms, DomainError>.Ok(entity);
    }
}
