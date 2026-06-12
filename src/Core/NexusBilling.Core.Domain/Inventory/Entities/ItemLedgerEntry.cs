using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemLedgerEntry : Entity
{
    private ItemLedgerEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string? ItemNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short EntryType { get; private set; }
    public string SourceNo { get; private set; }
    public string DocumentNo { get; private set; }
    public string Description { get; private set; }
    public string? LocationCode { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal RemainingQuantity { get; private set; }
    public decimal InvoicedQuantity { get; private set; }
    public int AppliesToEntry { get; private set; }
    public bool Open { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public bool Positive { get; private set; }
    public short SourceType { get; private set; }
    public bool DropShipment { get; private set; }
    public string TransactionType { get; private set; }
    public string TransportMethod { get; private set; }
    public string? CountryRegionCode { get; private set; }
    public string EntryExitPoint { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string? Area { get; private set; }
    public string TransactionSpecification { get; private set; }
    public string NoSeries { get; private set; }
    public short DocumentType { get; private set; }
    public int DocumentLineNo { get; private set; }
    public short OrderType { get; private set; }
    public string OrderNo { get; private set; }
    public int OrderLineNo { get; private set; }
    public int DimensionSetId { get; private set; }
    public bool AssembleToOrder { get; private set; }
    public string? JobNo { get; private set; }
    public string JobTaskNo { get; private set; }
    public bool JobPurchase { get; private set; }
    public string VariantCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public bool DerivedFromBlanketOrder { get; private set; }
    public string CrossReferenceNo { get; private set; }
    public string? OriginallyOrderedNo { get; private set; }
    public string OriginallyOrderedVarCode { get; private set; }
    public bool OutOfStockSubstitution { get; private set; }
    public string ItemCategoryCode { get; private set; }
    public bool Nonstock { get; private set; }
    public string? PurchasingCode { get; private set; }
    public string ProductGroupCode { get; private set; }
    public bool CompletelyInvoiced { get; private set; }
    public DateTime? LastInvoiceDate { get; private set; }
    public bool AppliedEntryToAdjust { get; private set; }
    public bool Correction { get; private set; }
    public decimal ShippedQtyNotReturned { get; private set; }
    public int ProdOrderCompLineNo { get; private set; }
    public string SerialNo { get; private set; }
    public string LotNo { get; private set; }
    public DateTime? WarrantyDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public short ItemTracking { get; private set; }
    public string ReturnReasonCode { get; private set; }

    public static OperationResult<ItemLedgerEntry, DomainError> Create(
        TenantIdentifier tenantId,
        int entryNo)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemLedgerEntry, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));
        if (entryNo <= 0)
            return OperationResult<ItemLedgerEntry, DomainError>.Fail(DomainError.Validation("inventory.entry_no_invalid", "El número de asiento debe ser mayor a cero."));

        var entity = new ItemLedgerEntry
        {
            TenantId = tenantId,
            EntryNo = entryNo,
            SourceNo = string.Empty,
            DocumentNo = string.Empty,
            Description = string.Empty,
            GlobalDimension1Code = string.Empty,
            GlobalDimension2Code = string.Empty,
            TransactionType = string.Empty,
            TransportMethod = string.Empty,
            EntryExitPoint = string.Empty,
            ExternalDocumentNo = string.Empty,
            TransactionSpecification = string.Empty,
            NoSeries = string.Empty,
            OrderNo = string.Empty,
            JobTaskNo = string.Empty,
            VariantCode = string.Empty,
            UnitOfMeasureCode = string.Empty,
            CrossReferenceNo = string.Empty,
            OriginallyOrderedVarCode = string.Empty,
            ItemCategoryCode = string.Empty,
            ProductGroupCode = string.Empty,
            SerialNo = string.Empty,
            LotNo = string.Empty,
            ReturnReasonCode = string.Empty
        };

        return OperationResult<ItemLedgerEntry, DomainError>.Ok(entity);
    }

    // EntryType constants
    public const short Purchase          = 0;
    public const short Sale              = 1;
    public const short PositiveAdjustment = 2;
    public const short NegativeAdjustment = 3;
    public const short Transfer          = 4;

    public static ItemLedgerEntry CreateAdjustment(
        TenantIdentifier tenantId,
        int entryNo,
        string itemNo,
        decimal quantity,
        string documentNo,
        string description,
        string unitOfMeasureCode)
    {
        short entryType = quantity >= 0 ? PositiveAdjustment : NegativeAdjustment;
        var entry = new ItemLedgerEntry
        {
            TenantId = tenantId,
            EntryNo = entryNo,
            ItemNo = itemNo,
            PostingDate = DateTime.UtcNow,
            EntryType = entryType,
            DocumentNo = documentNo,
            Description = description,
            Quantity = quantity,
            RemainingQuantity = quantity,
            InvoicedQuantity = 0,
            Open = true,
            Positive = quantity >= 0,
            UnitOfMeasureCode = unitOfMeasureCode,
            QtyPerUnitOfMeasure = 1,
            DocumentDate = DateTime.UtcNow,
            SourceNo = string.Empty,
            GlobalDimension1Code = string.Empty,
            GlobalDimension2Code = string.Empty,
            TransactionType = string.Empty,
            TransportMethod = string.Empty,
            EntryExitPoint = string.Empty,
            ExternalDocumentNo = string.Empty,
            TransactionSpecification = string.Empty,
            NoSeries = string.Empty,
            OrderNo = string.Empty,
            JobTaskNo = string.Empty,
            VariantCode = string.Empty,
            CrossReferenceNo = string.Empty,
            OriginallyOrderedVarCode = string.Empty,
            ItemCategoryCode = string.Empty,
            ProductGroupCode = string.Empty,
            SerialNo = string.Empty,
            LotNo = string.Empty,
            ReturnReasonCode = string.Empty,
        };
        return entry;
    }
}
