using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceLedgerEntry : Entity
{
    private ServiceLedgerEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string ServiceContractNo { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public string ServContractAccGrCode { get; private set; }
    public int DocumentLineNo { get; private set; }
    public bool MovedFromPrepaidAcc { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public decimal AmountLcy { get; private set; }
    public string CustomerNo { get; private set; }
    public string ShipToCode { get; private set; }
    public string ItemNoServiced { get; private set; }
    public string SerialNoServiced { get; private set; }
    public string UserId { get; private set; }
    public string ContractInvoicePeriod { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string ServiceItemNoServiced { get; private set; }
    public string VariantCodeServiced { get; private set; }
    public string ContractGroupCode { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public decimal CostAmount { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal ChargedQty { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal ContractDiscAmount { get; private set; }
    public string BillToCustomerNo { get; private set; }
    public string FaultReasonCode { get; private set; }
    public string Description { get; private set; }
    public string ServiceOrderType { get; private set; }
    public string ServiceOrderNo { get; private set; }
    public string JobNo { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string LocationCode { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public string WorkTypeCode { get; private set; }
    public string BinCode { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public string VariantCode { get; private set; }
    public short EntryType { get; private set; }
    public bool Open { get; private set; }
    public string ServPriceAdjmtGrCode { get; private set; }
    public string ServicePriceGroupCode { get; private set; }
    public bool Prepaid { get; private set; }
    public int ApplyUntilEntryNo { get; private set; }
    public int AppliesToEntryNo { get; private set; }
    public decimal Amount { get; private set; }
    public string JobTaskNo { get; private set; }
    public short JobLineType { get; private set; }
    public bool JobPosted { get; private set; }
    public int DimensionSetId { get; private set; }

    public static OperationResult<ServiceLedgerEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceLedgerEntry, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceLedgerEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceLedgerEntry, DomainError>.Ok(entity);
    }
}
