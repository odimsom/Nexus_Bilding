using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class Loaner : Entity
{
    private Loaner() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public string ItemNo { get; private set; }
    public DateTime? LastDateModified { get; private set; }
    public bool Blocked { get; private set; }
    public string NoSeries { get; private set; }
    public string SerialNo { get; private set; }

    public static OperationResult<Loaner, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<Loaner, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new Loaner()
        {
            TenantId = tenantId
        };
        return OperationResult<Loaner, DomainError>.Ok(entity);
    }
}
