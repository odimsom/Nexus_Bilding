using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ContractChangeLog : Entity
{
    private ContractChangeLog() { }

    public TenantIdentifier TenantId { get; private set; }
    public short ContractType { get; private set; }
    public string ContractNo { get; private set; }
    public int ChangeNo { get; private set; }
    public string UserId { get; private set; }
    public DateTime? DateOfChange { get; private set; }
    public string TimeOfChange { get; private set; }
    public short ContractPart { get; private set; }
    public string FieldDescription { get; private set; }
    public string OldValue { get; private set; }
    public string NewValue { get; private set; }
    public short TypeOfChange { get; private set; }
    public string ServiceItemNo { get; private set; }
    public int ServContractLineNo { get; private set; }

    public static OperationResult<ContractChangeLog, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ContractChangeLog, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ContractChangeLog()
        {
            TenantId = tenantId
        };
        return OperationResult<ContractChangeLog, DomainError>.Ok(entity);
    }
}
