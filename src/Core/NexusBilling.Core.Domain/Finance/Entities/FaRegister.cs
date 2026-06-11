using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaRegister : Entity
{
    private FaRegister() { }

    public TenantIdentifier TenantId { get; private set; }
    public int No { get; private set; }
    public int FromEntryNo { get; private set; }
    public int ToEntryNo { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public string SourceCode { get; private set; }
    public string UserId { get; private set; }
    public string JournalBatchName { get; private set; }
    public short JournalType { get; private set; }
    public int GLRegisterNo { get; private set; }
    public int FromMaintenanceEntryNo { get; private set; }
    public int ToMaintenanceEntryNo { get; private set; }

    public static OperationResult<FaRegister, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaRegister, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaRegister()
        {
            TenantId = tenantId
        };
        return OperationResult<FaRegister, DomainError>.Ok(entity);
    }
}
