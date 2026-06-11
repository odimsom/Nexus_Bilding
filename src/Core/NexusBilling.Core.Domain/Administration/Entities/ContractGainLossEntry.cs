using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ContractGainLossEntry : Entity
{
    private ContractGainLossEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string ContractNo { get; private set; }
    public string ContractGroupCode { get; private set; }
    public DateTime? ChangeDate { get; private set; }
    public string ReasonCode { get; private set; }
    public short TypeOfChange { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public string CustomerNo { get; private set; }
    public string ShipToCode { get; private set; }
    public string UserId { get; private set; }
    public decimal Amount { get; private set; }

    public static OperationResult<ContractGainLossEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ContractGainLossEntry, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ContractGainLossEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<ContractGainLossEntry, DomainError>.Ok(entity);
    }
}
