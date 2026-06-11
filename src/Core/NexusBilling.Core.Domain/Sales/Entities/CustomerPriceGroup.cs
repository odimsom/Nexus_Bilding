using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class CustomerPriceGroup : Entity
{
    private CustomerPriceGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public bool PriceIncludesVat { get; private set; }
    public bool AllowInvoiceDisc { get; private set; }
    public string VatBusPostingGrPrice { get; private set; }
    public string Description { get; private set; }
    public bool AllowLineDisc { get; private set; }

    public static OperationResult<CustomerPriceGroup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CustomerPriceGroup, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CustomerPriceGroup()
        {
            TenantId = tenantId
        };
        return OperationResult<CustomerPriceGroup, DomainError>.Ok(entity);
    }
}
