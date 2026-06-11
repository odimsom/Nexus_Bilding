using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class TrackingSpecification : Entity
{
    private TrackingSpecification() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string ItemNo { get; private set; }
    public string LocationCode { get; private set; }
    public decimal QuantityBase { get; private set; }
    public string Description { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public int SourceType { get; private set; }
    public short SourceSubtype { get; private set; }
    public string SourceId { get; private set; }
    public string SourceBatchName { get; private set; }
    public int SourceProdOrderLine { get; private set; }
    public int SourceRefNo { get; private set; }
    public int ItemLedgerEntryNo { get; private set; }
    public int TransferItemEntryNo { get; private set; }
    public string SerialNo { get; private set; }
    public bool Positive { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public int ApplToItemEntry { get; private set; }
    public DateTime? WarrantyDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public decimal QtyToHandleBase { get; private set; }
    public decimal QtyToInvoiceBase { get; private set; }
    public decimal QuantityHandledBase { get; private set; }
    public decimal QuantityInvoicedBase { get; private set; }
    public decimal QtyToHandle { get; private set; }
    public decimal QtyToInvoice { get; private set; }
    public short BufferStatus { get; private set; }
    public short BufferStatus2 { get; private set; }
    public decimal BufferValue1 { get; private set; }
    public decimal BufferValue2 { get; private set; }
    public decimal BufferValue3 { get; private set; }
    public decimal BufferValue4 { get; private set; }
    public decimal BufferValue5 { get; private set; }
    public string NewSerialNo { get; private set; }
    public string NewLotNo { get; private set; }
    public bool ProhibitCancellation { get; private set; }
    public string LotNo { get; private set; }
    public string VariantCode { get; private set; }
    public string BinCode { get; private set; }
    public int ApplFromItemEntry { get; private set; }
    public bool Correction { get; private set; }
    public DateTime? NewExpirationDate { get; private set; }
    public decimal QuantityActualHandledBase { get; private set; }

    public static OperationResult<TrackingSpecification, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TrackingSpecification, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TrackingSpecification()
        {
            TenantId = tenantId
        };
        return OperationResult<TrackingSpecification, DomainError>.Ok(entity);
    }
}
