using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DocExchServiceSetup : Entity
{
    private DocExchServiceSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string SignUpUrl { get; private set; }
    public string ServiceUrl { get; private set; }
    public string SignInUrl { get; private set; }
    public Guid ConsumerKey { get; private set; }
    public Guid ConsumerSecret { get; private set; }
    public Guid Token { get; private set; }
    public Guid TokenSecret { get; private set; }
    public Guid DocExchTenantId { get; private set; }
    public string UserAgent { get; private set; }
    public bool Enabled { get; private set; }
    public bool LogWebRequests { get; private set; }

    public static OperationResult<DocExchServiceSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DocExchServiceSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DocExchServiceSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<DocExchServiceSetup, DomainError>.Ok(entity);
    }
}
