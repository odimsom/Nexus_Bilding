using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceItem : Entity
{
    private ServiceItem() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string SerialNo { get; private set; }
    public string ServiceItemGroupCode { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public short Status { get; private set; }
    public short Priority { get; private set; }
    public string CustomerNo { get; private set; }
    public string ShipToCode { get; private set; }
    public string ItemNo { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public string LocationOfServiceItem { get; private set; }
    public decimal SalesUnitPrice { get; private set; }
    public decimal SalesUnitCost { get; private set; }
    public DateTime? WarrantyStartingDateLabor { get; private set; }
    public DateTime? WarrantyEndingDateLabor { get; private set; }
    public DateTime? WarrantyStartingDateParts { get; private set; }
    public DateTime? WarrantyEndingDateParts { get; private set; }
    public decimal WarrantyParts { get; private set; }
    public decimal WarrantyLabor { get; private set; }
    public decimal ResponseTimeHours { get; private set; }
    public DateTime? InstallationDate { get; private set; }
    public DateTime? SalesDate { get; private set; }
    public DateTime? LastServiceDate { get; private set; }
    public decimal DefaultContractValue { get; private set; }
    public decimal DefaultContractDiscount { get; private set; }
    public string VendorNo { get; private set; }
    public string VendorItemNo { get; private set; }
    public string NoSeries { get; private set; }
    public string VendorItemName { get; private set; }
    public string PreferredResource { get; private set; }
    public string VariantCode { get; private set; }
    public string ServicePriceGroupCode { get; private set; }
    public decimal DefaultContractCost { get; private set; }
    public string SearchDescription { get; private set; }
    public string SalesServShptDocumentNo { get; private set; }
    public int SalesServShptLineNo { get; private set; }
    public short ShipmentType { get; private set; }

    public static OperationResult<ServiceItem, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceItem, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceItem()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceItem, DomainError>.Ok(entity);
    }
}
