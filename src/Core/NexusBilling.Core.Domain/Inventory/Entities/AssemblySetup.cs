using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class AssemblySetup : Entity
{
    private AssemblySetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public bool StockoutWarning { get; private set; }
    public string AssemblyOrderNos { get; private set; }
    public string AssemblyQuoteNos { get; private set; }
    public string BlanketAssemblyOrderNos { get; private set; }
    public string PostedAssemblyOrderNos { get; private set; }
    public short CopyComponentDimensionsFrom { get; private set; }
    public string DefaultLocationForOrders { get; private set; }
    public bool CopyCommentsWhenPosting { get; private set; }
    public bool CreateMovementsAutomatically { get; private set; }

    public static OperationResult<AssemblySetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AssemblySetup, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AssemblySetup()
        {
            TenantId = tenantId
        };
        return OperationResult<AssemblySetup, DomainError>.Ok(entity);
    }
}
