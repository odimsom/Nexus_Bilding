using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WarehouseEntry : Entity
{
    private WarehouseEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string JournalBatchName { get; private set; }
    public int LineNo { get; private set; }
    public DateTime? RegisteringDate { get; private set; }
    public string LocationCode { get; private set; }
    public string ZoneCode { get; private set; }
    public string BinCode { get; private set; }
    public string Description { get; private set; }
    public string ItemNo { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal QtyBase { get; private set; }
    public int SourceType { get; private set; }
    public short SourceSubtype { get; private set; }
    public string SourceNo { get; private set; }
    public int SourceLineNo { get; private set; }
    public int SourceSublineNo { get; private set; }
    public short SourceDocument { get; private set; }
    public string SourceCode { get; private set; }
    public string ReasonCode { get; private set; }
    public string NoSeries { get; private set; }
    public string BinTypeCode { get; private set; }
    public decimal Cubage { get; private set; }
    public decimal Weight { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string WhseDocumentNo { get; private set; }
    public short WhseDocumentType { get; private set; }
    public int WhseDocumentLineNo { get; private set; }
    public short EntryType { get; private set; }
    public short ReferenceDocument { get; private set; }
    public string ReferenceNo { get; private set; }
    public string UserId { get; private set; }
    public string VariantCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public string SerialNo { get; private set; }
    public string LotNo { get; private set; }
    public DateTime? WarrantyDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string PhysInvtCountingPeriodCode { get; private set; }
    public short PhysInvtCountingPeriodType { get; private set; }
    public bool Dedicated { get; private set; }

    public static OperationResult<WarehouseEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WarehouseEntry, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WarehouseEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<WarehouseEntry, DomainError>.Ok(entity);
    }
}
