using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class RepairStatus : Entity
{
    private RepairStatus() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public short ServiceOrderStatus { get; private set; }
    public short Priority { get; private set; }
    public bool Initial { get; private set; }
    public bool PartlyServiced { get; private set; }
    public bool InProcess { get; private set; }
    public bool Finished { get; private set; }
    public bool Referred { get; private set; }
    public bool SparePartOrdered { get; private set; }
    public bool SparePartReceived { get; private set; }
    public bool WaitingForCustomer { get; private set; }
    public bool QuoteFinished { get; private set; }
    public bool PostingAllowed { get; private set; }
    public bool PendingStatusAllowed { get; private set; }
    public bool InProcessStatusAllowed { get; private set; }
    public bool FinishedStatusAllowed { get; private set; }
    public bool OnHoldStatusAllowed { get; private set; }

    public static OperationResult<RepairStatus, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<RepairStatus, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new RepairStatus()
        {
            TenantId = tenantId
        };
        return OperationResult<RepairStatus, DomainError>.Ok(entity);
    }
}
