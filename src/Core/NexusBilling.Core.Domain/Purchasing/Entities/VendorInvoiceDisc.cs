using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class VendorInvoiceDisc : Entity
{
    private VendorInvoiceDisc() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public decimal MinimumAmount { get; private set; }
    public decimal Discount { get; private set; }
    public decimal ServiceCharge { get; private set; }
    public string CurrencyCode { get; private set; }

    public static OperationResult<VendorInvoiceDisc, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<VendorInvoiceDisc, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new VendorInvoiceDisc()
        {
            TenantId = tenantId
        };
        return OperationResult<VendorInvoiceDisc, DomainError>.Ok(entity);
    }
}
