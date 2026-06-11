using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class DuplicateSearchStringSetup : Entity
{
    private DuplicateSearchStringSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public short Field { get; private set; }
    public short PartOfField { get; private set; }
    public int Length { get; private set; }

    public static OperationResult<DuplicateSearchStringSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<DuplicateSearchStringSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new DuplicateSearchStringSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<DuplicateSearchStringSetup, DomainError>.Ok(entity);
    }
}
