using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class PostedAssemblyLine : Entity
{
    private PostedAssemblyLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string DocumentNo { get; private set; }
    public int LineNo { get; private set; }
    public string OrderNo { get; private set; }
    public int OrderLineNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string VariantCode { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public string LeadTimeOffset { get; private set; }
    public short ResourceUsageType { get; private set; }
    public string LocationCode { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string BinCode { get; private set; }
    public string Position { get; private set; }
    public string Position2 { get; private set; }
    public string Position3 { get; private set; }
    public int ItemShptEntryNo { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal QuantityBase { get; private set; }
    public DateTime? DueDate { get; private set; }
    public decimal QuantityPer { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string InventoryPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public decimal UnitCost { get; private set; }
    public decimal CostAmount { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public int DimensionSetId { get; private set; }

    public static OperationResult<PostedAssemblyLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PostedAssemblyLine, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PostedAssemblyLine()
        {
            TenantId = tenantId
        };
        return OperationResult<PostedAssemblyLine, DomainError>.Ok(entity);
    }
}
