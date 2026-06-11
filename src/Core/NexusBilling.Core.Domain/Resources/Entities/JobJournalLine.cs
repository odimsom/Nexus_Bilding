using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobJournalLine : Entity
{
    private JobJournalLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public int LineNo { get; private set; }
    public string JobNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public string DocumentNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal DirectUnitCostLcy { get; private set; }
    public decimal UnitCostLcy { get; private set; }
    public decimal TotalCostLcy { get; private set; }
    public decimal UnitPriceLcy { get; private set; }
    public decimal TotalPriceLcy { get; private set; }
    public string ResourceGroupNo { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public string LocationCode { get; private set; }
    public bool Chargeable { get; private set; }
    public string PostingGroup { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string WorkTypeCode { get; private set; }
    public string CustomerPriceGroup { get; private set; }
    public int AppliesToEntry { get; private set; }
    public short EntryType { get; private set; }
    public string SourceCode { get; private set; }
    public string JournalBatchName { get; private set; }
    public string ReasonCode { get; private set; }
    public short RecurringMethod { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string RecurringFrequency { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string TransactionType { get; private set; }
    public string TransportMethod { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string EntryExitPoint { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string Area { get; private set; }
    public string TransactionSpecification { get; private set; }
    public string SerialNo { get; private set; }
    public string PostingNoSeries { get; private set; }
    public string SourceCurrencyCode { get; private set; }
    public decimal SourceCurrencyTotalCost { get; private set; }
    public decimal SourceCurrencyTotalPrice { get; private set; }
    public decimal SourceCurrencyLineAmount { get; private set; }
    public int DimensionSetId { get; private set; }
    public string TimeSheetNo { get; private set; }
    public int TimeSheetLineNo { get; private set; }
    public DateTime? TimeSheetDate { get; private set; }
    public string JobTaskNo { get; private set; }
    public decimal TotalCost { get; private set; }
    public decimal UnitPrice { get; private set; }
    public short LineType { get; private set; }
    public int AppliesFromEntry { get; private set; }
    public bool JobPostingOnly { get; private set; }
    public decimal LineDiscount { get; private set; }
    public decimal LineDiscountAmount { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal LineAmount { get; private set; }
    public decimal CurrencyFactor { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal LineAmountLcy { get; private set; }
    public decimal LineDiscountAmountLcy { get; private set; }
    public decimal TotalPrice { get; private set; }
    public decimal CostFactor { get; private set; }
    public string Description2 { get; private set; }
    public short LedgerEntryType { get; private set; }
    public int LedgerEntryNo { get; private set; }
    public int JobPlanningLineNo { get; private set; }
    public decimal RemainingQty { get; private set; }
    public decimal RemainingQtyBase { get; private set; }
    public string VariantCode { get; private set; }
    public string BinCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public decimal QuantityBase { get; private set; }
    public string ServiceOrderNo { get; private set; }
    public string PostedServiceShipmentNo { get; private set; }
    public string LotNo { get; private set; }

    public static OperationResult<JobJournalLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobJournalLine, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobJournalLine()
        {
            TenantId = tenantId
        };
        return OperationResult<JobJournalLine, DomainError>.Ok(entity);
    }
}
