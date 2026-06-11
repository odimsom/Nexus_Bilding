using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ExchangeServiceSetup : Entity
{
    private ExchangeServiceSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public Guid AzureAdAppId { get; private set; }
    public string AzureAdAppCertThumbprint { get; private set; }
    public string AzureAdAuthEndpoint { get; private set; }
    public string ExchangeServiceEndpoint { get; private set; }
    public string ExchangeResourceUri { get; private set; }

    public static OperationResult<ExchangeServiceSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ExchangeServiceSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ExchangeServiceSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<ExchangeServiceSetup, DomainError>.Ok(entity);
    }
}
