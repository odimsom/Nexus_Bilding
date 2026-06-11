using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TransferShipmentLine : Entity
{
    private TransferShipmentLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string DocumentNo { get; private set; }
    public int LineNo { get; private set; }
    public string ItemNo { get; private set; }
    public decimal Quantity { get; private set; }
    public string UnitOfMeasure { get; private set; }
    public string Description { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string InventoryPostingGroup { get; private set; }
    public decimal QuantityBase { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal GrossWeight { get; private set; }
    public decimal NetWeight { get; private set; }
    public decimal UnitVolume { get; private set; }
    public string VariantCode { get; private set; }
    public decimal UnitsPerParcel { get; private set; }
    public string Description2 { get; private set; }
    public string TransferOrderNo { get; private set; }
    public DateTime? ShipmentDate { get; private set; }
    public string ShippingAgentCode { get; private set; }
    public string ShippingAgentServiceCode { get; private set; }
    public string InTransitCode { get; private set; }
    public string TransferFromCode { get; private set; }
    public string TransferToCode { get; private set; }
    public int ItemShptEntryNo { get; private set; }
    public string ShippingTime { get; private set; }
    public int DimensionSetId { get; private set; }
    public string ItemCategoryCode { get; private set; }
    public string ProductGroupCode { get; private set; }
    public string TransferFromBinCode { get; private set; }

    public static OperationResult<TransferShipmentLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TransferShipmentLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TransferShipmentLine()
        {
            TenantId = tenantId
        };
        return OperationResult<TransferShipmentLine, DomainError>.Ok(entity);
    }
}
