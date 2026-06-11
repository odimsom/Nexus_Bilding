using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ReservationEntry : Entity
{
    private ReservationEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string ItemNo { get; private set; }
    public string LocationCode { get; private set; }
    public decimal QuantityBase { get; private set; }
    public short ReservationStatus { get; private set; }
    public string Description { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public int TransferredFromEntryNo { get; private set; }
    public int SourceType { get; private set; }
    public short SourceSubtype { get; private set; }
    public string SourceId { get; private set; }
    public string SourceBatchName { get; private set; }
    public int SourceProdOrderLine { get; private set; }
    public int SourceRefNo { get; private set; }
    public int ItemLedgerEntryNo { get; private set; }
    public DateTime? ExpectedReceiptDate { get; private set; }
    public DateTime? ShipmentDate { get; private set; }
    public string SerialNo { get; private set; }
    public string CreatedBy { get; private set; }
    public string ChangedBy { get; private set; }
    public bool Positive { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public decimal Quantity { get; private set; }
    public short Binding { get; private set; }
    public bool SuppressedActionMsg { get; private set; }
    public short PlanningFlexibility { get; private set; }
    public int ApplToItemEntry { get; private set; }
    public DateTime? WarrantyDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public decimal QtyToHandleBase { get; private set; }
    public decimal QtyToInvoiceBase { get; private set; }
    public decimal QuantityInvoicedBase { get; private set; }
    public string NewSerialNo { get; private set; }
    public string NewLotNo { get; private set; }
    public bool DisallowCancellation { get; private set; }
    public string LotNo { get; private set; }
    public string VariantCode { get; private set; }
    public int ApplFromItemEntry { get; private set; }
    public bool Correction { get; private set; }
    public DateTime? NewExpirationDate { get; private set; }
    public short ItemTracking { get; private set; }

    public static OperationResult<ReservationEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ReservationEntry, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ReservationEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<ReservationEntry, DomainError>.Ok(entity);
    }
}
