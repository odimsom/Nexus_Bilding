using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class RoutingHeader : Entity
{
    private RoutingHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public string SearchDescription { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public short Status { get; private set; }
    public short Type { get; private set; }
    public string VersionNos { get; private set; }
    public string NoSeries { get; private set; }

    public static OperationResult<RoutingHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<RoutingHeader, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new RoutingHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<RoutingHeader, DomainError>.Ok(entity);
    }
}
