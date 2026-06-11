using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class JobPlanningLine : Entity
{
    private JobPlanningLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public int LineNo { get; private set; }
    public string JobNo { get; private set; }
    public DateTime? PlanningDate { get; private set; }
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
    public DateTime? LastDateModified { get; private set; }
    public string UserId { get; private set; }
    public string WorkTypeCode { get; private set; }
    public string CustomerPriceGroup { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string JobTaskNo { get; private set; }
    public decimal LineAmountLcy { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal TotalCost { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice { get; private set; }
    public decimal LineAmount { get; private set; }
    public decimal LineDiscountAmount { get; private set; }
    public decimal LineDiscountAmountLcy { get; private set; }
    public decimal CostFactor { get; private set; }
    public string SerialNo { get; private set; }
    public string LotNo { get; private set; }
    public decimal LineDiscount { get; private set; }
    public short LineType { get; private set; }
    public string CurrencyCode { get; private set; }
    public DateTime? CurrencyDate { get; private set; }
    public decimal CurrencyFactor { get; private set; }
    public bool ScheduleLine { get; private set; }
    public bool ContractLine { get; private set; }
    public int JobContractEntryNo { get; private set; }
    public decimal VatUnitPrice { get; private set; }
    public decimal VatLineDiscountAmount { get; private set; }
    public decimal VatLineAmount { get; private set; }
    public decimal Vat { get; private set; }
    public string Description2 { get; private set; }
    public int JobLedgerEntryNo { get; private set; }
    public short Status { get; private set; }
    public short LedgerEntryType { get; private set; }
    public int LedgerEntryNo { get; private set; }
    public bool SystemCreatedEntry { get; private set; }
    public bool UsageLink { get; private set; }
    public decimal RemainingQty { get; private set; }
    public decimal RemainingQtyBase { get; private set; }
    public decimal RemainingTotalCost { get; private set; }
    public decimal RemainingTotalCostLcy { get; private set; }
    public decimal RemainingLineAmount { get; private set; }
    public decimal RemainingLineAmountLcy { get; private set; }
    public decimal QtyPosted { get; private set; }
    public decimal QtyToTransferToJournal { get; private set; }
    public decimal PostedTotalCost { get; private set; }
    public decimal PostedTotalCostLcy { get; private set; }
    public decimal PostedLineAmount { get; private set; }
    public decimal PostedLineAmountLcy { get; private set; }
    public decimal QtyToTransferToInvoice { get; private set; }
    public decimal QtyToInvoice { get; private set; }
    public short Reserve { get; private set; }
    public bool Planned { get; private set; }
    public string VariantCode { get; private set; }
    public string BinCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public decimal QuantityBase { get; private set; }
    public DateTime? RequestedDeliveryDate { get; private set; }
    public DateTime? PromisedDeliveryDate { get; private set; }
    public DateTime? PlannedDeliveryDate { get; private set; }
    public string ServiceOrderNo { get; private set; }

    public static OperationResult<JobPlanningLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<JobPlanningLine, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new JobPlanningLine()
        {
            TenantId = tenantId
        };
        return OperationResult<JobPlanningLine, DomainError>.Ok(entity);
    }
}
