using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class ResLedgerEntry : Entity
{
    private ResLedgerEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public short EntryType { get; private set; }
    public string DocumentNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public string ResourceNo { get; private set; }
    public string ResourceGroupNo { get; private set; }
    public string Description { get; private set; }
    public string WorkTypeCode { get; private set; }
    public string JobNo { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal DirectUnitCost { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal TotalCost { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string UserId { get; private set; }
    public string SourceCode { get; private set; }
    public bool Chargeable { get; private set; }
    public string JournalBatchName { get; private set; }
    public string ReasonCode { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string NoSeries { get; private set; }
    public short SourceType { get; private set; }
    public string SourceNo { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public decimal QuantityBase { get; private set; }
    public short OrderType { get; private set; }
    public string OrderNo { get; private set; }
    public int OrderLineNo { get; private set; }
    public int DimensionSetId { get; private set; }

    public static OperationResult<ResLedgerEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ResLedgerEntry, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ResLedgerEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<ResLedgerEntry, DomainError>.Ok(entity);
    }
}
