using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ProductionBomHeader : Entity
{
    private ProductionBomHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public string SearchName { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public int LowLevelCode { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public short Status { get; private set; }
    public string VersionNos { get; private set; }
    public string NoSeries { get; private set; }

    public static OperationResult<ProductionBomHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ProductionBomHeader, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ProductionBomHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<ProductionBomHeader, DomainError>.Ok(entity);
    }
}
