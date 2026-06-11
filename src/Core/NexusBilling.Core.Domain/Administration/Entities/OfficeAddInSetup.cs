using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class OfficeAddInSetup : Entity
{
    private OfficeAddInSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public int OfficeHostCodeunitId { get; private set; }

    public static OperationResult<OfficeAddInSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<OfficeAddInSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new OfficeAddInSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<OfficeAddInSetup, DomainError>.Ok(entity);
    }
}
