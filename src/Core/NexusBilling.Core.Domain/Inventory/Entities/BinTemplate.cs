using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class BinTemplate : Entity
{
    private BinTemplate() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string LocationCode { get; private set; }
    public string BinDescription { get; private set; }
    public string ZoneCode { get; private set; }
    public string BinTypeCode { get; private set; }
    public string WarehouseClassCode { get; private set; }
    public short BlockMovement { get; private set; }
    public string SpecialEquipmentCode { get; private set; }
    public int BinRanking { get; private set; }
    public decimal MaximumCubage { get; private set; }
    public decimal MaximumWeight { get; private set; }
    public bool Dedicated { get; private set; }

    public static OperationResult<BinTemplate, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BinTemplate, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BinTemplate()
        {
            TenantId = tenantId
        };
        return OperationResult<BinTemplate, DomainError>.Ok(entity);
    }
}
