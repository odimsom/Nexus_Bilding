using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class WarrantyLedgerEntry : Entity
{
    private WarrantyLedgerEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string DocumentNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public string CustomerNo { get; private set; }
    public string ShipToCode { get; private set; }
    public string BillToCustomerNo { get; private set; }
    public string VariantCodeServiced { get; private set; }
    public string ServiceItemNoServiced { get; private set; }
    public string ItemNoServiced { get; private set; }
    public string SerialNoServiced { get; private set; }
    public string ServiceItemGroupServiced { get; private set; }
    public string ServiceOrderNo { get; private set; }
    public string ServiceContractNo { get; private set; }
    public string FaultReasonCode { get; private set; }
    public string FaultAreaCode { get; private set; }
    public string FaultCode { get; private set; }
    public string SymptomCode { get; private set; }
    public string ResolutionCode { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public decimal Quantity { get; private set; }
    public string WorkTypeCode { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public bool Open { get; private set; }
    public string VendorNo { get; private set; }
    public string VendorItemNo { get; private set; }
    public string VariantCode { get; private set; }
    public int ServiceOrderLineNo { get; private set; }
    public int DimensionSetId { get; private set; }

    public static OperationResult<WarrantyLedgerEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WarrantyLedgerEntry, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WarrantyLedgerEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<WarrantyLedgerEntry, DomainError>.Ok(entity);
    }
}
