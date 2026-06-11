using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaMatrixPostingType : Entity
{
    private FaMatrixPostingType() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string FaPostingTypeName { get; private set; }

    public static OperationResult<FaMatrixPostingType, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaMatrixPostingType, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaMatrixPostingType()
        {
            TenantId = tenantId
        };
        return OperationResult<FaMatrixPostingType, DomainError>.Ok(entity);
    }
}
