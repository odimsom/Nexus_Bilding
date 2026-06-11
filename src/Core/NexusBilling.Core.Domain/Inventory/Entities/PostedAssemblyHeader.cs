using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class PostedAssemblyHeader : Entity
{
    private PostedAssemblyHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string SearchDescription { get; private set; }
    public string Description2 { get; private set; }
    public string OrderNo { get; private set; }
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
    public int ItemRcptEntryNo { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal QuantityBase { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal CostAmount { get; private set; }
    public decimal IndirectCost { get; private set; }
    public decimal OverheadRate { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public bool Reversed { get; private set; }
    public string NoSeries { get; private set; }
    public string PostingNoSeries { get; private set; }
    public string OrderNoSeries { get; private set; }
    public int DimensionSetId { get; private set; }
    public string UserId { get; private set; }
    public string SourceCode { get; private set; }

    public static OperationResult<PostedAssemblyHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PostedAssemblyHeader, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PostedAssemblyHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<PostedAssemblyHeader, DomainError>.Ok(entity);
    }
}
