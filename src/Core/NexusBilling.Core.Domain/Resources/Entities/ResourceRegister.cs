using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class ResourceRegister : Entity
{
    private ResourceRegister() { }

    public TenantIdentifier TenantId { get; private set; }
    public int No { get; private set; }
    public int FromEntryNo { get; private set; }
    public int ToEntryNo { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public string SourceCode { get; private set; }
    public string UserId { get; private set; }
    public string JournalBatchName { get; private set; }

    public static OperationResult<ResourceRegister, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ResourceRegister, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ResourceRegister()
        {
            TenantId = tenantId
        };
        return OperationResult<ResourceRegister, DomainError>.Ok(entity);
    }
}
