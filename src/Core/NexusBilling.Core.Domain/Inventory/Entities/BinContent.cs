using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class BinContent : Entity
{
    private BinContent() { }

    public TenantIdentifier TenantId { get; private set; }
    public string LocationCode { get; private set; }
    public string ZoneCode { get; private set; }
    public string BinCode { get; private set; }
    public string ItemNo { get; private set; }
    public string BinTypeCode { get; private set; }
    public string WarehouseClassCode { get; private set; }
    public short BlockMovement { get; private set; }
    public decimal MinQty { get; private set; }
    public decimal MaxQty { get; private set; }
    public int BinRanking { get; private set; }
    public bool Fixed { get; private set; }
    public bool CrossDockBin { get; private set; }
    public bool Default { get; private set; }
    public string VariantCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public bool Dedicated { get; private set; }

    public static OperationResult<BinContent, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BinContent, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BinContent()
        {
            TenantId = tenantId
        };
        return OperationResult<BinContent, DomainError>.Ok(entity);
    }
}
