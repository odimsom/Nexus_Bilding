using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class AssemblyHeader : Entity
{
    private AssemblyHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string SearchDescription { get; private set; }
    public string Description2 { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public string ItemNo { get; private set; }
    public string VariantCode { get; private set; }
    public string InventoryPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string LocationCode { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public DateTime? EndingDate { get; private set; }
    public string BinCode { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal QuantityBase { get; private set; }
    public decimal RemainingQuantity { get; private set; }
    public decimal RemainingQuantityBase { get; private set; }
    public decimal AssembledQuantity { get; private set; }
    public decimal AssembledQuantityBase { get; private set; }
    public decimal QuantityToAssemble { get; private set; }
    public decimal QuantityToAssembleBase { get; private set; }
    public short PlanningFlexibility { get; private set; }
    public bool MpsOrder { get; private set; }
    public string PostingNo { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal CostAmount { get; private set; }
    public decimal IndirectCost { get; private set; }
    public decimal OverheadRate { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string NoSeries { get; private set; }
    public string PostingNoSeries { get; private set; }
    public short Status { get; private set; }
    public int DimensionSetId { get; private set; }
    public string AssignedUserId { get; private set; }

    public static OperationResult<AssemblyHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AssemblyHeader, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AssemblyHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<AssemblyHeader, DomainError>.Ok(entity);
    }
}
