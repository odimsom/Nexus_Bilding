using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class BookingMgrSetup : Entity
{
    private BookingMgrSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public int BookingMgrCodeunit { get; private set; }

    public static OperationResult<BookingMgrSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BookingMgrSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BookingMgrSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<BookingMgrSetup, DomainError>.Ok(entity);
    }
}
