using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class ShipToAddress : Entity
{
    private ShipToAddress() { }

    public TenantIdentifier TenantId { get; private set; }
    public string CustomerNo { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Name2 { get; private set; }
    public string Address { get; private set; }
    public string Address2 { get; private set; }
    public string City { get; private set; }
    public string Contact { get; private set; }
    public string PhoneNo { get; private set; }
    public string TelexNo { get; private set; }
    public string ShipmentMethodCode { get; private set; }
    public string ShippingAgentCode { get; private set; }
    public string PlaceOfExport { get; private set; }
    public string CountryRegionCode { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public string LocationCode { get; private set; }
    public string FaxNo { get; private set; }
    public string TelexAnswerBack { get; private set; }
    public string PostCode { get; private set; }
    public string County { get; private set; }
    public string EMail { get; private set; }
    public string HomePage { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string ShippingAgentServiceCode { get; private set; }
    public string ServiceZoneCode { get; private set; }

    public static OperationResult<ShipToAddress, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ShipToAddress, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ShipToAddress()
        {
            TenantId = tenantId
        };
        return OperationResult<ShipToAddress, DomainError>.Ok(entity);
    }
}
