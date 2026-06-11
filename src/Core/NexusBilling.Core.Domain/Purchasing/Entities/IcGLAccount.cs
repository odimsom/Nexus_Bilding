using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class IcGLAccount : Entity
{
    private IcGLAccount() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Name { get; private set; }
    public short AccountType { get; private set; }
    public short IncomeBalance { get; private set; }
    public bool Blocked { get; private set; }
    public string MapToGLAccNo { get; private set; }
    public int Indentation { get; private set; }

    public static OperationResult<IcGLAccount, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IcGLAccount, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IcGLAccount()
        {
            TenantId = tenantId
        };
        return OperationResult<IcGLAccount, DomainError>.Ok(entity);
    }
}
