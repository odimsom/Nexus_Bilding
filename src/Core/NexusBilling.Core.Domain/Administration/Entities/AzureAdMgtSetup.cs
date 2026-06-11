using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AzureAdMgtSetup : Entity
{
    private AzureAdMgtSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public int AuthFlowCodeunitId { get; private set; }
    public int AzureAdUserMgtCodeunitId { get; private set; }

    public static OperationResult<AzureAdMgtSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AzureAdMgtSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AzureAdMgtSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<AzureAdMgtSetup, DomainError>.Ok(entity);
    }
}
