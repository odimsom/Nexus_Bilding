using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class MyAccount : Entity
{
    private MyAccount() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public string AccountNo { get; private set; }
    public string Name { get; private set; }

    public static OperationResult<MyAccount, DomainError> Create(
        TenantIdentifier tenantId,
        string userId,
        string accountNo,
        string name)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<MyAccount, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(userId))
            return OperationResult<MyAccount, DomainError>.Fail(DomainError.Validation("finance.user_id_required", "El campo user_id es obligatorio."));
        if (string.IsNullOrWhiteSpace(accountNo))
            return OperationResult<MyAccount, DomainError>.Fail(DomainError.Validation("finance.account_no_required", "El campo account_no es obligatorio."));
        if (string.IsNullOrWhiteSpace(name))
            return OperationResult<MyAccount, DomainError>.Fail(DomainError.Validation("finance.name_required", "El campo name es obligatorio."));

        var entity = new MyAccount()
        {
            TenantId = tenantId,
            UserId = userId.Trim(),
            AccountNo = accountNo.Trim(),
            Name = name.Trim(),
        };

        return OperationResult<MyAccount, DomainError>.Ok(entity);
    }
}
