using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class TaxAreaLine : Entity
{
    private TaxAreaLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string TaxArea { get; private set; }
    public string TaxJurisdictionCode { get; private set; }
    public int CalculationOrder { get; private set; }

    public static OperationResult<TaxAreaLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TaxAreaLine, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TaxAreaLine()
        {
            TenantId = tenantId
        };
        return OperationResult<TaxAreaLine, DomainError>.Ok(entity);
    }
}
