using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaDateType : Entity
{
    private FaDateType() { }

    public TenantIdentifier TenantId { get; private set; }
    public int FaDateTypeNo { get; private set; }
    public string FaDateTypeName { get; private set; }
    public bool FaEntry { get; private set; }
    public bool GLEntry { get; private set; }
    public int EntryNo { get; private set; }

    public static OperationResult<FaDateType, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaDateType, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaDateType()
        {
            TenantId = tenantId
        };
        return OperationResult<FaDateType, DomainError>.Ok(entity);
    }
}
