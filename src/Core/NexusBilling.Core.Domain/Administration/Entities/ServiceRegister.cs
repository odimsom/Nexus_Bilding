using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceRegister : Entity
{
    private ServiceRegister() { }

    public TenantIdentifier TenantId { get; private set; }
    public int No { get; private set; }
    public int FromEntryNo { get; private set; }
    public int ToEntryNo { get; private set; }
    public int FromWarrantyEntryNo { get; private set; }
    public int ToWarrantyEntryNo { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public string SourceCode { get; private set; }
    public string UserId { get; private set; }

    public static OperationResult<ServiceRegister, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceRegister, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceRegister()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceRegister, DomainError>.Ok(entity);
    }
}
