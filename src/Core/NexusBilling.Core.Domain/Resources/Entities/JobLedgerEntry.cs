using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobLedgerEntry : Entity
{
    private JobLedgerEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
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
    public string JobPostingGroup { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string WorkTypeCode { get; private set; }
    public string CustomerPriceGroup { get; private set; }
    public string UserId { get; private set; }
    public string SourceCode { get; private set; }
    public decimal AmtToPostToGL { get; private set; }
    public decimal AmtPostedToGL { get; private set; }
    public short EntryType { get; private set; }
    public string JournalBatchName { get; private set; }
    public string ReasonCode { get; private set; }
    public string TransactionType { get; private set; }
    public string TransportMethod { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string EntryExitPoint { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public string Area { get; private set; }
    public string TransactionSpecification { get; private set; }
    public string NoSeries { get; private set; }
    public decimal AdditionalCurrencyTotalCost { get; private set; }
    public decimal AddCurrencyTotalPrice { get; private set; }
    public decimal AddCurrencyLineAmount { get; private set; }
    public int DimensionSetId { get; private set; }
    public string JobTaskNo { get; private set; }
    public decimal LineAmountLcy { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal TotalCost { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice { get; private set; }
    public decimal LineAmount { get; private set; }
    public decimal LineDiscountAmount { get; private set; }
    public decimal LineDiscountAmountLcy { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal CurrencyFactor { get; private set; }
    public string Description2 { get; private set; }
    public short LedgerEntryType { get; private set; }
    public int LedgerEntryNo { get; private set; }
    public string SerialNo { get; private set; }
    public string LotNo { get; private set; }
    public decimal LineDiscount { get; private set; }
    public short LineType { get; private set; }
    public decimal OriginalUnitCostLcy { get; private set; }
    public decimal OriginalTotalCostLcy { get; private set; }
    public decimal OriginalUnitCost { get; private set; }
    public decimal OriginalTotalCost { get; private set; }
    public decimal OriginalTotalCostAcy { get; private set; }
    public bool Adjusted { get; private set; }
    public DateTime? DatetimeAdjusted { get; private set; }
    public string VariantCode { get; private set; }
    public string BinCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public decimal QuantityBase { get; private set; }
    public string ServiceOrderNo { get; private set; }
    public string PostedServiceShipmentNo { get; private set; }

    public static OperationResult<JobLedgerEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobLedgerEntry, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobLedgerEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<JobLedgerEntry, DomainError>.Ok(entity);
    }
}
