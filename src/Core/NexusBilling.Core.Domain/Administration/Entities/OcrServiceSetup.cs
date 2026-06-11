using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class OcrServiceSetup : Entity
{
    private OcrServiceSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string UserName { get; private set; }
    public Guid PasswordKey { get; private set; }
    public string SignUpUrl { get; private set; }
    public string ServiceUrl { get; private set; }
    public string SignInUrl { get; private set; }
    public Guid AuthorizationKey { get; private set; }
    public string CustomerName { get; private set; }
    public string CustomerId { get; private set; }
    public string CustomerStatus { get; private set; }
    public string OrganizationId { get; private set; }
    public string DefaultOcrDocTemplate { get; private set; }
    public bool Enabled { get; private set; }
    public bool MasterDataSyncEnabled { get; private set; }
    public DateTime? MasterDataLastSync { get; private set; }

    public static OperationResult<OcrServiceSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<OcrServiceSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new OcrServiceSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<OcrServiceSetup, DomainError>.Ok(entity);
    }
}
