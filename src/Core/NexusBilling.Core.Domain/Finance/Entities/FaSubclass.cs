using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaSubclass : Entity
{
    private FaSubclass() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string FaClassCode { get; private set; }
    public string DefaultFaPostingGroup { get; private set; }

    public static OperationResult<FaSubclass, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaSubclass, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaSubclass()
        {
            TenantId = tenantId
        };
        return OperationResult<FaSubclass, DomainError>.Ok(entity);
    }
}
