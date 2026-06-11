using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class BankDataConvServiceSetup : Entity
{
    private BankDataConvServiceSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string UserName { get; private set; }
    public Guid PasswordKey { get; private set; }
    public string SignUpUrl { get; private set; }
    public string ServiceUrl { get; private set; }
    public string SupportUrl { get; private set; }

    public static OperationResult<BankDataConvServiceSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BankDataConvServiceSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BankDataConvServiceSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<BankDataConvServiceSetup, DomainError>.Ok(entity);
    }
}
