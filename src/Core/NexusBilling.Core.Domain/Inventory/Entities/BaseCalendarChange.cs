using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class BaseCalendarChange : Entity
{
    private BaseCalendarChange() { }

    public TenantIdentifier TenantId { get; private set; }
    public string BaseCalendarCode { get; private set; }
    public short RecurringSystem { get; private set; }
    public DateTime? Date { get; private set; }
    public short Day { get; private set; }
    public string Description { get; private set; }
    public bool Nonworking { get; private set; }

    public static OperationResult<BaseCalendarChange, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BaseCalendarChange, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new BaseCalendarChange()
        {
            TenantId = tenantId
        };
        return OperationResult<BaseCalendarChange, DomainError>.Ok(entity);
    }
}
