using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class CapacityLedgerEntry : Entity
{
    private CapacityLedgerEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string No { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short Type { get; private set; }
    public string DocumentNo { get; private set; }
    public string Description { get; private set; }
    public string OperationNo { get; private set; }
    public string WorkCenterNo { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal SetupTime { get; private set; }
    public decimal RunTime { get; private set; }
    public decimal StopTime { get; private set; }
    public decimal InvoicedQuantity { get; private set; }
    public decimal OutputQuantity { get; private set; }
    public decimal ScrapQuantity { get; private set; }
    public decimal ConcurrentCapacity { get; private set; }
    public string CapUnitOfMeasureCode { get; private set; }
    public decimal QtyPerCapUnitOfMeasure { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public bool LastOutputLine { get; private set; }
    public bool CompletelyInvoiced { get; private set; }
    public string StartingTime { get; private set; }
    public string EndingTime { get; private set; }
    public string RoutingNo { get; private set; }
    public int RoutingReferenceNo { get; private set; }
    public string ItemNo { get; private set; }
    public string VariantCode { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string StopCode { get; private set; }
    public string ScrapCode { get; private set; }
    public string WorkCenterGroupCode { get; private set; }
    public string WorkShiftCode { get; private set; }
    public bool Subcontracting { get; private set; }
    public short OrderType { get; private set; }
    public string OrderNo { get; private set; }
    public int OrderLineNo { get; private set; }
    public int DimensionSetId { get; private set; }

    public static OperationResult<CapacityLedgerEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CapacityLedgerEntry, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CapacityLedgerEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<CapacityLedgerEntry, DomainError>.Ok(entity);
    }
}
