using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class CustInvoiceDisc : Entity
{
    private CustInvoiceDisc() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public decimal MinimumAmount { get; private set; }
    public decimal Discount { get; private set; }
    public decimal ServiceCharge { get; private set; }
    public string CurrencyCode { get; private set; }

    public static OperationResult<CustInvoiceDisc, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CustInvoiceDisc, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CustInvoiceDisc()
        {
            TenantId = tenantId
        };
        return OperationResult<CustInvoiceDisc, DomainError>.Ok(entity);
    }
}
