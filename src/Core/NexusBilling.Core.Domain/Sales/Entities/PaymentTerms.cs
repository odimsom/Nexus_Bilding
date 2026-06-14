using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class PaymentTerms : Entity
{
    private PaymentTerms() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; set; }
    public string DueDateCalculation { get; private set; } = string.Empty;
    public string DiscountDateCalculation { get; private set; } = string.Empty;
    public decimal Discount { get; private set; }
    public string Description { get; set; }
    public bool CalcPmtDiscOnCrMemos { get; private set; }

    public static OperationResult<PaymentTerms, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PaymentTerms, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PaymentTerms()
        {
            TenantId = tenantId
        };
        return OperationResult<PaymentTerms, DomainError>.Ok(entity);
    }
}
