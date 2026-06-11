using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceItemComponent : Entity
{
    private ServiceItemComponent() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ParentServiceItemNo { get; private set; }
    public int LineNo { get; private set; }
    public bool Active { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public DateTime? DateInstalled { get; private set; }
    public string VariantCode { get; private set; }
    public string SerialNo { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public string ServiceOrderNo { get; private set; }
    public int FromLineNo { get; private set; }
    public DateTime? LastDateModified { get; private set; }

    public static OperationResult<ServiceItemComponent, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceItemComponent, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceItemComponent()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceItemComponent, DomainError>.Ok(entity);
    }
}
