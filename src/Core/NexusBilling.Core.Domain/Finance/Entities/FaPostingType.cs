using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaPostingType : Entity
{
    private FaPostingType() { }

    public TenantIdentifier TenantId { get; private set; }
    public int FaPostingTypeNo { get; private set; }
    public string FaPostingTypeName { get; private set; }
    public bool FaEntry { get; private set; }
    public bool GLEntry { get; private set; }
    public int EntryNo { get; private set; }

    public static OperationResult<FaPostingType, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaPostingType, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaPostingType()
        {
            TenantId = tenantId
        };
        return OperationResult<FaPostingType, DomainError>.Ok(entity);
    }
}
