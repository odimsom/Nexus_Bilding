using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServiceOrderAllocation : Entity
{
    private ServiceOrderAllocation() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public short Status { get; private set; }
    public string DocumentNo { get; private set; }
    public DateTime? AllocationDate { get; private set; }
    public string ResourceNo { get; private set; }
    public string ResourceGroupNo { get; private set; }
    public int ServiceItemLineNo { get; private set; }
    public decimal AllocatedHours { get; private set; }
    public string StartingTime { get; private set; }
    public string FinishingTime { get; private set; }
    public string Description { get; private set; }
    public string ReasonCode { get; private set; }
    public string ServiceItemNo { get; private set; }
    public bool Posted { get; private set; }
    public string ServiceItemSerialNo { get; private set; }
    public bool ServiceStarted { get; private set; }
    public short DocumentType { get; private set; }

    public static OperationResult<ServiceOrderAllocation, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServiceOrderAllocation, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServiceOrderAllocation()
        {
            TenantId = tenantId
        };
        return OperationResult<ServiceOrderAllocation, DomainError>.Ok(entity);
    }
}
