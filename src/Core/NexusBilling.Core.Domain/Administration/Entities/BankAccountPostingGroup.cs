using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class BankAccountPostingGroup : Entity
{
    private BankAccountPostingGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string GLBankAccountNo { get; private set; }

    public static OperationResult<BankAccountPostingGroup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BankAccountPostingGroup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BankAccountPostingGroup()
        {
            TenantId = tenantId
        };
        return OperationResult<BankAccountPostingGroup, DomainError>.Ok(entity);
    }
}
