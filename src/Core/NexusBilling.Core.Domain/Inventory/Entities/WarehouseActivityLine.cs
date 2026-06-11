using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WarehouseActivityLine : Entity
{
    private WarehouseActivityLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short ActivityType { get; private set; }
    public string No { get; private set; }
    public int LineNo { get; private set; }
    public int SourceType { get; private set; }
    public short SourceSubtype { get; private set; }
    public string SourceNo { get; private set; }
    public int SourceLineNo { get; private set; }
    public int SourceSublineNo { get; private set; }
    public short SourceDocument { get; private set; }
    public string LocationCode { get; private set; }
    public string ShelfNo { get; private set; }
    public int SortingSequenceNo { get; private set; }
    public string ItemNo { get; private set; }
    public string VariantCode { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal QtyBase { get; private set; }
    public decimal QtyOutstanding { get; private set; }
    public decimal QtyOutstandingBase { get; private set; }
    public decimal QtyToHandle { get; private set; }
    public decimal QtyToHandleBase { get; private set; }
    public decimal QtyHandled { get; private set; }
    public decimal QtyHandledBase { get; private set; }
    public short ShippingAdvice { get; private set; }
    public DateTime? DueDate { get; private set; }
    public short DestinationType { get; private set; }
    public string DestinationNo { get; private set; }
    public string ShippingAgentCode { get; private set; }
    public string ShippingAgentServiceCode { get; private set; }
    public string ShipmentMethodCode { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public bool AssembleToOrder { get; private set; }
    public bool AtoComponent { get; private set; }
    public string SerialNo { get; private set; }
    public string LotNo { get; private set; }
    public DateTime? WarrantyDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string BinCode { get; private set; }
    public string ZoneCode { get; private set; }
    public short ActionType { get; private set; }
    public short WhseDocumentType { get; private set; }
    public string WhseDocumentNo { get; private set; }
    public int WhseDocumentLineNo { get; private set; }
    public int BinRanking { get; private set; }
    public decimal Cubage { get; private set; }
    public decimal Weight { get; private set; }
    public string SpecialEquipmentCode { get; private set; }
    public string BinTypeCode { get; private set; }
    public int BreakbulkNo { get; private set; }
    public bool OriginalBreakbulk { get; private set; }
    public bool Breakbulk { get; private set; }
    public short CrossDockInformation { get; private set; }
    public bool Dedicated { get; private set; }

    public static OperationResult<WarehouseActivityLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WarehouseActivityLine, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WarehouseActivityLine()
        {
            TenantId = tenantId
        };
        return OperationResult<WarehouseActivityLine, DomainError>.Ok(entity);
    }
}
