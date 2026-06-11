using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AddIn : Entity
{
    private AddIn() { }

    public TenantIdentifier TenantId { get; private set; }
    public string AddInName { get; private set; }
    public string PublicKeyToken { get; private set; }
    public string Version { get; private set; }
    public short Category { get; private set; }
    public string Description { get; private set; }
    public byte[]? Resource { get; private set; }

    public static OperationResult<AddIn, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AddIn, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AddIn()
        {
            TenantId = tenantId
        };
        return OperationResult<AddIn, DomainError>.Ok(entity);
    }
}
