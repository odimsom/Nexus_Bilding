using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class NavAppTenantAddIn : Entity
{
    private NavAppTenantAddIn() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid AppId { get; private set; }
    public string AddInName { get; private set; }
    public string PublicKeyToken { get; private set; }
    public string Version { get; private set; }
    public short Category { get; private set; }
    public string Description { get; private set; }
    public byte[]? Resource { get; private set; }

    public static OperationResult<NavAppTenantAddIn, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<NavAppTenantAddIn, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new NavAppTenantAddIn()
        {
            TenantId = tenantId
        };
        return OperationResult<NavAppTenantAddIn, DomainError>.Ok(entity);
    }
}
