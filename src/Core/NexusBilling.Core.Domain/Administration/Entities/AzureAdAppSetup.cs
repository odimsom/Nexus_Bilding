using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AzureAdAppSetup : Entity
{
    private AzureAdAppSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid AppId { get; private set; }
    public byte[]? SecretKey { get; private set; }
    public int PrimaryKey { get; private set; }
    public string RedirectUrl { get; private set; }

    public static OperationResult<AzureAdAppSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AzureAdAppSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AzureAdAppSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<AzureAdAppSetup, DomainError>.Ok(entity);
    }
}
