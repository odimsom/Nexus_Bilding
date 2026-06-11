using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class ResJournalLine : Entity
{
    private ResJournalLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public int LineNo { get; private set; }
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
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string SourceCode { get; private set; }
    public string JournalBatchName { get; private set; }
    public string ReasonCode { get; private set; }
    public short RecurringMethod { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string RecurringFrequency { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string PostingNoSeries { get; private set; }
    public short SourceType { get; private set; }
    public string SourceNo { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public short OrderType { get; private set; }
    public string OrderNo { get; private set; }
    public int OrderLineNo { get; private set; }
    public int DimensionSetId { get; private set; }
    public string TimeSheetNo { get; private set; }
    public int TimeSheetLineNo { get; private set; }
    public DateTime? TimeSheetDate { get; private set; }
    public bool SystemCreatedEntry { get; private set; }

    public static OperationResult<ResJournalLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ResJournalLine, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ResJournalLine()
        {
            TenantId = tenantId
        };
        return OperationResult<ResJournalLine, DomainError>.Ok(entity);
    }
}
