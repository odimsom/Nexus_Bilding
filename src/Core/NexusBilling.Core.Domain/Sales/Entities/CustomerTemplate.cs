using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class CustomerTemplate : Entity
{
    private CustomerTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string TerritoryCode { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string CustomerPostingGroup { get; private set; }
    public string CurrencyCode { get; private set; }
    public string CustomerPriceGroup { get; private set; }
    public string PaymentTermsCode { get; private set; }
    public string ShipmentMethodCode { get; private set; }
    public string InvoiceDiscCode { get; private set; }
    public string CustomerDiscGroup { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string PaymentMethodCode { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public bool AllowLineDisc { get; private set; }

    public static OperationResult<CustomerTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CustomerTemplate, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CustomerTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<CustomerTemplate, DomainError>.Ok(entity);
    }
}
