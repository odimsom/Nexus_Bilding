using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceShipmentItemLine : Entity
{
    private ServiceShipmentItemLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public int LineNo { get; private set; }
    public string ServiceItemNo { get; private set; }
    public string ServiceItemGroupCode { get; private set; }
    public string ItemNo { get; private set; }
    public string SerialNo { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public short Priority { get; private set; }
    public decimal ResponseTimeHours { get; private set; }
    public DateTime? ResponseDate { get; private set; }
    public string ResponseTime { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public string StartingTime { get; private set; }
    public DateTime? FinishingDate { get; private set; }
    public string FinishingTime { get; private set; }
    public string ServiceShelfNo { get; private set; }
    public DateTime? WarrantyStartingDateParts { get; private set; }
    public DateTime? WarrantyEndingDateParts { get; private set; }
    public bool Warranty { get; private set; }
    public decimal WarrantyParts { get; private set; }
    public decimal WarrantyLabor { get; private set; }
    public DateTime? WarrantyStartingDateLabor { get; private set; }
    public DateTime? WarrantyEndingDateLabor { get; private set; }
    public string ContractNo { get; private set; }
    public string LoanerNo { get; private set; }
    public string VendorNo { get; private set; }
    public string VendorItemNo { get; private set; }
    public string FaultReasonCode { get; private set; }
    public string ServicePriceGroupCode { get; private set; }
    public string FaultAreaCode { get; private set; }
    public string SymptomCode { get; private set; }
    public string FaultCode { get; private set; }
    public string ResolutionCode { get; private set; }
    public string VariantCode { get; private set; }
    public decimal ActualResponseTimeHours { get; private set; }
    public string ServicePriceAdjmtGrCode { get; private set; }
    public short AdjustmentType { get; private set; }
    public decimal BaseAmountToAdjust { get; private set; }
    public string ShipToCode { get; private set; }
    public string CustomerNo { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public int DimensionSetId { get; private set; }

    public static OperationResult<ServiceShipmentItemLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceShipmentItemLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceShipmentItemLine()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceShipmentItemLine, DomainError>.Ok(entity);
    }
}
