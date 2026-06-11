using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class CertificateOfSupply : Entity
{
    private CertificateOfSupply() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public short Status { get; private set; }
    public string No { get; private set; }
    public DateTime? ReceiptDate { get; private set; }
    public bool Printed { get; private set; }
    public string CustomerVendorName { get; private set; }
    public string ShipmentMethodCode { get; private set; }
    public DateTime? ShipmentPostingDate { get; private set; }
    public string ShipToCountryRegionCode { get; private set; }
    public string CustomerVendorNo { get; private set; }
    public string VehicleRegistrationNo { get; private set; }

    public static OperationResult<CertificateOfSupply, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CertificateOfSupply, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CertificateOfSupply()
        {
            TenantId = tenantId
        };
        return OperationResult<CertificateOfSupply, DomainError>.Ok(entity);
    }
}
